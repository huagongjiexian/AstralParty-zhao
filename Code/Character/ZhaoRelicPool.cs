using System.Collections.Generic;
using MegaCrit.Sts2.Core.Models;
using Zhao.Relics;

namespace Zhao.Character;

public class ZhaoRelicPool : RelicPoolModel
{
	public override string EnergyColorName => "ironclad";

	protected override global::System.Collections.Generic.IEnumerable<RelicModel> GenerateAllRelics()
	{
		return (global::System.Collections.Generic.IEnumerable<RelicModel>)(object)new FoxFireRelic[1] { ModelDb.Relic<FoxFireRelic>() };
	}
}
