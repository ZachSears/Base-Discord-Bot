using Base_Discord_Bot.Main.Controller;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_Discord_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();

        }
        private DiscordSocketClient _client;
        private CommandHandler handler;

        public async Task MainAsync()
        {


            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            _client.Log += Log;

            _client = new DiscordSocketClient();



            new CommandHandler();
            await _client.LoginAsync(TokenType.Bot, "");
            await _client.StartAsync();


            handler = new CommandHandler();

            await handler.InitializeAsync(_client);

            await Task.Delay(-1);

        }


        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
    }
}
