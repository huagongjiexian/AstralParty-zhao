using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using Zhao.Cards;
using Zhao.Relics;

namespace Zhao.Character;

/// <summary>
/// 角色「照」:四形态(巫女/小护士/歌姬/淑女)。
/// 视觉资源当前全部借用铁甲战士占位(用户决定:动作资源待选,先占位,可换)。
/// </summary>
public class ZhaoCharacter : CharacterModel
{
    public const string CharacterId = "zhao";

    // ---------- 数值(规格未定的以 ⚠️ 标注) ----------
    public override int StartingHp => 75;              // ⚠️ 规格未给 HP,暂定 75
    public override int StartingGold => 99;
    public override CharacterGender Gender => CharacterGender.Feminine; // 语法性别,代词用她

    // ---------- 视觉(占位,全部借用铁甲战士;正式美术待动作确认后替换) ----------
    public override Color NameColor => new("FFB300");
    public override Color EnergyLabelOutlineColor => new("801212FF");
    public override Color DialogueColor => new("590700");
    public override VfxColor SpeechBubbleColor => VfxColor.Red;
    public override Color MapDrawingColor => new("CB282B");
    public override float AttackAnimDelay => 0.15f;
    public override float CastAnimDelay => 0.25f;

    // 占位视觉(0.0.6 起移除全部铁甲战士图片占位,改用模组自带占位图;正式美术待用户提供)。
    // 注意:本体的部分视觉属性不是 virtual(VisualsPath/TrailPath/IconTexturePath/EnergyCounterPath/
    // RestSiteAnimPath/MerchantAnimPath/CharacterSelectBg/Sfx 等),改由 ZhaoVisualPatch(Harmony)统一占位。
    protected override string MapMarkerPath => "res://zhao/images/ui/zhao_character_placeholder.png";
    protected override string IconPath => SceneHelper.GetScenePath("ui/character_icons/zhao_icon");
    // 选角按钮图标:全透明占位 —— 按钮视觉由用户的选角框图承担(不再被铁甲图标遮挡)
    protected override string CharacterSelectIconPath => "res://zhao/images/char_select/zhao_select_icon.png";
    protected override string CharacterSelectLockedIconPath => "res://zhao/images/char_select/zhao_select_locked.png";
    public override string CharacterSelectSfx => "event:/sfx/characters/ironclad/ironclad_select";

    public override List<string> GetArchitectAttackVfx() => new()
    {
        "vfx/vfx_attack_blunt",
        "vfx/vfx_heavy_blunt",
        "vfx/vfx_attack_slash",
        "vfx/vfx_bloody_impact",
        "vfx/vfx_rock_shatter",
    };

    // ---------- 卡池/遗物/药水 ----------
    public override CardPoolModel CardPool => ModelDb.CardPool<ZhaoCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<ZhaoRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<ZhaoPotionPool>();

    protected override CharacterModel? UnlocksAfterRunAs => null;

    // ---------- 初始卡组(规格第37节,共12张) ----------
    public override IEnumerable<CardModel> StartingDeck => new CardModel[]
    {
        ModelDb.Card<KitsuneFireStrike>(),   // 1× 狐火打击
        ModelDb.Card<LightCard>(),           // 1× 照小姐就是我们的光！
        ModelDb.Card<SectionIntro>(),        // 1× セクション(段落)·イントロ(前奏)
        ModelDb.Card<EmergencyTreatment>(),  // 1× 紧急治疗
        ModelDb.Card<StrikeIronclad>(),      // 4× 打击(本体原版)
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<DefendIronclad>(),      // 4× 防御(本体原版)
        ModelDb.Card<DefendIronclad>(),
        ModelDb.Card<DefendIronclad>(),
        ModelDb.Card<DefendIronclad>(),
    };

    // 起始遗物:狐之火(用户设计:战斗开始时获得2层狐火)
    public override IReadOnlyList<RelicModel> StartingRelics => new[]
    {
        ModelDb.Relic<FoxFireRelic>(),
    };
}
