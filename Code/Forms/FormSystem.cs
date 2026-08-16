using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using Zhao.Cards;
using Zhao.FoxFire;
using Zhao.Powers;
using Zhao.Pursuit;

namespace Zhao.Forms;

/// <summary>四种形态(与四个控制器/图集变体对应:base=巫女、_01=小护士、_02=歌姬、Max=淑女)。</summary>
public enum ZhaoForm
{
    None = 0,
    Kitsune = 1,  // 巫女形态
    Nurse = 2,    // 小护士形态
    Diva = 3,     // 歌姬形态
    Lady = 4,     // 淑女形态
}

/// <summary>段落阶段(固定顺序,不可倒退/跳级):前奏→主歌→副歌→间奏→尾声。</summary>
public enum SectionStage
{
    None = 0,
    Intro = 1,     // イントロ(前奏)
    Main = 2,      // Aメロ(主歌)
    Chorus = 3,    // サビ(副歌/高潮)
    Interlude = 4, // 間奏(间奏)
    Outro = 5,     // アウトロ(尾声)
}

/// <summary>
/// 形态系统核心:形态查询/切换与段落推进。
/// 切换限制:无(规格:同回合可多次切换,不增加任何冷却/次数限制)。
/// 段落 Power 独立于形态 Power:切换形态后段落保留(前奏→X 切换的前提)。
/// </summary>
public static class FormSystem
{
    // ---------- 形态查询 ----------
    public static ZhaoForm GetCurrentForm(Creature creature)
    {
        if (creature.HasPower<KitsuneFormPower>()) return ZhaoForm.Kitsune;
        if (creature.HasPower<NurseFormPower>()) return ZhaoForm.Nurse;
        if (creature.HasPower<DivaFormPower>()) return ZhaoForm.Diva;
        if (creature.HasPower<LadyFormPower>()) return ZhaoForm.Lady;
        return ZhaoForm.None;
    }

    // ---------- 形态切换(离场结算→前奏入口效果→进入新形态) ----------
    /// <summary>
    /// 切换到目标形态。流程:
    /// 1) 当前形态离场结算(规格已确认部分);
    /// 2) 若当前处于"歌姬形态·前奏阶段",执行前奏→目标形态的入口效果(规格第14/15/16节);
    /// 3) 移除旧形态标记,应用新形态标记。
    /// </summary>
    public static async Task SwitchForm(PlayerChoiceContext choiceContext, Creature creature, ZhaoForm targetForm)
    {
        var current = GetCurrentForm(creature);
        var section = GetSection(creature);
        bool fromIntro = current == ZhaoForm.Diva && section?.Stage == SectionStage.Intro;

        // 1) 离场结算(规格已确认部分)
        if (current != ZhaoForm.None && current != targetForm)
        {
            switch (current)
            {
                case ZhaoForm.Kitsune:
                    // 【离开巫女形态】进行1次追击,消耗1层狐火,并额外造成"消耗后剩余狐火数"的伤害
                    await LeaveKitsuneForm(choiceContext, creature);
                    break;
                case ZhaoForm.Nurse:
                    // 【离开小护士形态】回复等同于当前治愈层数的生命(不消耗治愈——规格明令不得减少)
                    await LeaveNurseForm(creature);
                    break;
                case ZhaoForm.Lady:
                    // 【离开淑女形态】回复2点能量
                    await LeaveLadyForm(creature);
                    break;
                case ZhaoForm.Diva:
                    // 离开歌姬形态:规格未定义离场效果;隐藏歌姬常态视频背景(暂停,不重建)
                    DivaVideoBackground.HideFromDivaForm();
                    break;
            }
        }

        // 2) 前奏→目标形态的入口效果(规格第14/15/16节;段落保留)
        if (fromIntro && targetForm != ZhaoForm.Diva)
        {
            switch (targetForm)
            {
                case ZhaoForm.Kitsune:
                    // 前奏→巫女:先获得1层狐火,然后1次不消耗狐火的追击,伤害=当前狐火层数的一半(至少1,内部向下取整)
                    var p1 = PlayerFor(creature);
                    if (p1 == null) break;
                    await FoxFireCmd.Gain(1, p1);
                    int fireAfterGain = FoxFireCmd.Get(p1);
                    decimal chaseDamage = Math.Max(1m, (decimal)(fireAfterGain / 2));
                    await PursuitExecutor.Chase(choiceContext, p1, hitCount: 1, damagePerHit: chaseDamage, target: null);
                    break;
                case ZhaoForm.Nurse:
                    // 前奏→小护士:先获得5层治愈,然后回复等同于当前治愈总层数的生命(至少1;必须先获得再计算)
                    await PowerCmd.Apply<HealingPower>(choiceContext, creature, 5m, creature, null);
                    int healingAfterGain = creature.GetPowerAmount<HealingPower>();
                    await CreatureCmd.Heal(creature, Math.Max(1m, healingAfterGain));
                    break;
                case ZhaoForm.Lady:
                    // 前奏→淑女:先获得1层光,然后回复当前光层数一半的能量(最低0,内部向下取整)
                    await PowerCmd.Apply<LightPower>(choiceContext, creature, 1m, creature, null);
                    int lightAfterGain = creature.GetPowerAmount<LightPower>();
                    var p2 = PlayerFor(creature);
                    if (p2 != null && lightAfterGain > 0)
                    {
                        await PlayerCmd.GainEnergy(decimal.Floor(lightAfterGain / 2m), p2);
                    }
                    break;
            }
        }

        // 3) 移除旧形态标记
        await PowerCmd.Remove<KitsuneFormPower>(creature);
        await PowerCmd.Remove<NurseFormPower>(creature);
        await PowerCmd.Remove<DivaFormPower>(creature);
        await PowerCmd.Remove<LadyFormPower>(creature);

        // 4) 进入新形态
        switch (targetForm)
        {
            case ZhaoForm.Kitsune:
                await PowerCmd.Apply<KitsuneFormPower>(choiceContext, creature, 1m, creature, null);
                break;
            case ZhaoForm.Nurse:
                await PowerCmd.Apply<NurseFormPower>(choiceContext, creature, 1m, creature, null);
                break;
            case ZhaoForm.Diva:
                await PowerCmd.Apply<DivaFormPower>(choiceContext, creature, 1m, creature, null);
                // 进入歌姬形态:启用歌姬常态视频背景(以角色稳定战斗位置为锚点,悬挂于角色正上方,屏幕顶)
                DivaVideoBackground.ShowForDivaForm(creature);
                break;
            case ZhaoForm.Lady:
                await PowerCmd.Apply<LadyFormPower>(choiceContext, creature, 1m, creature, null);
                break;
        }
    }

