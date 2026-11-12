# Viewport Keeper

[English](README.md) | [简体中文](README.zh-CN.md)

Viewport Keeper 用于持续启用 Dalamud 的 Dear ImGui 多视口支持，使兼容的插件窗口能够移出《最终幻想 XIV》游戏窗口。

> [!IMPORTANT]
> Viewport Keeper 只负责启用共享的 ImGui 能力。如果目标插件本身不支持多视口，Viewport Keeper 不能强制使它的窗口分离。插件需要连接 Dalamud 的内部渲染后端，因此 Dalamud 发生重大更新后，可能需要安装相应的兼容版本。

## 功能

- 在每一帧开始渲染前恢复 `ImGuiConfigFlags.ViewportsEnable`。
- 在 Dalamud 渲染后端尚未准备完成时安全重试。
- 卸载插件时完整解除事件处理器。
- 不提供命令和设置页，也不访问网络、收集遥测或保存持久数据。

## 安装

1. 安装并启动 XIVLauncher，确认 Dalamud 已启用。
2. 打开 **Dalamud 设置 → 测试版**。
3. 添加下面的自定义插件仓库地址：

   ```text
   https://raw.githubusercontent.com/chefiffer/ViewPortKeeper/main/repo.json
   ```

4. 打开插件安装器，搜索 **Viewport Keeper**，然后选择 **安装**。

## 使用

Viewport Keeper 没有独立窗口或命令。插件加载后，直接将兼容的 Dalamud 插件窗口拖出游戏客户端边缘即可。

如果窗口仍然无法移出游戏客户端，目标窗口可能被固定在主视口，或者目标插件本身不支持 ImGui 多视口。

## 故障排查

### 插件加载后没有明显变化

这是正常现象。Viewport Keeper 本身没有可见界面，只有在拖动兼容窗口离开游戏客户端时才能观察到效果。

### 窗口仍然无法移出客户端

- 确认 Viewport Keeper 已经加载。
- 尝试另一个已知支持 ImGui 多视口的插件窗口。
- 检查目标插件是否主动将窗口固定在主视口。
- Dalamud 更新后，请安装适配当前 API 等级的 Viewport Keeper 版本。

## 参与贡献

提交兼容性问题时，请说明 Dalamud API 版本，以及是否有任何兼容窗口能够移出客户端。Linux 报告还应包含发行版、桌面会话、Wine/Proton 版本和 XIVLauncher.Core 版本。请勿上传账户凭据或无关游戏日志。

## 许可证

Viewport Keeper 采用 GNU Affero General Public License v3.0。完整条款请参阅 [LICENSE](LICENSE)。
