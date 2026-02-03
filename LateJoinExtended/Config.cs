using PlayerRoles;
using System.Collections.Generic;
using System.ComponentModel;

namespace LateJoinExtended;

public sealed class Config
{
    [Description("Enable/disable LateJoinExtended")]
    public bool IsEnabled { get; set; } = true;

    [Description("Allow multiple late joins by the same person?")]
    public bool IsAllowedToLateJoinMultipleTimes { get; set; } = false;

    [Description("Enable/disable Server Side Settings")]
    public bool SSSEnable { get; set; } = true;

    [Description("Server Side Settings Key ID")]
    public int SSSKey { get; set; } = 1998;

    [Description("How many seconds into the round should we allow late joining?")]
    public int LateJoinLimitSeconds { get; set; } = 60;

    [Description("How many seconds we should show the late join message?")]
    public int BroadcastTime { get; set; } = 6;

    [Description("How many seconds later should we try late join?")]
    public float LateJoinDelay { get; set; } = 2.5f;

    [Description("The roles which will be used as a roster for the late join and their weight")]
    public Dictionary<RoleTypeId, double> DesiredRoles { get; set; } = new()
    {
        {
            RoleTypeId.ClassD,
            45.0
        },
        {
            RoleTypeId.Scientist,
            20.0
        },
        {
            RoleTypeId.FacilityGuard,
            50.0
        },
        {
            RoleTypeId.ChaosConscript,
            2.0
        },
        {
            RoleTypeId.NtfPrivate,
            2.0
        },
    };
}