using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using Zhao.FoxFire;
using Zhao.Forms;
using Zhao.Powers;
using Zhao.Pursuit;

namespace Zhao.Cards;

/// <summary>
/// セクション(段落)·Aメロ(主歌)。由前奏卡转化而来(不在初始卡组/奖励池)。技能牌(⚠️ 默认解释)。
/// 基础:1费。可玩条件(⚠️ 解释):段落=前奏,或当前处于巫女/淑女/小护士形态(规格定义了这些形态→主歌路径)。
/// 分支:
///  - 前奏阶段:消耗1狐火,1次追击(6伤害),回复6生命,回复1能量,前奏→主歌,本卡转化为副歌卡;
///  - 巫女→主歌:消耗1狐火,1次追击(6伤害),获得3点力量;
///  - 淑女→主歌:消耗2层光,获得1层狐火,回复1点能量;
///  - 小护士→主歌:此后每使用1张技能牌获得1层治愈(⚠️ 持续时间默认本场战斗)。
/// +:0费;额外直到下一回合开始前:自身造成的伤害+1、受到的伤害-1(仅主歌+拥有,主歌++保留)。
/// 各分支进入主歌后本卡均转化为副歌卡(⚠️ 形态路径的转化规格未明说,默认延续链条)。
/// 狐火部分为特殊能量资源(0.107.1 星辉同架构):分支条件消耗经 FoxFireCmd.Lose,获得经 FoxFireCmd.Gain。
/// 转化为副歌推迟到打出结算完成后(OnTransformAfterPlay)。
/// </summary>
public sealed class SectionMain : ZhaoCardModel
{
    public SectionMain() : base(1, CardType.Skill, CardRarity.Token, TargetType.Self)
    {
    }

    public override int MaxUpgradeLevel => 2;

    protected override bool IsPlayable
    {
        get
        {
            if (!base.IsPlayable)
            {
                return false;
            }
            var creature = base.Owner.Creature;
            var form = FormSystem.GetCurrentForm(creature);
            if (FormSystem.GetSection(creature)?.Stage == SectionStage.Intro)
                return true;
            return form is ZhaoForm.Kitsune or ZhaoForm.Lady or ZhaoForm.Nurse;
        }
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var creature = base.Owner.Creature;
        var player = base.Owner;
        var section = FormSystem.GetSection(creature);
        var form = FormSystem.GetCurrentForm(creature);

        if (section?.Stage == SectionStage.Intro)
        {
            // 【前奏进入主歌】狐火强化:有狐火则消耗1并追击6;无狐火则跳过追击(用户确认:不追击,其他继续)
            if (FoxFireCmd.Get(player) > 0)
            {
                await FoxFireCmd.Lose(1, player);
                await PursuitExecutor.Chase(choiceContext, player, hitCount: 1, damagePerHit: 6m, target: null);
            }
            await CreatureCmd.Heal(creature, 6m);
            await PlayerCmd.GainEnergy(1m, player);
            await FormSystem.SetStage(choiceContext, creature, SectionStage.Main);
        }
        else if (form == ZhaoForm.Kitsune)
        {
            // 【巫女→主歌】狐火强化:有狐火则消耗1并追击6(用户通则:无能量则强化效果不可用,普通效果继续)
            if (FoxFireCmd.Get(player) > 0)
            {
                await FoxFireCmd.Lose(1, player);
                await PursuitExecutor.Chase(choiceContext, player, hitCount: 1, damagePerHit: 6m, target: null);
            }
            await PowerCmd.Apply<StrengthPower>(choiceContext, creature, 3m, creature, this);
            await FormSystem.SwitchForm(choiceContext, creature, ZhaoForm.Diva);
            await FormSystem.SetStage(choiceContext, creature, SectionStage.Main);
        }
        else if (form == ZhaoForm.Lady)
        {
            // 【淑女→主歌】消耗2层光,获得1层狐火,回复1点能量
            int light = creature.GetPowerAmount<LightPower>();
            int consumed = Math.Min(2, light);   // ⚠️ 光不足2时按可消耗量处理(规格未定义)
            if (consumed > 0)
            {
                await PowerCmd.ModifyAmount(choiceContext, creature.GetPower<LightPower>()!, -consumed, creature, this);
            }
            await FoxFireCmd.Gain(1, player);
            await PlayerCmd.GainEnergy(1m, player);
            await FormSystem.SwitchForm(choiceContext, creature, ZhaoForm.Diva);
            await FormSystem.SetStage(choiceContext, creature, SectionStage.Main);
        }
        else if (form == ZhaoForm.Nurse)
        {
            // 【小护士→主歌】此后每使用1张技能牌获得1层治愈(⚠️ 持续时间默认本场战斗)
            await FormSystem.SwitchForm(choiceContext, creature, ZhaoForm.Diva);
            await FormSystem.SetStage(choiceContext, creature, SectionStage.Main);
            await PowerCmd.Apply<NurseMainHealingPower>(choiceContext, creature, 1m, creature, this);
        }

        // 主歌+:直到下一回合开始前,自身造成的伤害+1、受到的伤害-1(主歌++保留)
        if (base.CurrentUpgradeLevel >= 1)
        {
            await PowerCmd.Apply<MainMelodyPower>(choiceContext, creature, 1m, creature, this);
        }
    }

    protected override Task? OnTransformAfterPlay() => TransformHelper.TransformInto<SectionChorus>(this);

    protected override void OnUpgrade()
    {
        if (base.CurrentUpgradeLevel == 1)
        {
            // 基础1费 → +0费(++保持0费)
            base.EnergyCost.UpgradeBy(-1);
        }
    }
}
