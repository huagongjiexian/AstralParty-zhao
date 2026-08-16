// ============================================================================
// 导出期占位脚本(Export-time stub,NOT the real NCreatureVisuals):
// zhao.tscn 根节点引用了游戏本体的 C# 脚本 res://src/Core/Nodes/Combat/NCreatureVisuals.cs。
// Godot 导出器在把 .tscn 转成二进制 .scn 时会解析 ext_resource;
// 本工程内没有该文件会导致导出器把 script 引用丢弃(0.0.5 已验证),
// 因此放置此占位文件,让导出器能解析路径并把 script 引用完整写入导出产物。
//
// 运行时行为与游戏本体完全一致:
//  - 游戏中该路径由游戏本体程序集(sts2.dll)的 C# 脚本路径映射解析,指向真正的 NCreatureVisuals;
//  - 模组程序集不会被注册进 Godot 的脚本路径映射,本占位类不会被当作场景脚本使用。
// 类名刻意与本体不同,避免任何按类名反射/查找的歧义。
// ============================================================================
using Godot;

namespace Zhao.ExportStubs;

public partial class NCreatureVisualsExportStub : Node2D
{
}
