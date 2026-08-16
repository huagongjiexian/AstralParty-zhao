using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Zhao.FoxFire;
using Zhao.Pursuit;

namespace Zhao.Cards;

/// <summary>
/// 狐火打击(初始卡1张)。用户决定:攻击牌。
/// 基础:1费,造成6伤害,获得1狐火。
/// +:8伤害,2狐火。++:10伤害,3狐火,然后进行1次不消耗狐火的追击(6伤害)。
/// 狐火为特殊能量资源(0.107.1 星辉同架构),经 FoxFireCmd 增加,不再是 Power/Buff。
/// </summary>
public sealed class KitsuneFireStrike : ZhaoCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DamageVar(6m, ValueProp.Move),
        new IntVar("Foxfire", 1m),
    };

    public KitsuneFireStrike() : base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
    {
    }

    public override int MaxUpgradeLevel => 2;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");

        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .Execute(choiceContext);

        // 获得狐火(特殊资源,对应原版 PlayerCmd.GainStars)
        await FoxFireCmd.Gain((int)base.DynamicVars["Foxfire"].IntValue, base.Owner);

        // ++:1次不消耗狐火的追击,6伤害(目标=本卡目标 ⚠️ 规格未指定追击目标)
        if (base.CurrentUpgradeLevel >= 2)
        {
            await PursuitExecutor.Chase(choiceContext, base.Owner, hitCount: 1, damagePerHit: 6m, target: cardPlay.Target);
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(2m);
        base.DynamicVars["Foxfire"].UpgradeValueBy(1m);
    }
}
