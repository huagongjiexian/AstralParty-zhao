照 0.0.17.8 — 战斗闪退隔离测试版

变更：
1. 彻底移除战斗场景 IntroPlayer/Show 自动播放链。
2. 运行时 kitsune_frames 只包含 Idle 和 Attack。
3. Show 90 帧原图仅保存在本源码包 zhao/art/kitsune/Show/，未打入运行 PCK。
4. Idle = 10 FPS；Attack = 40 FPS；每帧 duration = 1.0。
5. 镜像方向 flip_h=true，位置 Vector2(8.6,-148.8)。
6. ZhaoCombatAnimation 的攻击回调机制本版本不改：该机制在已知稳定的 0.0.16.7 中已经存在，因此先隔离 0.0.17.4 起新增的入场链。
7. DLL AssemblyVersion/文件版本/manifest 统一为 0.0.17.8。

测试重点：进入战斗后使用快速重开，观察是否仍出现 name_changed/JNI Fatal。
