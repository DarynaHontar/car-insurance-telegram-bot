using Telegram.Bot.Types;
using Telegram.Bot;

namespace CarInsuranceTelegramBot
{
    /// <summary>
    /// Handles text commands received by the Telegram bot.
    /// </summary>
    public static class BotTextCommandHandler
    {
        /// <summary>
        /// Handles the /start command by sending a welcome message and updating the session state.
        /// </summary>
        public static async Task HandleStartCommandAsync(ITelegramBotClient botClient, long chatId, UserSession session)
        {
            await botClient.SendTextMessageAsync(chatId, BotMessages.StartMessage);
            session.State = UserState.WaitingForDocuments;
        }

        /// <summary>
        /// Handles the /confirm command by updating the session state if the user is in the correct state.
        /// </summary>
        public static async Task HandleConfirmCommandAsync(ITelegramBotClient botClient, long chatId, UserSession session)
        {
            if (session.State == UserState.WaitingForConfirmation)
            {
                session.State = UserState.Confirmed;
                await botClient.SendTextMessageAsync(chatId, BotMessages.ConfirmPrompt);
            }
        }

        /// <summary>
        /// Handles the /retry command by resetting the session state to waiting for documents.
        /// </summary>
        public static async Task HandleRetryCommandAsync(ITelegramBotClient botClient, long chatId, UserSession session)
        {
            session.State = UserState.WaitingForDocuments;
            await botClient.SendTextMessageAsync(chatId, BotMessages.RetryPrompt);
        }

        /// <summary>
        /// Handles the /acceptprice command by generating and sending an insurance policy document if the user has confirmed.
        /// </summary>
        public static async Task HandleAcceptPriceCommandAsync(ITelegramBotClient botClient, long chatId, UserSession session)
        {
            if (session.State == UserState.Confirmed)
            {
                session.State = UserState.PriceAccepted;
                var policy = PolicyGenerator.GeneratePolicy(session);
                await botClient.SendTextMessageAsync(chatId, BotMessages.PolicyGenerated);
                await botClient.SendDocumentAsync(
                    chatId,
                    InputFile.FromStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(policy)), "policy.txt"),
                    caption: BotMessages.PolicyCaption
                );
            }
        }

        /// <summary>
        /// Handles the /declineprice command by notifying the user that the process has been declined.
        /// </summary>
        public static async Task HandleDeclinePriceCommandAsync(ITelegramBotClient botClient, long chatId)
        {
            await botClient.SendTextMessageAsync(chatId, BotMessages.DeclineMessage);
        }

        /// <summary>
        /// Handles unknown or unsupported commands by sending a default message.
        /// </summary>
        public static async Task HandleUnknownCommandAsync(ITelegramBotClient botClient, long chatId)
        {
            await botClient.SendTextMessageAsync(chatId, BotMessages.UnknownCommand);
        }
    }
}
