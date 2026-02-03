using System.Collections.Generic;
using UserSettings.ServerSpecific;

namespace LateJoinExtended;
public class SSSHandler
{
    private static Config _config => LateJoinExtended.Instance.Config;

    static public void OnEnable()
    {
        ServerSpecificSettingBase[] SSS = new ServerSpecificSettingBase[]
        {
            new SSGroupHeader("LateJoinExtended"),
            new SSTwoButtonsSetting(_config.SSSKey, "Late Join", "Enable", "Disable", false, "Opt-In/Out of the late join system.")
        };

        if (ServerSpecificSettingsSync.DefinedSettings == null || ServerSpecificSettingsSync.DefinedSettings.Length <= 0)
        {
            ServerSpecificSettingsSync.DefinedSettings = SSS;
        }
        else
        {
            List<ServerSpecificSettingBase> newSSS = new List<ServerSpecificSettingBase>(ServerSpecificSettingsSync.DefinedSettings);
            newSSS.AddRange(SSS);
            ServerSpecificSettingsSync.DefinedSettings = newSSS.ToArray();
        }

        ServerSpecificSettingsSync.SendToAll();
    }

    static public void OnDisable()
    {

    }
}
