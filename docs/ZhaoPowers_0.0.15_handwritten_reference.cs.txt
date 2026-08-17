using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using Zhao.FoxFire;
using Zhao.Forms;
using Zhao.Pursuit;

namespace Zhao.Powers;

/// <summary>
/// 形态标记基类(单实例 Buff)。子类即四种形态:
/// 巫女形态 KitsuneFormPower / 小护士形态 NurseFormPower / 歌姬形态 DivaFormPower / 淑女形态 LadyFormPower。
/// </summary>
public abstract class ZhaoFormPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    public override int DisplayAmount => 0;
}

public class NurseFormPower : ZhaoFormPower { }
public class DivaFormPower : ZhaoFormPower { }
public class LadyFormPower : ZhaoFormPower { }

/// <summary>
/// 巫女形态:狐火获取规则(规格第9/13节)。
/// 处于巫女形态时,使用原始费用为3费的卡牌 → 获得1点狐火;
/// 前奏存在期间,使用2费卡(当前费用2,含被前奏降为2费的原始3费牌)时也可以触发狐火获取。
/// 狐火为特殊能量资源(0.107.1 星辉同架构),经 FoxFireCmd.Gain 增加。
/// </summary>
public class KitsuneFormPower : ZhaoFormPower
{
    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var card = cardPlay.Card;
        if (card == null || card.Owner?.Creature != base.Owner)
            return;
        if (!cardPlay.IsFirstInSeries)
            return;   // ⚠️ 重放只计一次(默认解释)

        int canonical = card.EnergyCost.Canonical;
        bool inIntro = FormSystem.GetSection(base.Owner)?.Stage == SectionStage.Intro;
        int currentCost = (int)card.EnergyCost.GetWithModifiers(CostModifiers.All);

        bool grantsFire = canonical == 3 || (inIntro && currentCost == 2);
        if (grantsFire)
        {
            var player = FormSystem.PlayerFor(base.Owner);
            if (player != null)
            {
                await FoxFireCmd.Gain(1, player);
            }
        }
    }
}

/// <summary>治愈(小护士核心):可叠加;回合开始回复层数生命,回合结束层数减半(内部向下取整)。</summary>
public class HealingPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState state)
    {
        if (side != CombatSide.Player || !participants.Contains(base.Owner) || base.Owner.IsDead)
            return;
        await CreatureCmd.Heal(base.Owner, base.Amount);
    }

    public override async Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side != CombatSide.Player || !participants.Contains(base.Owner) || base.Owner.IsDead)
            return;
        // 层数减半,向下取整(仅内部计算;玩家文本不写取整)
        int newAmount = (int)decimal.Floor(base.Amount / 2m);
        await PowerCmd.ModifyAmount(choiceContext, this, newAmount - base.Amount, base.Owner, null);
    }
}

/// <summary>光(淑女核心):层数资源。</summary>
public class LightPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}

