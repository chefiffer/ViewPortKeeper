# Viewport Keeper

[English](README.md) | [简体中文](README.zh-CN.md)

Viewport Keeper continuously enables Dear ImGui multi-viewport support in Dalamud, allowing compatible plugin windows to move outside the FINAL FANTASY XIV game window.

> [!IMPORTANT]
> Viewport Keeper only enables the shared ImGui capability. It cannot force a window to detach if the target plugin itself does not support multi-viewport behavior. Because the plugin connects to Dalamud's internal rendering backend, a major Dalamud update may require installing a corresponding compatible version.

## Features

- Restores `ImGuiConfigFlags.ViewportsEnable` before each frame begins rendering.
- Retries safely while Dalamud's rendering backend is not yet ready.
- Fully detaches its event handler when the plugin is unloaded.
- Provides no commands or settings page, and does not access the network, collect telemetry, or store persistent data.

## Installation

1. Install and start XIVLauncher, and make sure Dalamud is enabled.
2. Open **Dalamud Settings → Experimental**.
3. Add the following custom plugin repository URL:

   ```text
   https://raw.githubusercontent.com/chefiffer/ViewPortKeeper/main/repo.json
   ```

4. Open the Plugin Installer, search for **Viewport Keeper**, and select **Install**.

## Usage

Viewport Keeper has no separate window or command. After the plugin is loaded, simply drag a compatible Dalamud plugin window beyond the edge of the game client.

If the window still cannot move outside the game client, it may be pinned to the main viewport, or the target plugin itself may not support ImGui multi-viewports.

## Troubleshooting

### No visible change after loading the plugin

This is expected. Viewport Keeper has no visible interface of its own; its effect can only be observed when a compatible window is dragged outside the game client.

### A window still cannot move outside the client

- Confirm that Viewport Keeper is loaded.
- Try another plugin window known to support ImGui multi-viewports.
- Check whether the target plugin deliberately pins its window to the main viewport.
- After a Dalamud update, install a Viewport Keeper version compatible with the current API level.

## Contributing

When reporting a compatibility issue, include the Dalamud API version and whether any compatible window can move outside the client. Linux reports should also include the distribution, desktop session, Wine/Proton version, and XIVLauncher.Core version. Do not upload account credentials or unrelated game logs.

## License

Viewport Keeper is licensed under the GNU Affero General Public License v3.0. See [LICENSE](LICENSE).
