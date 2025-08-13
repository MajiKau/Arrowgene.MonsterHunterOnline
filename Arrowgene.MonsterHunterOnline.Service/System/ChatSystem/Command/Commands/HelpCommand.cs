using System.Collections.Generic;

namespace Arrowgene.MonsterHunterOnline.Service.System.ChatSystem.Command.Commands
{
    /// <summary>
    /// Provides information about available commands
    /// </summary>
    public class HelpCommand : ChatCommand
    {
        private readonly Dictionary<string, ChatCommand> _commands;

        public HelpCommand(Dictionary<string, ChatCommand> commands)
        {
            _commands = commands;
        }

        public override AccountType Account => AccountType.User;
        public override string Key => "help";
        public override string HelpText => "usage: `/help` - Provides information about available commands";

        public override void Execute(string[] command, Client client, ChatMessage message, List<ChatMessage> responses)
        {
            foreach (var cmd in _commands)
            {
                string msg = cmd.Value.HelpText;
                ChatMessage response = ChatMessage.CommandMessage(client, msg);
                responses.Add(response);
            }
        }
    }
}