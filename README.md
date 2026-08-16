# 照 (Zhao) — 《杀戮尖塔2》四形态角色模组

《Slay the Spire 2》(杀戮尖塔2) 的角色模组「照」，一位拥有四种形态的小狐狸：

| 形态 | 定位 |
|---|---|
| 巫女 | 狐火（基础形态） |
| 小护士 | 治愈 |
| 歌姬 | 段落（前奏 / 主歌 / 副歌 / 间奏 / 尾声） |
| 淑女 | 光 |

- 模组 id：`zhao`
- 版本：`0.0.15`（`zhao.json` 中为 0.0.15）
- 作者：喵小照
- 目标游戏版本：`0.107.1`

## 目录结构

| 目录 / 文件 | 内容 |
|---|---|
| `Code/` | C# 模组源码（`[ModInitializer]` 入口 + Harmony 补丁） |
| `zhao/` | Godot 资源：美术 (`art/`)、图标 (`images/`)、本地化 (`localization/eng|zhs`)、视频 (`video/`) |
| `scenes/` | Godot 场景（角色视觉 `creature_visuals/`、界面 `ui/`、角色选择 `screens/char_select/`） |
| `src/` | 辅助脚本 |
| `project.godot` / `zhao.csproj` / `zhao.sln` | Godot + .NET 工程文件 |
| `export_presets.cfg` | Godot 导出预设（导出 `zhao.pck`） |
| `build_checks.ps1` | 打包前静态检查脚本 |

> `发布/`（交付物 `zhao.dll` / `zhao.json` / `zhao.pck`）与 `.godot/`（生成缓存）不进入版本库。完整交付包请从发布渠道获取。

## 安装

1. 解压发布包（`zhao.dll` / `zhao.json` / `zhao.pck`）。
2. 将三个文件放入游戏目录的 `mods\zhao\` 文件夹（不存在则新建）。

## 构建

```powershell
dotnet build D:\deepseek\00-模组-照\zhao.csproj
```

重新导出 `zhao.pck`：

```powershell
D:\deepseek\04-工具链\megadot\MegaDot_v4.5.1-stable_mono_win64_console.exe --headless --path D:\deepseek\00-模组-照 --export-pack "BasicExport" "<目标路径>\zhao.pck"
```

打包前运行静态检查：

```powershell
powershell -File D:\deepseek\00-模组-照\build_checks.ps1
```

## 依赖

- Godot 4.5（与游戏本体一致的 MegaDot 4.5.1）
- .NET SDK 9
