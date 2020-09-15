using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base_Discord_Bot.Main.Controller
{
    public class CommandHandler : ModuleBase<SocketCommandContext>
    {
        private DiscordSocketClient _bot;
        private CommandService _service;
        private IServiceProvider _services;
        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _bot = client;
            _service = new CommandService();

            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

            _bot.MessageReceived += HandleCommandAsync;
        }
        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;

            var context = new SocketCommandContext(_bot, msg);

            int argPos = 0;
            if (msg.HasStringPrefix("-", ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    await Context.Channel.SendMessageAsync("Unknown Command please");
                }
            }
        }
    }
}
