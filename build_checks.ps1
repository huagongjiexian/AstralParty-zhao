# Static integrity check for zhao mod 0.0.15 (run before packaging; no game testing)
$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$codeDir = Join-Path $root 'Code'
$locDir  = Join-Path $root 'zhao\localization'

$script:fail = 0
function Check([string]$label, [bool]$ok) {
    if ($ok) { Write-Output ("PASS  " + $label) } else { Write-Output ("FAIL  " + $label); $script:fail++ }
}

# Slugify (CamelCase -> UPPER_SNAKE), mirrors StringHelper.Slugify in 0.107.1
function Slugify([string]$name) {
    $s = $name -creplace '([a-z0-9])([A-Z])', '$1_$2'
    $s = $s.ToUpperInvariant() -replace '[^A-Z0-9_]', ''
    return $s
}

function ReadJson([string]$path) {
    $txt = [System.IO.File]::ReadAllText($path, [System.Text.Encoding]::UTF8)
    return ($txt | ConvertFrom-Json)
}

# 1. Model classes
$cardClasses = @(Select-String -Path (Join-Path $codeDir 'Cards\*.cs') -Pattern 'public sealed class (\w+) : (ZhaoCardModel|CardModel)' | ForEach-Object { $_.Matches[0].Groups[1].Value })
$powerClasses = @(Select-String -Path (Join-Path $codeDir 'Powers\*.cs') -Pattern 'public class (\w+) : (ZhaoFormPower|PowerModel)' | ForEach-Object { $_.Matches[0].Groups[1].Value } | Select-Object -Unique)
$charClasses  = @(Select-String -Path (Join-Path $codeDir 'Character\*.cs') -Pattern 'public class (\w+) : CharacterModel' | ForEach-Object { $_.Matches[0].Groups[1].Value })
$relicClasses = @(Select-String -Path (Join-Path $codeDir 'Relics\*.cs') -Pattern 'public sealed class (\w+) : RelicModel' | ForEach-Object { $_.Matches[0].Groups[1].Value })

Write-Output ("cards: " + ($cardClasses -join ', '))
Write-Output ("powers: " + ($powerClasses -join ', '))
Write-Output ("characters: " + ($charClasses -join ', '))

# 2. Localization keys
$zhsCards = ReadJson (Join-Path $locDir 'zhs\cards.json')
$engCards = ReadJson (Join-Path $locDir 'eng\cards.json')
$zhsPowers = ReadJson (Join-Path $locDir 'zhs\powers.json')
$engPowers = ReadJson (Join-Path $locDir 'eng\powers.json')
$zhsChars = ReadJson (Join-Path $locDir 'zhs\characters.json')
$engChars = ReadJson (Join-Path $locDir 'eng\characters.json')

$zhsCardKeys = @($zhsCards.PSObject.Properties | ForEach-Object { $_.Name })
$engCardKeys = @($engCards.PSObject.Properties | ForEach-Object { $_.Name })
$zhsPowerKeys = @($zhsPowers.PSObject.Properties | ForEach-Object { $_.Name })
$engPowerKeys = @($engPowers.PSObject.Properties | ForEach-Object { $_.Name })
$zhsCharKeys = @($zhsChars.PSObject.Properties | ForEach-Object { $_.Name })
$engCharKeys = @($engChars.PSObject.Properties | ForEach-Object { $_.Name })

foreach ($c in $cardClasses) {
    $slug = Slugify $c
    $okZ = ($zhsCardKeys -contains ($slug + '.title')) -and ($zhsCardKeys -contains ($slug + '.description'))
    $okE = ($engCardKeys -contains ($slug + '.title')) -and ($engCardKeys -contains ($slug + '.description'))
    Check ("card $slug title/desc zhs" ) $okZ
    Check ("card $slug title/desc eng" ) $okE
}
foreach ($p in $powerClasses) {
    $slug = Slugify $p
    $okZ = ($zhsPowerKeys -contains ($slug + '.title')) -and ($zhsPowerKeys -contains ($slug + '.description'))
    $okE = ($engPowerKeys -contains ($slug + '.title')) -and ($engPowerKeys -contains ($slug + '.description'))
    Check ("power $slug title/desc zhs") $okZ
    Check ("power $slug title/desc eng") $okE
}
foreach ($ch in $charClasses) {
    $slug = Slugify $ch
    $required = @('title','titleObject','description','pronounObject','possessiveAdjective','pronounPossessive','pronounSubject','cardsModifierTitle','cardsModifierDescription','eventDeathPrevention','unlockText','goldMonologue','aromaPrinciple','banter.alive.endTurnPing','banter.dead.endTurnPing')
    $missingZ = @(); $missingE = @()
    foreach ($k in $required) {
        if (-not ($zhsCharKeys -contains ($slug + '.' + $k))) { $missingZ += $k }
        if (-not ($engCharKeys -contains ($slug + '.' + $k))) { $missingE += $k }
    }
    Check ("character $slug required keys zhs") ($missingZ.Count -eq 0)
    if ($missingZ.Count -gt 0) { Write-Output ("        missing zhs: " + ($missingZ -join ', ')) }
    Check ("character $slug required keys eng") ($missingE.Count -eq 0)
    if ($missingE.Count -gt 0) { Write-Output ("        missing eng: " + ($missingE -join ', ')) }
}