/// <summary>
/// 段落(歌姬状态机):Amount 存阶段序号(1前奏/2主歌/3副歌/4间奏/5尾声)。
/// 段落状态独立于形态 Power —— 切换形态后段落保留(规格第14~16节的前奏→X 切换以此为前提)。
/// </summary>
public class SectionPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    public override int DisplayAmount => 0;

    public SectionStage Stage => (SectionStage)(int)base.Amount;

    /// <summary>触发进入尾声的那张尾声卡的升级等级(结算数值用:0=基础,1=+,2=++)。</summary>
    public int OutroLevel { get; set; }

    /// <summary>前奏存在期间:所有费用大于2的卡牌费用-1(规格第13节)。</summary>
    public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
    {
        modifiedCost = originalCost;
        if (Stage != SectionStage.Intro)
            return false;
        if (card.Owner?.Creature != base.Owner)
            return false;
        if (originalCost > 2m)
        {
            modifiedCost = originalCost - 1m;
            return true;
        }
        return false;
    }

    /// <summary>副歌存在期间,回合结束(玩家侧)自动进入间奏;解放随高潮结束消失(用户决定)。</summary>
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side != CombatSide.Player || !participants.Contains(base.Owner) || Stage != SectionStage.Chorus)
            return;

        await PowerCmd.ModifyAmount(choiceContext, this, (decimal)SectionStage.Interlude - (decimal)base.Amount, base.Owner, null);
        await PowerCmd.Remove<LiberationPower>(base.Owner);
        await FormSystem.EnterInterlude(choiceContext, base.Owner);
    }

    /// <summary>检测进入尾声阶段 → 执行尾声资源结算(规格第27/28节;结算不属于尾声卡本身)。</summary>
    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power == this && Stage == SectionStage.Outro)
        {
            await OutroSettlement(choiceContext);
        }
    }

    /// <summary>
    /// 进入尾声阶段时的效果:
    /// 【狐火】消耗全部狐火:基础 F次追击×6;+:(F+floor(F/4))次×7(++与+相同——规格明令不得再提高)。
    /// 【光】消耗全部光:基础 L能量;+:L+floor(L/4)能量。
    /// 【治愈】消耗全部治愈:基础 H生命;+:H+floor(H/4)生命。
    /// 整数除法即向下取整(仅内部计算,不写入玩家文本)。
    /// </summary>
    private async Task OutroSettlement(PlayerChoiceContext choiceContext)
    {
        var player = FormSystem.PlayerFor(base.Owner);
        if (player == null)
            return;

        bool upgraded = OutroLevel >= 1;   // 尾声+/++ 同数值(++未确认新数值,不得提高)

        // 【狐火】消耗全部狐火:基础 F次追击×6;+:(F+floor(F/4))次×7(++与+相同——规格明令不得再提高)。
        // 狐火为特殊能量资源(0.107.1 星辉同架构),经 FoxFireCmd 查询/消耗。
        int foxfire = FoxFireCmd.Get(player);
        if (foxfire > 0)
        {
            int hits = foxfire + (upgraded ? foxfire / 4 : 0);
            decimal damage = upgraded ? 7m : 6m;
            await PursuitExecutor.Chase(choiceContext, player, hits, damage, null);
            await FoxFireCmd.Lose(foxfire, player);
        }

        // 【光】
        int light = base.Owner.GetPowerAmount<LightPower>();
        if (light > 0)
        {
            int energy = light + (upgraded ? light / 4 : 0);
            await PlayerCmd.GainEnergy(energy, player);
            await PowerCmd.Remove<LightPower>(base.Owner);
        }

        // 【治愈】
        int healing = base.Owner.GetPowerAmount<HealingPower>();
        if (healing > 0)
        {
            int heal = healing + (upgraded ? healing / 4 : 0);
            await CreatureCmd.Heal(base.Owner, heal);
            await PowerCmd.Remove<HealingPower>(base.Owner);
        }

        // 尾声结束后段落结束(⚠️ 默认:序列完结,可重新开始;待用户确认)
        await PowerCmd.Remove(this);
    }
}

/// <summary>
/// 解放:副歌阶段内临时强化所有卡牌(基础→+;已是+且存在++的→++;++不变)。
/// 用户决定:解放随高潮(副歌)结束而消失 → 由 SectionPower 在进入间奏时移除本 Power。
/// </summary>
public class LiberationPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    private readonly List<(CardModel card, int originalLevel)> _bumped = new();

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        await base.AfterApplied(applier, cardSource);
        _bumped.Clear();

        var player = PlayerForOwner();
        if (player == null) return;

        foreach (var card in AllCombatCards(player))
        {
            if (card.CurrentUpgradeLevel < card.MaxUpgradeLevel)
            {
                _bumped.Add((card, card.CurrentUpgradeLevel));
                TemporaryUpgrade.ApplyOneLevel(card);
            }
        }
    }

    public override async Task AfterRemoved(Creature oldOwner)
    {
        foreach (var (card, originalLevel) in _bumped)
        {
            TemporaryUpgrade.RevertToLevel(card, originalLevel);
        }
        _bumped.Clear();
        await base.AfterRemoved(oldOwner);
    }

    private Player? PlayerForOwner()
    {
        var combatState = base.Owner.CombatState;
        return combatState?.Players.FirstOrDefault(p => p.Creature == base.Owner);
    }

    private static IEnumerable<CardModel> AllCombatCards(Player player)
    {
        var state = player.PlayerCombatState;
        if (state == null)
        {
            return Array.Empty<CardModel>();
        }
        return state.Hand.Cards
            .Concat(state.DrawPile.Cards)
            .Concat(state.DiscardPile.Cards)
            .Concat(state.ExhaustPile.Cards);
    }
}
