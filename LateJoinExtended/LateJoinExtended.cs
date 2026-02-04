using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;
using System;

namespace LateJoinExtended;

public class LateJoinExtended : Plugin<Config>
{
    public static LateJoinExtended Instance { get; private set; } = null!;

    public override string Name => "LateJoinExtended";

    public override string Author => "tyabus";

    public override string Description => "Late joins player to a specified class";

    public override Version Version => new Version(1, 0, 2);

    public override Version RequiredApiVersion => new Version(LabApiProperties.CompiledVersion);

    public override void Enable()
    {
        Instance = this;

        if( GameCore.ConfigFile.ServerConfig.GetInt("late_join_time") > 0 )
            Logger.Warn("late_join_time is not 0, consider changing it in config_gameplay.txt");

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