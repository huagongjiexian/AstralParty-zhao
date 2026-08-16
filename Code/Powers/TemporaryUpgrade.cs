using MegaCrit.Sts2.Core.Models;

namespace Zhao.Powers;

public static class TemporaryUpgrade
{
	public static void ApplyOneLevel(CardModel card)
	{
		card.UpgradeInternal();
		card.FinalizeUpgradeInternal();
	}

	public static void RevertToLevel(CardModel card, int originalLevel)
	{
		card.DowngradeInternal();
		for (int i = 0; i < originalLevel; i++)
		{
			card.UpgradeInternal();
			card.FinalizeUpgradeInternal();
		}
	}
}