# 3. eng/zhs key parity
Check "cards eng/zhs key sets identical" ((($engCardKeys | Sort-Object) -join '|') -eq (($zhsCardKeys | Sort-Object) -join '|'))
Check "powers eng/zhs key sets identical" ((($engPowerKeys | Sort-Object) -join '|') -eq (($zhsPowerKeys | Sort-Object) -join '|'))
Check "characters eng/zhs key sets identical" ((($engCharKeys | Sort-Object) -join '|') -eq (($zhsCharKeys | Sort-Object) -join '|'))

# 4. StartingDeck referenced models exist
$zhaoCharFile = Join-Path $codeDir 'Character\ZhaoCharacter.cs'
$cardRefs = @(Select-String -Path $zhaoCharFile -Pattern 'ModelDb\.Card<(\w+)>' | ForEach-Object { $_.Matches[0].Groups[1].Value } | Select-Object -Unique)
foreach ($r in $cardRefs) {
    $exists = ($r -in $cardClasses) -or ($r -in @('StrikeIronclad','DefendIronclad'))
    Check "deck model $r exists" $exists
}

# 5. Relic situation: FoxFireRelic must exist and be the starting relic
$zhsRelics = ReadJson (Join-Path $locDir 'zhs\relics.json')
$engRelics = ReadJson (Join-Path $locDir 'eng\relics.json')
$zhsRelicKeys = @($zhsRelics.PSObject.Properties | ForEach-Object { $_.Name })
$engRelicKeys = @($engRelics.PSObject.Properties | ForEach-Object { $_.Name })
foreach ($r in $relicClasses) {
    $slug = Slugify $r
    $okZ = ($zhsRelicKeys -contains ($slug + '.title')) -and ($zhsRelicKeys -contains ($slug + '.description')) -and ($zhsRelicKeys -contains ($slug + '.flavor'))
    $okE = ($engRelicKeys -contains ($slug + '.title')) -and ($engRelicKeys -contains ($slug + '.description')) -and ($engRelicKeys -contains ($slug + '.flavor'))
    Check "relic $slug title/desc/flavor zhs" $okZ
    Check "relic $slug title/desc/flavor eng" $okE
}
Check "relics eng/zhs key sets identical" ((($engRelicKeys | Sort-Object) -join '|') -eq (($zhsRelicKeys | Sort-Object) -join '|'))
$zhaoCharText = Get-Content $zhaoCharFile -Raw
Check "StartingRelics references FoxFireRelic (>=1 starting relic)" ($zhaoCharText -match 'ModelDb\.Relic<FoxFireRelic>')
# Relic must belong to ZhaoRelicPool (vanilla rule: RelicModel.Pool = First(pool whose AllRelicIds contains relic id))
$zhaoRelicPoolText = Get-Content (Join-Path $codeDir 'Character\ZhaoRelicPool.cs') -Raw
Check "ZhaoRelicPool contains FoxFireRelic" ($zhaoRelicPoolText -match 'ModelDb\.Relic<FoxFireRelic>')

# ================= 0.0.5 checks =================

# 6. CardPool membership: every concrete Zhao CardModel must resolve CardModel.Pool
#    (0.107.1: Pool = First(AllCardPools with AllCardIds containing id); falling through to the
#    MockCardPool probe throws "You monster!" because MockCardPool.GenerateAllCards() -> MockCanonical()
#    -> NeverEverCallThisOutsideOfTests_ClearOwner() throws when TestMode.IsOff.)
$zhaoPoolText = Get-Content (Join-Path $codeDir 'Character\ZhaoCardPool.cs') -Raw
$poolCards = @(Select-String -Path (Join-Path $codeDir 'Character\ZhaoCardPool.cs') -Pattern 'ModelDb\.Card<(\w+)>' | ForEach-Object { $_.Matches[0].Groups[1].Value } | Select-Object -Unique)
$modEntryText = Get-Content (Join-Path $codeDir 'ModEntry.cs') -Raw
$tokenCards = @(Select-String -Path (Join-Path $codeDir 'ModEntry.cs') -Pattern 'AddModelToPool<TokenCardPool,\s*(\w+)>' | ForEach-Object { $_.Matches[0].Groups[1].Value } | Select-Object -Unique)
$pooledAll = @($poolCards) + @($tokenCards)
Write-Output ("zhao pool cards: " + ($poolCards -join ', '))
Write-Output ("token pool cards: " + ($tokenCards -join ', '))
foreach ($c in $cardClasses) {
    Check "card $c has legal pool (ZhaoCardPool or TokenCardPool)" ($c -in $pooledAll)
}
# Starting deck cards must all be in ZhaoCardPool (drawn cards hit Pool during NCard.Create)
foreach ($r in $cardRefs) {
    if ($r -notin @('StrikeIronclad','DefendIronclad')) {
        Check "starting deck card $r in ZhaoCardPool" ($r -in $poolCards)
    }
}
# Transform-only cards must be Token rarity and registered to vanilla TokenCardPool
foreach ($t in $tokenCards) {
    $tokFile = Join-Path $codeDir ("Cards\$t.cs")
    $tokText = Get-Content $tokFile -Raw
    Check "$t rarity is Token" ($tokText -match 'CardRarity\.Token')
}
Check "no card references MockCardPool (code-level)" (-not ($zhaoPoolText -match 'MockCardPool[>\(]'))
Check "no card file references MockCardPool (code-level)" (-not ((Get-ChildItem (Join-Path $codeDir 'Cards') -Filter *.cs | ForEach-Object { Get-Content $_.FullName -Raw }) -match 'MockCardPool[>\(]'))