    /// <summary>
    /// 离开巫女形态:进行1次追击,消耗1狐火,并额外造成"消耗后剩余狐火数"的伤害。
    /// 用户通则(0.0.6):狐火只解锁强化效果——狐火为0时跳过追击(无能量则强化效果不可用,普通效果继续)。
    /// </summary>
    private static async Task LeaveKitsuneForm(PlayerChoiceContext choiceContext, Creature creature)
    {
        var player = PlayerFor(creature);
        if (player == null) return;

        int fire = FoxFireCmd.Get(player);
        if (fire > 0)
        {
            await FoxFireCmd.Lose(1, player);
            fire -= 1;
            await PursuitExecutor.Chase(choiceContext, player, hitCount: 1, damagePerHit: 1m, target: null);
            // 额外伤害:消耗1层后剩余狐火数量
            if (fire > 0)
            {
                await PursuitExecutor.Chase(choiceContext, player, hitCount: 1, damagePerHit: fire, target: null);
            }
        }
    }

    private static async Task LeaveNurseForm(Creature creature)
    {
        int healing = creature.GetPowerAmount<HealingPower>();
        if (healing > 0 && !creature.IsDead)
        {
            await CreatureCmd.Heal(creature, healing); // 不消耗治愈(规格)
        }
    }

    private static async Task LeaveLadyForm(Creature creature)
    {
        var player = PlayerFor(creature);
        if (player != null)
        {
            await PlayerCmd.GainEnergy(2m, player); // 离开淑女形态:回复2点能量
        }
    }

    // ---------- 段落推进 ----------
    public static SectionPower? GetSection(Creature creature) => creature.GetPower<SectionPower>();

    public static async Task SetStage(PlayerChoiceContext choiceContext, Creature creature, SectionStage stage)
    {
        var section = creature.GetPower<SectionPower>();
        if (section == null)
        {
            await PowerCmd.Apply<SectionPower>(choiceContext, creature, (decimal)stage, creature, null);
            return;
        }
        await PowerCmd.ModifyAmount(choiceContext, section, (decimal)stage - section.Amount, creature, null);
    }

    /// <summary>
    /// 进入间奏:玩家从抽牌堆或弃牌区选择1张"形态卡"并免费打出(用户确认:玩家选择、免费打出)。
    /// 选择过滤:形态卡(前奏卡/照小姐/紧急治疗;巫女无进入卡——规格未定义)且当前可打出(CanPlay,
    /// 防止自动打出把前奏在歌姬形态中打出导致段落回退)。
    /// ⚠️ 待确认:两堆合并选择的呈现方式(当前实现:抽牌堆有可选卡则从抽牌堆选,否则从弃牌区选)。
    /// </summary>
    public static async Task EnterInterlude(PlayerChoiceContext choiceContext, Creature creature)
    {
        var player = PlayerFor(creature);
        if (player == null) return;

        // 形态卡筛选:能进入形态且当前可打出的卡
        static bool IsFormCard(CardModel c) =>
            c is Cards.SectionIntro or Cards.LightCard or Cards.EmergencyTreatment;

        var state = player.PlayerCombatState;
        if (state == null) return;

        // 先抽牌堆,再弃牌区
        CardPile? sourcePile = null;
        if (state.DrawPile.Cards.Any(c => IsFormCard(c) && c.CanPlay()))
        {
            sourcePile = state.DrawPile;
        }
        else if (state.DiscardPile.Cards.Any(c => IsFormCard(c) && c.CanPlay()))
        {
            sourcePile = state.DiscardPile;
        }
        if (sourcePile == null)
        {
            return;
        }

        var prefs = new CardSelectorPrefs(new LocString("characters", "ZHAO_CHARACTER.interludeCardPrompt"), 1);
        var selection = await CardSelectCmd.FromCombatPile(
            choiceContext, sourcePile, player, prefs, c => IsFormCard(c) && c.CanPlay());
        var picked = selection.FirstOrDefault();
        if (picked == null)
        {
            return;
        }

        // 免费自动打出(本体 AutoPlay 不支付能量/星辉/狐火)
        await CardCmd.AutoPlay(choiceContext, picked, null);
    }

    public static Player? PlayerFor(Creature creature)
    {
        return creature.CombatState?.Players.FirstOrDefault(p => p.Creature == creature);
    }
}
