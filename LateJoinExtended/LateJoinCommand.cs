using CommandSystem;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;

namespace LateJoinExtended;

[CommandHandler(typeof(ClientCommandHandler))]
public class LateJoinCmd : ICommand
{
    public string Command => "latejoin";

    public string[] Aliases { get; } = [""];

    public string Description => "Attempts to late join.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        Player player = Player.Get(sender);

        if (player == null || player.IsDestroyed)
        {
            response = "Invalid Player!";
            return false;
        }

        if (!LateJoinExtended.Instance.Config.IsLateJoinCommandEnabled)
        {
            response = "Command is disabled by server.";
            return false;
        }

        if (!LateJoinExtended.Instance.Config.IsEnabled)
        {
            response = "Plugin is disabled.";
            return false;
        }

        if (!Round.IsRoundInProgress)
        {
            response = "No round in progress.";
            return false;
        }

        if (player.IsAlive || player.Role == RoleTypeId.Overwatch)
        {
            response = "You cannot late join at this moment.";
            return false;
        }

        RoleTypeId OldRole = player.Role;

        EventHandlers.HandleLateJoin(player);

        if(OldRole != player.Role)
            response = "Successfully late joined.";
        else
            response = "You cannot late join at this moment.";

        return true;
    }
}