# 7. Combat visuals scene (0.107.1 vanilla structure: Node2D root + NCreatureVisuals script +
#    %Visuals Node2D + %Bounds Control + %CenterPos/%IntentPos Marker2D)
$scenePath = Join-Path $root 'scenes\creature_visuals\zhao.tscn'
$sceneText = Get-Content $scenePath -Raw
Check "zhao.tscn exists" (Test-Path $scenePath)
Check "zhao.tscn root is Node2D named Zhao" ($sceneText -match '\[node name="Zhao" type="Node2D"\]')
Check "zhao.tscn root has NCreatureVisuals script" ($sceneText -match 'script = ExtResource\("1_ncv"\)')
Check "zhao.tscn script ext_resource is vanilla NCreatureVisuals.cs" ($sceneText -match 'path="res://src/Core/Nodes/Combat/NCreatureVisuals\.cs"')
Check "zhao.tscn has %Visuals unique node" ($sceneText -match 'name="Visuals"[^\n]*[\r\n]|name="Visuals"')
Check "zhao.tscn Visuals is AnimatedSprite2D" ($sceneText -match '\[node name="Visuals" type="AnimatedSprite2D"')
Check "zhao.tscn Visuals unique_name_in_owner" ($sceneText -match 'unique_name_in_owner = true')
Check "zhao.tscn has Bounds/CenterPos/IntentPos" (($sceneText -match 'name="Bounds" type="Control"') -and ($sceneText -match 'name="CenterPos" type="Marker2D"') -and ($sceneText -match 'name="IntentPos" type="Marker2D"'))
Check "zhao.tscn references kitsune_frames.tres" ($sceneText -match 'res://zhao/art/kitsune/kitsune_frames\.tres')
Check "kitsune_frames.tres exists" (Test-Path (Join-Path $root 'zhao\art\kitsune\kitsune_frames.tres'))
Check "export stub for NCreatureVisuals.cs exists (export-time script resolution)" (Test-Path (Join-Path $root 'src\Core\Nodes\Combat\NCreatureVisuals.cs'))
# CharacterModel.VisualsPath patch must point at this scene
$visPatchText = Get-Content (Join-Path $codeDir 'Patches\ZhaoVisualPatch.cs') -Raw
Check "ZhaoVisualPatch VisualsPath -> creature_visuals/zhao" ($visPatchText -match 'get_VisualsPath"\] = SceneHelper\.GetScenePath\("creature_visuals/zhao"\)')

