using System.Collections.Generic;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using Zhao.Cards;
using Zhao.Relics;

namespace Zhao.Character;

public class ZhaoCharacter : CharacterModel
{
	public const string CharacterId = "zhao";

	public override int StartingHp => 75;

	public override int StartingGold => 99;

	public override CharacterGender Gender => (CharacterGender)1;

	public override Color NameColor => new Color("FFB300");

	public override Color EnergyLabelOutlineColor => new Color("801212FF");

	public override Color DialogueColor => new Color("590700");

	public override VfxColor SpeechBubbleColor => (VfxColor)0;

	public override Color MapDrawingColor => new Color("CB282B");

	public override float AttackAnimDelay => 0.15f;

	public override float CastAnimDelay => 0.25f;

	protected override string MapMarkerPath => "res://zhao/images/ui/zhao_character_placeholder.png";

	protected override string IconPath => SceneHelper.GetScenePath("ui/character_icons/zhao_icon");

	protected override string CharacterSelectIconPath => "res://zhao/images/char_select/zhao_select_icon.png";

	protected override string CharacterSelectLockedIconPath => "res://zhao/images/char_select/zhao_select_locked.png";

	public override string CharacterSelectSfx => "event:/sfx/characters/ironclad/ironclad_select";

	public override CardPoolModel CardPool => (CardPoolModel)ModelDb.CardPool<ZhaoCardPool>();

	public override RelicPoolModel RelicPool => (RelicPoolModel)ModelDb.RelicPool<ZhaoRelicPool>();

	public override PotionPoolModel PotionPool => (PotionPoolModel)ModelDb.PotionPool<ZhaoPotionPool>();

	protected override CharacterModel? UnlocksAfterRunAs => null;

	public override global::System.Collections.Generic.IEnumerable<CardModel> StartingDeck => (global::System.Collections.Generic.IEnumerable<CardModel>)(object)new CardModel[12]
	{
		ModelDb.Card<KitsuneFireStrike>(),
		ModelDb.Card<LightCard>(),
		ModelDb.Card<SectionIntro>(),
		ModelDb.Card<EmergencyTreatment>(),
		(CardModel)ModelDb.Card<StrikeIronclad>(),
		(CardModel)ModelDb.Card<StrikeIronclad>(),
		(CardModel)ModelDb.Card<StrikeIronclad>(),
		(CardModel)ModelDb.Card<StrikeIronclad>(),
		(CardModel)ModelDb.Card<DefendIronclad>(),
		(CardModel)ModelDb.Card<DefendIronclad>(),
		(CardModel)ModelDb.Card<DefendIronclad>(),
		(CardModel)ModelDb.Card<DefendIronclad>()
	};

	public override global::System.Collections.Generic.IReadOnlyList<RelicModel> StartingRelics => (global::System.Collections.Generic.IReadOnlyList<RelicModel>)(object)new FoxFireRelic[1] { ModelDb.Relic<FoxFireRelic>() };

	public override List<string> GetArchitectAttackVfx()
	{
		List<string> obj = new List<string>();
		obj.Add("vfx/vfx_attack_blunt");
		obj.Add("vfx/vfx_heavy_blunt");
		obj.Add("vfx/vfx_attack_slash");
		obj.Add("vfx/vfx_bloody_impact");
		obj.Add("vfx/vfx_rock_shatter");
		return obj;
	}
}
