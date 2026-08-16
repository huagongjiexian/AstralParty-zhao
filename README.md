# 照 · Zhao

> 拥有四种形态的少女。以巫女（狐火）、小护士（治愈）、歌姬（段落）、淑女（光）编织属于自己的战斗旋律。

「照」是《杀戮尖塔 2》（Slay the Spire 2）的一名原创角色模组。她能在四种形态之间自由切换，每一种形态都对应一套独立的战斗节奏：

| 形态 | 核心机制 | 玩法定位 |
|---|---|---|
| 巫女 | 狐火 | 基础形态，围绕「狐火」层层累积、滚起雪球 |
| 小护士 | 治愈 | 回合开始回复、回合结束衰减的续航流派 |
| 歌姬 | 段落 | 前奏 → 主歌 → 副歌 → 间奏 → 尾声，按节奏推进 |
| 淑女 | 光 | 积攒「光」，一次性倾泻打出爆发 |

## 下载与安装

最新打包版见 [Releases](https://github.com/huagongjiexian/AstralParty-zhao/releases)（`zhao-0.0.15.zip`），也可直接取仓库 `发布/` 目录下的三个文件。

1. 解压得到 `zhao.dll`、`zhao.json`、`zhao.pck` 三个文件
2. 放入游戏目录的 `mods\zhao\`（不存在则新建）
3. 启动游戏，在角色选择界面选择「照」

## 基本信息

| 项目 | 值 |
|---|---|
| 模组 id | `zhao` |
| 版本 | `0.0.15` |
| 作者 | 喵小照 |
| 目标游戏 | 《Slay the Spire 2》（杀戮尖塔 2）`0.107.1` |

## 卡牌

狐火打击 · 照小姐就是我们的光！ · 段落·前奏 · 紧急治疗 · 段落·主歌 · 段落·副歌 · 尾声 · 光よ！ · 追击追击 · 快进

## 目录结构

| 目录 / 文件 | 内容 |
|---|---|
| `Code/` | C# 模组源码（`[ModInitializer]` 入口 + Harmony 补丁） |
| `zhao/` | Godot 资源：美术 `art/`、图标 `images/`、本地化 `localization/`、视频 `video/` |
| `scenes/` | Godot 场景（角色视觉、界面、角色选择画面） |
| `src/` | 辅助脚本 |
| `发布/` | 交付物 `zhao.dll` / `zhao.json` / `zhao.pck` |
| `project.godot` / `zhao.csproj` / `zhao.sln` | Godot + .NET 工程文件 |
| `build_checks.ps1` | 打包前静态检查脚本 |

## 构建

```powershell
dotnet build zhao.csproj
```

重新导出 `zhao.pck`：

```powershell
megadot --headless --path . --export-pack "BasicExport" "发布\zhao.pck"
```

> `megadot` 为与游戏本体一致的 MegaDot（Godot 4.5）控制台可执行文件。

打包前静态检查：

```powershell
powershell -File build_checks.ps1
```

## 依赖

- Godot 4.5（MegaDot，与游戏本体一致）
- .NET SDK 9
