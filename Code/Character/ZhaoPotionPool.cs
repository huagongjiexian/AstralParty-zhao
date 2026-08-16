using System;
using System.Collections.Generic;
using MegaCrit.Sts2.Core.Models;

namespace Zhao.Character;

public class ZhaoPotionPool : PotionPoolModel
{
	public override string EnergyColorName => "ironclad";

	protected override global::System.Collections.Generic.IEnumerable<PotionModel> GenerateAllPotions()
	{
		return global::System.Array.Empty<PotionModel>();
	}
}
