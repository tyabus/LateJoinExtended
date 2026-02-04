# LateJoinExtended
LateJoinExtended is a LabAPI plugin that replaces & extends the vanilla late join system.

## Requirements
- Make sure the config option in `config_gameplay.txt` named `late_join_time` is set to 0.

## Releases and Installation
You can find the latest release [here](https://github.com/tyabus/LateJoinExtended/releases).
Once downloaded, place the LateJoinExtended.dll file into the /LabAPI/plugins/<port> folder and restart your server.

## Configs
| Config option | Value type | Default value | Description |
| --- | --- | --- | --- |
| `IsEnabled` | bool | true | Enables the plugin. Set it to false to disable it. |
| `IsAllowedToLateJoinMultipleTimes` | bool | false | Allows multiple late joins by the same person |
| `IsLateJoinCommandEnabled` | bool | true | Allows player to late join by command |
| `SSSEnable` | bool | true | Enable/disable Server Side Settings |
| `SSSKey` | int | 1998 | Unique Server Side Settings key ID |
| `LateJoinLimitSeconds` | int | 60 | How many seconds should late join be open |
| `BroadcastTime` | int | 6 | Time in seconds that will late join message will be shown |
| `LateJoinDelay` | float | 2.5f | How many seconds after joining should we try late join |
| `DesiredRoles` | Dictionary<RoleTypeId, double> | Check config | The roles which will be used as a roster for the late join and their weight |
