using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Zhao.Forms;
using Zhao.Pursuit;

namespace Zhao.Cards;

/// <summary>
/// 追击追击。普通,攻击牌(⚠️ 类型为默认解释)。
/// 【基础】2费,狐火2:进行2次追击,每次6伤害;巫女形态额外进行1次追击。
/// 【+】2费,狐火2:与基础相同(不得擅自加强)。
/// 【++】3费,狐火1:进行4次追击,每次8伤害;巫女形态额外进行2次(总计6次)。
/// 狐火2/狐火1 为真正的资源支付成本:FoxFireCost 声明成本(对应原版 CanonicalStarCost),
/// 狐火不足时卡牌不可使用(对应原版 HasEnoughResourcesFor 星辉闸门),
/// 支付由 ZhaoFoxFireCombatHooks.BeforeCardPlayed 在原版出牌管线中效果执行前自动完成(对应原版 SpendStars)。
/// </summary>
public sealed class ChaseChase : ZhaoCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new IntVar("Foxfire", 2m),
        new IntVar("Hits", 2m),
        new IntVar("ChaseDamage", 6m),
    };

    public ChaseChase() : base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
    }

    public override int MaxUpgradeLevel => 2;

    /// <summary>狐火支付成本:基础/+ 为 2;++ 为 1。对应原版 CanonicalStarCost。</summary>
    public override int FoxFireCost => (int)base.DynamicVars["Foxfire"].IntValue;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        var creature = base.Owner.Creature;

        int hits = (int)base.DynamicVars["Hits"].IntValue;
        decimal damage = base.DynamicVars["ChaseDamage"].BaseValue;

        // 巫女形态额外追击:基础/+:1次;++:2次
        if (FormSystem.GetCurrentForm(creature) == ZhaoForm.Kitsune)
        {
            hits += base.CurrentUpgradeLevel >= 2 ? 2 : 1;
        }

        await PursuitExecutor.Chase(choiceContext, base.Owner, hitCount: hits, damagePerHit: damage, target: cardPlay.Target);
    }

    protected override void OnUpgrade()
    {
        if (base.CurrentUpgradeLevel == 1)
        {
            // 基础→+:效果与基础相同(不得擅自加强)→ 不改任何数值
        }
        else if (base.CurrentUpgradeLevel == 2)
        {
            // ++:3费,狐火1,4次追击×8伤害
            base.EnergyCost.UpgradeBy(1);                    // 2费 → 3费
            base.DynamicVars["Foxfire"].UpgradeValueBy(-1m); // 2 → 1
            base.DynamicVars["Hits"].UpgradeValueBy(2m);     // 2 → 4
            base.DynamicVars["ChaseDamage"].UpgradeValueBy(2m); // 6 → 8
        }
    }
}
