using System;
using System.Linq;
using System.Reflection;
using Dalamud.Bindings.ImGui;
using Dalamud.IoC;
using Dalamud.Plugin;

namespace ViewportKeeper;

public sealed class Plugin : IDalamudPlugin
{
    private const BindingFlags InstanceMembers =
        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

    [PluginService] private static IDalamudPluginInterface PluginInterface { get; set; } = null!;

    private object? backend;
    private EventInfo? newRenderFrameEvent;
    private Delegate? newRenderFrameHandler;
    private bool disposed;

    public Plugin()
    {
        PluginInterface.UiBuilder.Draw += TryAttach;
        TryAttach();
    }

    public void Dispose()
    {
        disposed = true;
        PluginInterface.UiBuilder.Draw -= TryAttach;

        if (backend is not null && newRenderFrameEvent is not null && newRenderFrameHandler is not null)
            newRenderFrameEvent.RemoveEventHandler(backend, newRenderFrameHandler);

        backend = null;
        newRenderFrameEvent = null;
        newRenderFrameHandler = null;
    }

    private void TryAttach()
    {
        if (disposed || newRenderFrameHandler is not null)
            return;

        try
        {
            var uiBuilder = PluginInterface.UiBuilder;
            var uiBuilderType = uiBuilder.GetType();
            var managerField = uiBuilderType.GetField("interfaceManager", InstanceMembers)
                ?? uiBuilderType.GetFields(InstanceMembers).FirstOrDefault(field =>
                    field.FieldType.FullName == "Dalamud.Interface.Internal.InterfaceManager");
            var manager = managerField?.GetValue(uiBuilder);
            if (manager is null)
                return;

            var managerType = manager.GetType();
            backend = managerType.GetProperty("Backend", InstanceMembers)?.GetValue(manager)
                ?? managerType.GetField("backend", InstanceMembers)?.GetValue(manager);
            if (backend is null)
                return;

            newRenderFrameEvent = backend.GetType().GetEvent("NewRenderFrame", InstanceMembers);
            if (newRenderFrameEvent?.EventHandlerType is null)
                return;

            var callback = GetType().GetMethod(nameof(OnNewRenderFrame), InstanceMembers);
            if (callback is null)
                return;

            newRenderFrameHandler = Delegate.CreateDelegate(
                newRenderFrameEvent.EventHandlerType,
                this,
                callback,
                true);
            newRenderFrameEvent.AddEventHandler(backend, newRenderFrameHandler);
            PluginInterface.UiBuilder.Draw -= TryAttach;
        }
        catch
        {
            backend = null;
            newRenderFrameEvent = null;
            newRenderFrameHandler = null;
        }
    }

    private void OnNewRenderFrame()
    {
        var io = ImGui.GetIO();
        io.ConfigFlags |= ImGuiConfigFlags.ViewportsEnable;
    }
}