# 8. Relic icon paths: must be res://zhao/... AND import chain complete (.import sidecar + ctex on disk)
$relicFile = Join-Path $codeDir 'Relics\FoxFireRelic.cs'
$relicText = Get-Content $relicFile -Raw
$iconPaths = @([regex]::Matches($relicText, '=> "([^"]+\.png)"') | ForEach-Object { $_.Groups[1].Value } | Select-Object -Unique)
foreach ($ip in $iconPaths) {
    $rel = $ip -replace '^res://',''
    $disk = Join-Path $root ($rel -replace '/','\')
    $sidecar = $disk + '.import'
    Check "relic icon path $ip exists on disk" (Test-Path $disk)
    Check "relic icon sidecar $rel.import exists" (Test-Path $sidecar)
    if (Test-Path $sidecar) {
        $sc = Get-Content $sidecar -Raw
        $ctexRel = [regex]::Match($sc, 'path(?:\.\w+)?="res://\.godot/imported/([^"]+\.ctex)"').Groups[1].Value
        if ($ctexRel -eq '') { $ctexRel = [regex]::Match($sc, 'dest_files=\["res://\.godot/imported/([^"]+\.ctex)"\]').Groups[1].Value }
        Check "relic icon import chain: ctex exists for $rel" (($ctexRel -ne '') -and (Test-Path (Join-Path $root ('.godot\imported\' + $ctexRel))))
    }
    Check "relic icon path uses zhao namespace" ($ip -like 'res://zhao/*')
}
Check "no relic path uses bare res://images/ prefix" (-not ($relicText -match '"res://images/'))

# 9. Version unification: everything 0.0.15, game stays 0.107.1
$ver = '0.0.15'
$jsonText = Get-Content (Join-Path $root 'zhao.json') -Raw
Check "zhao.json version = $ver" ($jsonText.Contains('"version": "0.0.15"'))
Check "zhao.json min_game_version = 0.107.1" ($jsonText -match '"min_game_version":\s*"0\.107\.1"')
Check "zhao.json id/has_pck/has_dll" (($jsonText -match '"id":\s*"zhao"') -and ($jsonText -match '"has_pck":\s*true') -and ($jsonText -match '"has_dll":\s*true'))
$csprojText = Get-Content (Join-Path $root 'zhao.csproj') -Raw
Check "zhao.csproj Version = $ver" ($csprojText.Contains('<Version>0.0.15</Version>'))
$pgText = Get-Content (Join-Path $root 'project.godot') -Raw
Check "project.godot config/version = $ver" ($pgText.Contains('config/version="0.0.15"'))
$epText = Get-Content (Join-Path $root 'export_presets.cfg') -Raw
Check "export_presets file/product version = $ver" (($epText.Contains('application/file_version="0.0.15"')) -and ($epText.Contains('application/product_version="0.0.15"')))

# ================= 0.0.6 checks: 狐火特殊资源重构 =================

# 10. 狐火不再是 Power/Buff:不得再出现 KitsuneFirePower(类、Apply、GetPowerAmount、ModifyAmount、Remove)
$allCodeText = (Get-ChildItem $codeDir -Recurse -Filter *.cs | ForEach-Object { Get-Content $_.FullName -Raw }) -join "`n"
Check "KitsuneFirePower fully removed (no class/apply/query)" (-not ($allCodeText -match 'KitsuneFirePower'))
Check "powers.json has no KITSUNE_FIRE_POWER key" (-not ((Get-Content (Join-Path $locDir 'zhs\powers.json') -Raw) -match 'KITSUNE_FIRE_POWER'))

# 11. 狐火特殊资源体系文件齐备(0.107.1 星辉同架构)
Check "FoxFireResource exists" (Test-Path (Join-Path $codeDir 'FoxFire\FoxFireResource.cs'))
Check "FoxFireBank exists" (Test-Path (Join-Path $codeDir 'FoxFire\FoxFireBank.cs'))
Check "FoxFireCmd exists" (Test-Path (Join-Path $codeDir 'FoxFire\FoxFireCmd.cs'))
Check "ZhaoFoxFireCombatHooks exists" (Test-Path (Join-Path $codeDir 'FoxFire\ZhaoFoxFireCombatHooks.cs'))
Check "NFoxFireCounter exists" (Test-Path (Join-Path $codeDir 'FoxFire\NFoxFireCounter.cs'))
Check "ModEntry subscribes combat hooks for foxfire payment" ($modEntryText -match 'SubscribeForCombatStateHooks')
$hooksText = Get-Content (Join-Path $codeDir 'FoxFire\ZhaoFoxFireCombatHooks.cs') -Raw
Check "FoxFire payment in BeforeCardPlayed (IsFirstInSeries once)" (($hooksText -match 'BeforeCardPlayed\(CardPlay') -and ($hooksText -match 'IsFirstInSeries') -and ($hooksText -match 'FoxFireCmd\.Spend'))
Check "FoxFire payment skips non-Zhao cards" ($hooksText -match 'is not ZhaoCardModel')

# 12. 卡牌迁移:全部走 ZhaoCardModel + FoxFireCmd
$baseModelText = Get-Content (Join-Path $codeDir 'Cards\ZhaoCardModel.cs') -Raw
Check "ZhaoCardModel defines FoxFireCost (mirrors CanonicalStarCost)" ($baseModelText -match 'public virtual int FoxFireCost')
Check "ZhaoCardModel gates IsPlayable by foxfire (mirrors HasEnoughResourcesFor)" ($baseModelText -match 'FoxFireCmd\.Get\(Owner\) < FoxFireCost')
Check "ZhaoCardModel transforms after play via Played event (no mid-play transform)" (($baseModelText -match 'Played \+= OnPlayedFinalize') -and ($baseModelText -match 'OnTransformAfterPlay'))
foreach ($c in $cardClasses) {
    $cardText = Get-Content (Join-Path $codeDir ("Cards\$c.cs")) -Raw
    Check "card $c extends ZhaoCardModel" ($cardText -match "class $c : ZhaoCardModel")
}
$chaseText = Get-Content (Join-Path $codeDir 'Cards\ChaseChase.cs') -Raw
Check "ChaseChase declares FoxFireCost (pay via vanilla-style pipeline)" ($chaseText -match 'public override int FoxFireCost')
Check "ChaseChase no longer manually consumes foxfire in OnPlay" (-not ($chaseText -match 'ModifyAmount'))
$kfsText = Get-Content (Join-Path $codeDir 'Cards\KitsuneFireStrike.cs') -Raw
Check "KitsuneFireStrike grants foxfire via FoxFireCmd.Gain" ($kfsText -match 'FoxFireCmd\.Gain')
$introText = Get-Content (Join-Path $codeDir 'Cards\SectionIntro.cs') -Raw
$mainText  = Get-Content (Join-Path $codeDir 'Cards\SectionMain.cs') -Raw
Check "SectionIntro transform deferred to after-play (OnTransformAfterPlay)" ($introText -match 'OnTransformAfterPlay\(\) => TransformHelper\.TransformInto<SectionMain>')
Check "SectionMain transform deferred to after-play (OnTransformAfterPlay)" ($mainText -match 'OnTransformAfterPlay\(\) => TransformHelper\.TransformInto<SectionChorus>')
Check "no card transforms itself inside OnPlay (old await pattern removed)" (-not ($allCodeText -match 'await TransformHelper\.TransformInto'))
$relicText2 = Get-Content (Join-Path $codeDir 'Relics\FoxFireRelic.cs') -Raw
Check "FoxFireRelic grants 2 foxfire via FoxFireCmd.Gain (not Power)" ($relicText2 -match 'FoxFireCmd\.Gain\(2, base\.Owner\)')
Check "FormSystem uses FoxFireCmd (entry/leave kitsune)" ((Get-Content (Join-Path $codeDir 'Forms\FormSystem.cs') -Raw) -match 'FoxFireCmd\.(Gain|Lose|Get)')
Check "ZhaoPowers uses FoxFireCmd (form gain / outro settlement)" ((Get-Content (Join-Path $codeDir 'Powers\ZhaoPowers.cs') -Raw) -match 'FoxFireCmd\.(Gain|Lose|Get)')
Check "CombatEndCleanupPatch clears FoxFireBank at combat end" ((Get-Content (Join-Path $codeDir 'Patches\CombatEndCleanupPatch.cs') -Raw) -match 'FoxFireBank\.ClearCombat\(\)')

# 13. 狐火 UI:独立计数器(不是 Power 图标),由 ZhaoCombatUiPatch 挂载
Check "ZhaoCombatUiPatch mounts NFoxFireCounter after Activate" ((Get-Content (Join-Path $codeDir 'Patches\ZhaoCombatUiPatch.cs') -Raw) -match 'NFoxFireCounter\.Create\(me\)')
Check "NFoxFireCounter subscribes AmountChanged (mirrors StarsChanged)" ((Get-Content (Join-Path $codeDir 'FoxFire\NFoxFireCounter.cs') -Raw) -match 'AmountChanged \+= OnAmountChanged')
Check "foxfire counter icon exists on disk" (Test-Path (Join-Path $root 'zhao\images\foxfire\foxfire_icon.png'))

# 14. 出牌悬浮修复:转化必须推迟到 Played(结果堆移动)之后,不得在 OnPlay 内转化当前卡
Check "TransformHelper comment forbids mid-play self transform" ((Get-Content (Join-Path $codeDir 'Cards\TransformHelper.cs') -Raw) -match 'Played')
# 0.0.6 优化强化检查:
#  - 订阅必须放在 AfterCreated(本体 DeepCloneFields 会清空克隆体事件,构造订阅在战斗克隆上失效)
Check "ZhaoCardModel subscribes Played in AfterCreated (clone-safe)" (($baseModelText -match 'override void AfterCreated') -and ($baseModelText -match 'Played \+= OnPlayedFinalize'))
Check "ZhaoCardModel does NOT subscribe Played in constructor" (-not ($baseModelText -match 'protected ZhaoCardModel[\s\S]{0,200}?Played \+='))
#  - 转化继承升级走本体公开命令 CardCmd.Upgrade(原版 Charge 先例),不再用 TemporaryUpgrade
$transformText2 = Get-Content (Join-Path $codeDir 'Cards\TransformHelper.cs') -Raw
Check "TransformHelper inherits upgrades via CardCmd.Upgrade" ($transformText2 -match 'CardCmd\.Upgrade\(r\.cardAdded, CardPreviewStyle\.None\)')
Check "TransformHelper no longer uses TemporaryUpgrade" (-not ($transformText2 -match 'TemporaryUpgrade'))
#  - 用户通则:狐火只解锁强化效果;主歌分支/离开巫女的追击在狐火为0时跳过
Check "SectionMain chase gated on foxfire (user rule)" (($mainText -match 'if \(FoxFireCmd\.Get\(player\) > 0\)') -and ($mainText -match 'PursuitExecutor\.Chase'))
Check "LeaveKitsuneForm chase gated on foxfire" ((Get-Content (Join-Path $codeDir 'Forms\FormSystem.cs') -Raw) -match 'if \(fire > 0\)')
#  - 间奏:玩家选择(本体 CardSelectCmd)+ 免费 AutoPlay;过滤 CanPlay 防止段落回退
$formSystemText2 = Get-Content (Join-Path $codeDir 'Forms\FormSystem.cs') -Raw
Check "EnterInterlude uses CardSelectCmd.FromCombatPile (player choice)" ($formSystemText2 -match 'CardSelectCmd\.FromCombatPile')
Check "EnterInterlude filters CanPlay (no section regression)" ($formSystemText2 -match 'CanPlay\(\)')
Check "EnterInterlude free AutoPlay" ($formSystemText2 -match 'CardCmd\.AutoPlay')
Check "interlude prompt key exists zhs/eng" (($zhsCharKeys -contains 'ZHAO_CHARACTER.interludeCardPrompt') -and ($engCharKeys -contains 'ZHAO_CHARACTER.interludeCardPrompt'))
#  - 淑女入口回能显式向下取整
Check "Lady entry energy uses explicit floor" ($formSystemText2 -match 'decimal\.Floor\(lightAfterGain / 2m\)')

# ================= 0.0.6b checks: 选角页轮播(4张透明底PNG,按1-2-3-4循环) =================

$carouselDir = Join-Path $root 'zhao\images\char_select_carousel'
foreach ($n in 1..4) {
    Check "carousel image $n.png exists" (Test-Path (Join-Path $carouselDir "$n.png"))
}
$carouselScene = Join-Path $root 'scenes\screens\char_select\char_select_bg_zhao.tscn'
$carouselSceneText = if (Test-Path $carouselScene) { Get-Content $carouselScene -Raw } else { '' }
Check "zhao char select bg scene exists" (Test-Path $carouselScene)
Check "carousel scene references 4 images in order 1-2-3-4" (
    ($carouselSceneText -match 'path="res://zhao/images/char_select_carousel/1\.png"') -and
    ($carouselSceneText -match 'path="res://zhao/images/char_select_carousel/2\.png"') -and
    ($carouselSceneText -match 'path="res://zhao/images/char_select_carousel/3\.png"') -and
    ($carouselSceneText -match 'path="res://zhao/images/char_select_carousel/4\.png"'))
Check "carousel animation key order 1-2-3-4" (
    ($carouselSceneText -match 'values": \[ExtResource\("1_img"\), ExtResource\("2_img"\), ExtResource\("3_img"\), ExtResource\("4_img"\)\]'))
Check "carousel animation loops (loop_mode = 1)" ($carouselSceneText -match 'loop_mode = 1')
Check "carousel animation does NOT autoplay (starts on selection, synced with button frame)" (-not ($carouselSceneText -match 'autoplay'))
Check "carousel timing synced with button frame (2.5s per image, 10s loop)" (($carouselSceneText -match 'PackedFloat32Array\(0, 2\.5, 5, 7\.5\)') -and ($carouselSceneText -match 'length = 10\.0'))
Check "carousel uses built-in nodes only (no mod C# script in scene)" (-not ($carouselSceneText -match 'script = ExtResource'))
Check "ZhaoVisualPatch CharacterSelectBg -> char_select_bg_zhao" ($visPatchText -match 'get_CharacterSelectBg"\] = SceneHelper\.GetScenePath\("screens/char_select/char_select_bg_zhao"\)')
Check "ZhaoVisualPatch no longer uses ironclad char select bg" (-not ($visPatchText -match 'char_select_bg_ironclad'))

# ================= 0.0.6c checks: 选角按钮框(4张竖版框图,选中时1-2-3-4循环) =================

$frameDir = Join-Path $root 'zhao\images\char_select_frame'
foreach ($n in 1..4) {
    Check "select button frame image $n.png exists" (Test-Path (Join-Path $frameDir "$n.png"))
}
$btnPatchText = Get-Content (Join-Path $codeDir 'Patches\ZhaoSelectButtonPatch.cs') -Raw
Check "ZhaoSelectButtonPatch exists and targets Init" ($btnPatchText -match 'NCharacterSelectButton\.Init')
Check "select button frame only for ZhaoCharacter" ($btnPatchText -match 'is not ZhaoCharacter')
Check "select button frame uses __args injection (name-match safe)" ($btnPatchText -match '__args\[0\]')
Check "select button frame cycles 1-2-3-4 (TrackInsertKey order)" (
    ($btnPatchText -match 'Frame1Path' -and $btnPatchText -match 'Frame2Path' -and $btnPatchText -match 'Frame3Path' -and $btnPatchText -match 'Frame4Path') -and
    ($btnPatchText -match '0f \* FrameDuration, GD\.Load<Texture2D>\(Frame1Path\)') -and
    ($btnPatchText -match '1f \* FrameDuration, GD\.Load<Texture2D>\(Frame2Path\)') -and
    ($btnPatchText -match '2f \* FrameDuration, GD\.Load<Texture2D>\(Frame3Path\)') -and
    ($btnPatchText -match '3f \* FrameDuration, GD\.Load<Texture2D>\(Frame4Path\)'))
Check "select button frame loops" ($btnPatchText -match 'LoopModeEnum\.Linear')
Check "select button frame starts carousel when selected (IsSelected)" ($btnPatchText -match 'button\.IsSelected')
Check "select button frame hides vanilla outlines (no double frame)" ($btnPatchText -match 'OutlineLocal' -and $btnPatchText -match 'OutlineRemote' -and $btnPatchText -match 'OutlineMixed')
Check "select button frame is Sprite2D (outside GUI input path, touch-safe)" ($btnPatchText -match 'ZhaoSelectFrame : Sprite2D')
# 0.0.6d: 默认第1张、选中后才轮播、与背景同步
Check "select button frame defaults to visible frame 1" ($btnPatchText -match 'Visible = true')
Check "select button frame animation does not autoplay" (-not ($btnPatchText -match 'Autoplay = "ZhaoFrame"'))
Check "select button frame syncs with bg carousel (FindBgCarouselPlayer)" ($btnPatchText -match 'FindBgCarouselPlayer')
Check "select button frame start/reset both players on selection edge" (($btnPatchText -match 'PlayBoth') -and ($btnPatchText -match 'ResetBoth'))
Check "select button frame targets its own texture" ($btnPatchText -match 'NodePath\("\.:texture"\)')
Check "select button animations use explicit names" (($btnPatchText -match 'PlayAnimation\(framePlayer, FrameAnimationName\)') -and ($btnPatchText -match 'PlayAnimation\(bgPlayer, BgAnimationName\)'))
Check "select button visual children ignore mouse input" (($btnPatchText -match 'IgnoreChildControls') -and ($btnPatchText -match 'MouseFilterEnum\.Ignore'))
Check "select button handles direct left click" (($btnPatchText -match 'SignalName\.GuiInput') -and ($btnPatchText -match 'button\.Select\(\)'))
# 0.0.6d: 移除铁甲战士图片占位(图标/遮罩/阴影不再遮挡选角框)
Check "select button frame hides vanilla icon/mask/shadow for Zhao" (($btnPatchText -match '%Icon') -and ($btnPatchText -match 'MarginContainer/Mask') -and ($btnPatchText -match '%Shadow'))
$zhaoCharText2 = Get-Content (Join-Path $codeDir 'Character\ZhaoCharacter.cs') -Raw
Check "ZhaoCharacter select icon no longer ironclad" (-not ($zhaoCharText2 -match 'char_select_ironclad'))
Check "ZhaoCharacter map marker no longer ironclad" (-not ($zhaoCharText2 -match 'map_marker_ironclad'))
Check "ZhaoCharacter icon scene no longer ironclad" (-not ($zhaoCharText2 -match 'character_icons/ironclad'))
Check "transparent select icon placeholder exists" (Test-Path (Join-Path $root 'zhao\images\char_select\zhao_select_icon.png'))
Check "character ui placeholder exists" (Test-Path (Join-Path $root 'zhao\images\ui\zhao_character_placeholder.png'))
Check "zhao character icon scene exists" (Test-Path (Join-Path $root 'scenes\ui\character_icons\zhao_icon.tscn'))
Check "ZhaoVisualPatch top-panel icons no longer ironclad" (-not ($visPatchText -match 'character_icon_ironclad'))
Check "ZhaoVisualPatch arm textures no longer ironclad" (-not ($visPatchText -match 'multiplayer_hand_ironclad'))

# 17. 巫女待机动画(用户提供30帧)已替换
$kitsuneTres = [System.IO.File]::ReadAllText((Join-Path $root 'zhao\art\kitsune\kitsune_frames.tres'), [System.Text.Encoding]::UTF8)
Check "kitsune Idle uses 0.05s frame duration" ((Select-String -LiteralPath (Join-Path $root 'zhao\art\kitsune\kitsune_frames.tres') -Pattern '"duration": 0\.05' -Quiet) -and (Select-String -LiteralPath (Join-Path $root 'zhao\art\kitsune\kitsune_frames.tres') -Pattern '"name": &"Idle",' -Quiet))
Check "kitsune Idle still 30 frames (new upload)" ((Select-String -Path (Join-Path $root 'zhao\art\kitsune\kitsune_frames.tres') -Pattern 'Idle/frame_' | Measure-Object).Count -eq 30)
Check "kitsune total frames now 240 (user 60-frame attack)" ((Select-String -Path (Join-Path $root 'zhao\art\kitsune\kitsune_frames.tres') -Pattern '^\[ext_resource' | Measure-Object).Count -eq 240)
Check "kitsune Attack is user 60-frame animation" ((Select-String -Path (Join-Path $root 'zhao\art\kitsune\kitsune_frames.tres') -Pattern 'Attack/frame_' | Measure-Object).Count -eq 60)
Check "kitsune Attack uses 33ms frames, no loop" ((Select-String -LiteralPath (Join-Path $root 'zhao\art\kitsune\kitsune_frames.tres') -Pattern '"loop": false,' -Quiet) -and (Select-String -LiteralPath (Join-Path $root 'zhao\art\kitsune\kitsune_frames.tres') -Pattern '"name": &"Attack",' -Quiet) -and (Select-String -LiteralPath (Join-Path $root 'zhao\art\kitsune\kitsune_frames.tres') -Pattern '"duration": 0\.0333' -Quiet))
$cardAnimPatchText = Get-Content (Join-Path $codeDir 'Patches\ZhaoCardAnimationPatch.cs') -Raw
Check "Zhao attack animation patch targets card play" ($cardAnimPatchText -match 'CardModel\.OnPlayWrapper')
Check "Zhao attack animation only accepts Attack cards" (($cardAnimPatchText -match 'Type != CardType\.Attack') -and ($cardAnimPatchText -match 'is not ZhaoCharacter'))
Check "Zhao attack animation returns to Idle" (($cardAnimPatchText -match 'AttackAnimation') -and ($cardAnimPatchText -match 'IdleAnimation') -and ($cardAnimPatchText -match 'AnimationFinished'))
$visSceneText = Get-Content (Join-Path $root 'scenes\creature_visuals\zhao.tscn') -Raw
Check "zhao.tscn Visuals centered (user tscn params)" ($visSceneText -match 'centered = true')
Check "zhao.tscn Visuals position mirrors MIRRORED tscn (-26.4, -148.8)" ($visSceneText -match 'position = Vector2\(-26\.4, -148\.8\)')
Check "zhao.tscn Visuals flip_h (mirrored idle)" ($visSceneText -match 'flip_h = true')
Check "zhao bg scene root ignores mouse (no click blocking)" ($carouselSceneText -match '\[node name="ZhaoBg" type="Control"\]\r?\nmouse_filter = 2')
Check "zhao.tscn Visuals nearest filter (user tscn params)" ($visSceneText -match 'texture_filter = 1')

# 15. 卡图/Power 图标占位(替代原版 card_atlas/power_atlas 缺失时的 BETA 占位)
Check "card portrait placeholder exists" (Test-Path (Join-Path $root 'zhao\images\cards\zhao_card_placeholder.png'))
Check "power icon placeholder exists" (Test-Path (Join-Path $root 'zhao\images\powers\zhao_power_placeholder.png'))
Check "ZhaoCardModel overrides PortraitPath/BetaPortraitPath to placeholder" (($baseModelText -match 'override string PortraitPath') -and ($baseModelText -match 'override string BetaPortraitPath'))
Check "ZhaoPowerIconPatch redirects Zhao.Powers icons to placeholder" ((Get-Content (Join-Path $codeDir 'Patches\ZhaoPowerIconPatch.cs') -Raw) -match 'zhao_power_placeholder\.png')

# 16. 歌姬视频定位:以角色战斗锚点为参考,不写死屏幕像素
$videoText = [System.IO.File]::ReadAllText((Join-Path $codeDir 'Forms\DivaVideoBackground.cs'), [System.Text.Encoding]::UTF8)
Check "DivaVideoBackground uses creature node anchor (GetCreatureNode)" (Select-String -LiteralPath (Join-Path $codeDir 'Forms\DivaVideoBackground.cs') -Pattern 'GetCreatureNode\(creature\)' -Quiet)
Check "DivaVideoBackground converts via canvas transform (GetGlobalTransform)" (Select-String -LiteralPath (Join-Path $codeDir 'Forms\DivaVideoBackground.cs') -Pattern 'GetGlobalTransform\(\)\.AffineInverse\(\)' -Quiet)
Check "DivaVideoBackground has no 1920x1080 hardcoded position" (-not ($videoText -match '1920|1080'))
Check "DivaVideoBackground keeps native size (no resize)" (-not ($videoText -match 'ExpandAspect|CustomMinimumSize'))

Write-Output "====================================================="
if ($script:fail -eq 0) { Write-Output "ALL CHECKS PASSED" } else { Write-Output ("FAILURES: " + $script:fail) }
