using LabApi.Features;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;
using System;

namespace LateJoinExtended;

public class LateJoinExtended : Plugin
{
    public static LateJoinExtended Instance { get; private set; } = null!;

    public Config Config { get; private set; } = null!;

    public override string Name => "LateJoinExtended";

    public override string Author => "tyabus";

    public override string Description => "Late joins player to a specified class";

    public override Version Version => new Version(1, 0, 1);

    public override Version RequiredApiVersion => new Version(LabApiProperties.CompiledVersion);

    public override void LoadConfigs()
    {
        Config = this.TryLoadConfig("config.yml", out Config? config)
            ? config
            : new Config();
    }

    public override void Enable()
    {
        Instance = this;
        EventHandlers.RegisterEvents();

        if( Config.SSSEnable )
            SSSHandler.OnEnable();
    }

    public override void Disable()
    {
        EventHandlers.UnregisterEvents();
        if( Config.SSSEnable )
            SSSHandler.OnDisable();
        Instance = null!;
    }
}