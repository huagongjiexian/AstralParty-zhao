照模组 0.0.16.7 源码包

来源：从当前实际发布的 0.0.16.7 zhao.dll + zhao.pck 使用 GDRE Tools 2.6.0 恢复，确保与当前测试版本一致。

当前巫女动画参数：
- Idle：30 帧，18 FPS，duration=1.0
- Attack：60 帧，20 FPS，duration=1.0
- 战斗场景位置：Vector2(8.6, -148.8)

主要目录：
- Code/：模组 C# 代码
- Code/Patches/：Harmony / 动画适配等代码
- Code/FoxFire/：狐火独立资源代码
- Code/Forms/：四形态系统
- Code/Cards/：卡牌代码
- scenes/：Godot 场景
- zhao/art/：动画与美术源资源
- zhao/images/：其他图片资源
- zhao/localization/：本地化
- zhao/video/：视频资源
- zhao.csproj / zhao.sln：C# 工程

注意：
GDRE 报告中有两个脚本无法从二进制恢复完整正文：
- Code/Powers/ZhaoPowers.cs
- src/Core/Nodes/Combat/NCreatureVisuals.cs
这两个文件在恢复目录中保留为占位；Code/ExportStubs/NCreatureVisualsExportStub.cs 保留了对应导出兼容桩。

编译时仍需要游戏本体的 sts2.dll、0Harmony 以及 Godot 4.5.1 Mono/.NET 环境。
