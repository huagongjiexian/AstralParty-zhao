照模组 0.0.17.6 源码/资源包

关键动画：
- Show（战斗入场/第一回合展示）：90帧，20 FPS，非循环，镜像显示。
- Idle：30帧，20 FPS，循环。
- Attack：60帧，23 FPS，非循环。
- 战斗位置：Vector2(8.6, -148.8)，flip_h=true。

新增文件：
- zhao/art/kitsune/Show/：90张展示动作原始 RGBA PNG。
- zhao/art/kitsune/zhao_battle_intro.gd：入场播放一次 Show，结束切回 Idle。
- 来源资料/展示动作/：用户提供动作包的动画信息和镜像场景参考。

注意：
Code/ 下的 C# 为当前 DLL 的恢复/反编译源码，其中仍包含 GDRE 反编译遗留的伪语法，不能把整棵 C# 工程视作未经整理即可重新编译的原始工程。本版没有重编整 DLL，以避免改变已验证可运行的逻辑；本版新增的 GDScript 和动画资源为完整可编辑源码。
