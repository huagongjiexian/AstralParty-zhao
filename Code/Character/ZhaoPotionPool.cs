using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Potions;

namespace Zhao.Character;

/// <summary>药水池:规格未定义任何专属药水 → 空池。</summary>
public class ZhaoPotionPool : PotionPoolModel
{
    public override string EnergyColorName => "ironclad"; // ⚠️ 占位

    protected override IEnumerable<PotionModel> GenerateAllPotions() => Array.Empty<PotionModel>();
}
