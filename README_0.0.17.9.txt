照 0.0.17.9

当前巫女运行时动作：
- Show：90 帧，50 FPS，非循环，镜像。
- Idle：30 帧，20 FPS，循环，镜像。
- Attack：60 帧，40 FPS，非循环，镜像。

入场实现：
AnimatedSprite2D 直接 autoplay="Show"。Show 播放完成后，场景自身把 animation_finished 信号连接到同一个 AnimatedSprite2D 的 play("Idle")。
没有 IntroPlayer、没有 GDScript、没有额外静态回调容器。

位置：Vector2(8.6, -148.8)
flip_h=true
