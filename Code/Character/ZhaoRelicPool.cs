using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using Zhao.Relics;

namespace Zhao.Character;

/// <summary>
/// 遗物池:包含用户设计的初始遗物「狐之火」。
/// 原版规则:每个 RelicModel 必须归属某个 RelicPoolModel(RelicModel.Pool 通过 AllRelicIds 查找),
/// 起始遗物同样加入角色专属池(参考 IroncladRelicPool 收录 BurningBlood 的做法)。
/// </summary>
public class ZhaoRelicPool : RelicPoolModel
{
    public override string EnergyColorName => "ironclad"; // ⚠️ 占位(能量图标前缀,待正式配色)

    protected override IEnumerable<RelicModel> GenerateAllRelics() => new[]
    {
        ModelDb.Relic<FoxFireRelic>(),
    };
}
