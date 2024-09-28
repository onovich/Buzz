# Buzz
Buzz: A Lightweight Vibration Calculation Library for Controllers<br/>
**Buzz，轻量级的手柄震动计算库，取名自“鸣”。**

It creates separate vibration tasks for both low-frequency and high-frequency motors in controllers. Each task supports delayed activation, duration control, and frequency gradients based on easing functions.<br/>
**Buzz 为手柄的低频马达和高频马达分别创建震动任务，每个任务支持延时触发、时长控制、基于缓动函数的频率渐变曲线控制。**

The core lifecycle is responsible only for frequency calculations, while the upper layer applies the results as needed, without inversion of control.<br/>
**底层生命周期只负责频率计算，上层获取计算结果自行应用，无控制反转。**

Buzz supports creating tasks for single motors, multiple motors, and task groups.<br/>
**支持创建单马达任务、多马达任务，支持创建任务组。**

When multiple tasks run simultaneously, frequency blending is not implemented (as it tends to produce meaningless noise). Instead, the current strategy allows later tasks to interrupt earlier ones on the same motor.<br/>
**当多个任务同时运行时，没做频率融合（我认为融合效果不佳，只会得到无意义的噪音），目前采用的是在同一马达上后触发的任务打断先触发的任务的策略。**

The project includes configuration examples and runtime demonstrations.<br/>
**项目内提供了配置方案示例，以及运行时示例。**

# Readiness
Stable and available.<br/>
**稳定可用。**
