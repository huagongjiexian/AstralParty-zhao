using Godot;
using MegaCrit.Sts2.Core.Models;
using Zhao.Cards;

namespace Zhao.Character;

public class ZhaoCardPool : CardPoolModel
{
	public override string Title => "zhao";

	public override string EnergyColorName => "ironclad";

	public override string CardFrameMaterialPath => "card_frame_red";

	public override Color DeckEntryCardColor => new Color("FFB300");

	public override bool IsColorless => false;

	protected override CardModel[] GenerateAllCards()
	{
		return (CardModel[])(object)new CardModel[8]
		{
			ModelDb.Card<KitsuneFireStrike>(),
			ModelDb.Card<LightCard>(),
			ModelDb.Card<SectionIntro>(),
			ModelDb.Card<EmergencyTreatment>(),
			ModelDb.Card<ChaseChase>(),
			ModelDb.Card<FastForward>(),
			ModelDb.Card<OutroCard>(),
			ModelDb.Card<LightYo>()
		};
	}
}
