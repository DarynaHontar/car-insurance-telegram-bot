using Telegram.Bot;
using Telegram.Bot.Polling;

namespace CarInsuranceTelegramBot
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var botToken = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");
                var openAiKey = "openAiKey";

                var botClient = new TelegramBotClient(botToken);

                var receiverOptions = new ReceiverOptions
                {
                    AllowedUpdates = { } // Receive all update types
                };

                // Initialize services
                var sessionManager = new SessionManager();
                var mindeeService = new MindeeService();
                var openAI = new OpenAIService(openAiKey!);

                // Start receiving updates
                botClient.StartReceiving(
                    updateHandler: (bot, update, token) => BotService.HandleUpdateAsync(bot, update, sessionManager, mindeeService, openAI),
                    pollingErrorHandler: ErrorService.HandlePollingErrorAsync,
                    receiverOptions: receiverOptions
                );

                var me = await botClient.GetMeAsync();
                Console.WriteLine($"Bot {me.Username} started...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
