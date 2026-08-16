using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using Zhao.FoxFire;

namespace Zhao.Relics;

/// <summary>
/// 狐之火(初始遗物,用户设计):战斗开始时,获得2层狐火。
/// 狐火为特殊能量式战斗资源(可累积、战斗结束清零)。
/// </summary>
public sealed class FoxFireRelic : RelicModel
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    // 图标:模组自带占位 PNG(正式美术待动作/美术资源确认后替换)。
    // 路径必须指向模组自身 pck 内的真实路径(res://zhao/images/...);
    // 此前误用 ImageHelper.GetImagePath → res://images/... 前缀,导致游戏中资源路径不存在,
    // ResourceLoader.Load<Texture2D> 报 "No loader found"。加载机制保持原版(经 .import 侧车 remap 到 .godot/imported 的 ctex)。
    public override string PackedIconPath => "res://zhao/images/packed/relics/fox_fire_relic.png";
    protected override string PackedIconOutlinePath => "res://zhao/images/packed/relics/fox_fire_relic_outline.png";
    protected override string BigIconPath => "res://zhao/images/relics/fox_fire_relic.png";

    public override async Task BeforeCombatStart()
    {
        Flash();
        // 战斗开始时获得2点狐火 —— 狐火是特殊能量资源(0.107.1 星辉同架构),
        // 经 FoxFireCmd.Gain 增加(对应原版 PlayerCmd.GainStars),不再是 Power/Buff。
        await FoxFireCmd.Gain(2, base.Owner);
    }
}
