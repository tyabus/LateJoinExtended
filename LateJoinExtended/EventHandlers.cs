using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using LabApi.Features.Extensions;
using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;
using Respawning.Objectives;
using System;
using System.Collections.Generic;
using System.Linq;
using UserSettings.ServerSpecific;

namespace LateJoinExtended;
public class EventHandlers
{
    static private readonly Random random = new Random();
    static private List<string> LateJoinIds = new List<string>();
    static private DateTime roundStartTime = DateTime.MinValue;
    private static Config _config => LateJoinExtended.Instance.Config;

    public static void UnregisterEvents()
    {
        ServerEvents.RoundStarted -= OnRoundStarted;
        PlayerEvents.Joined -= OnPlayerJoin;
    }

    public static void RegisterEvents()
    {
        ServerEvents.RoundStarted += OnRoundStarted;
        PlayerEvents.Joined += OnPlayerJoin;
    }

    static private void OnRoundStarted()
    {
        roundStartTime = DateTime.UtcNow;

        if(!_config.IsAllowedToLateJoinMultipleTimes)
            LateJoinIds.Clear();
    }

    static private void OnPlayerJoin(PlayerJoinedEventArgs ev)
    {
        if(!_config.IsEnabled)
            return;

        if(_config.SSSEnable)
            ServerSpecificSettingsSync.SendToPlayer(ev.Player.ReferenceHub);

        if(!Round.IsRoundInProgress)
            return;

        Timing.CallDelayed(_config.LateJoinDelay, () =>
        {
            HandleLateJoin(ev.Player);
        });
    }

    static public void HandleLateJoin(Player plr)
    {
        if (plr == null || plr.IsDestroyed)
            return;

        if (plr.IsAlive || !plr.IsPlayer)
            return;

        if (plr.Role == RoleTypeId.Overwatch)
            return;

        if (ServerSpecificSettingsSync.GetSettingOfUser<SSTwoButtonsSetting>(plr.ReferenceHub, _config.SSSKey).SyncIsB)
            return;

        if (LateJoinIds.Contains(plr.UserId) && !_config.IsAllowedToLateJoinMultipleTimes)
            return;

        double TotalSeconds = (DateTime.UtcNow - roundStartTime).TotalSeconds;
        if(Convert.ToInt32(TotalSeconds) < _config.LateJoinLimitSeconds) // round it up
        {
            RoleTypeId desiredRole = PickWeightedRole();
            string text = $"You joined {TotalSeconds:F1}s late.\nYou have been spawned as a <color={desiredRole.GetRoleColor().ToHex()}>{desiredRole.GetFullName()}</color>.";
            plr.SetRole(desiredRole, RoleChangeReason.LateJoin);
            plr.SendBroadcast(text, (ushort)_config.BroadcastTime, Broadcast.BroadcastFlags.Normal, false);
            if( !_config.IsAllowedToLateJoinMultipleTimes)
                LateJoinIds.Add(plr.UserId);
        }
    }

    static private RoleTypeId PickWeightedRole()
    {
        Dictionary<RoleTypeId, double> weightedRoles = _config.DesiredRoles;
        double num = random.NextDouble() * weightedRoles.Values.Sum();

        foreach (KeyValuePair<RoleTypeId, double> weightedRole in weightedRoles)
        {
            num -= weightedRole.Value;
            if (num <= 0.0)
            {
                return weightedRole.Key;
            }
        }
        return weightedRoles.Keys.First();
    }
}