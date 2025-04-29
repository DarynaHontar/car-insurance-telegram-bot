using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CarInsuranceTelegramBot
{
    /// <summary>
    /// This class handles incoming updates (messages and photos) from users in the Telegram bot.
    /// </summary>
    public static class BotService
    {
        /// <summary>
        /// Handles the update from Telegram (text or photo messages) and processes them accordingly.
        /// </summary>
        /// <param name="botClient">The Telegram bot client.</param>
        /// <param name="update">The update object containing the user's message.</param>
        /// <param name="sessionManager">The session manager for tracking user sessions.</param>
        /// <param name="mindeeService">The service for processing the documents sent by the user.</param>
        /// <param name="openAIService">The service for interacting with OpenAI's GPT for text-based responses.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task HandleUpdateAsync(
            ITelegramBotClient botClient,
            Update update,
            SessionManager sessionManager,
            MindeeService mindeeService,
            OpenAIService openAIService)
        {
            if (update.Message == null) return;

            var message = update.Message;
            var chatId = message.Chat.Id;
            var userSession = sessionManager.GetSession(chatId);

            switch (message.Type)
            {
                case MessageType.Text:
                    await HandleTextMessageAsync(botClient, message, userSession, openAIService);
                    break;

                case MessageType.Photo:
                    await HandlePhotoMessageAsync(botClient, message, userSession, mindeeService);
                    break;
            }
        }

        /// <summary>
        /// Handles text messages sent by the user, including commands like /start, /confirm, /retry, etc.
        /// </summary>
        /// <param name="botClient">The Telegram bot client.</param>
        /// <param name="message">The user's message.</param>
        /// <param name="userSession">The session data of the user.</param>
        /// <param name="openAIService">The service that interacts with OpenAI for text responses.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private static async Task HandleTextMessageAsync(
            ITelegramBotClient botClient,
            Message message,
            UserSession userSession,
            OpenAIService openAIService)
        {
            var chatId = message.Chat.Id;

            switch (message.Text)
            {
                case "/start":
                    await BotTextCommandHandler.HandleStartCommandAsync(botClient, chatId, userSession);
                    break;

                case "/confirm":
                    await BotTextCommandHandler.HandleConfirmCommandAsync(botClient, chatId, userSession);
                    break;

                case "/retry":
                    await BotTextCommandHandler.HandleRetryCommandAsync(botClient, chatId, userSession);
                    break;

                case "/acceptprice":
                    await BotTextCommandHandler.HandleAcceptPriceCommandAsync(botClient, chatId, userSession);
                    break;

                case "/declineprice":
                    await BotTextCommandHandler.HandleDeclinePriceCommandAsync(botClient, chatId);
                    break;

                default:
                    // Processing free text user messages
                    /*var openAIResponse = await openAIService.GetResponseAsync(message.Text);
                    await botClient.SendTextMessageAsync(chatId, openAIResponse);*/

                    //Using standard messages
                    await botClient.SendTextMessageAsync(chatId, BotMessages.UnknownCommand);
                    break;
            }
        }

        /// <summary>
        /// Handles photo messages sent by the user. Processes the document for extraction of data.
        /// </summary>
        /// <param name="botClient">The Telegram bot client.</param>
        /// <param name="message">The user's photo message.</param>
        /// <param name="userSession">The session data of the user.</param>
        /// <param name="mindeeService">The service that processes the photo document for data extraction.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private static async Task HandlePhotoMessageAsync(
            ITelegramBotClient botClient,
            Message message,
            UserSession userSession,
            MindeeService mindeeService)
        {
            var chatId = message.Chat.Id;

            if (userSession.State == UserState.WaitingForDocuments)
            {
                try
                {
                    using var stream = await DownloadPhotoAsync(botClient, message);
                    var extractedData = await ExtractDataAsync(mindeeService, stream);
                    UpdateUserSession(userSession, extractedData);
                    await botClient.SendTextMessageAsync(
                    chatId,
                    $"Personal info is:\n{extractedData}\n\n Confirm data (/confirm) or retry again (/retry).");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing photo: {ex.Message}");
                    await botClient.SendTextMessageAsync(chatId, "An error occurred while processing your photo. Please try again.");
                }
            }
            else
            {
                await botClient.SendTextMessageAsync(chatId, BotMessages.UnexpectedPhoto);
            }
        }

        /// <summary>
        /// Downloads the latest photo from the user's message into a memory stream.
        /// </summary>
        /// <param name="botClient">The Telegram bot client.</param>
        /// <param name="message">The incoming Telegram message containing the photo.</param>
        /// <returns>A <see cref="MemoryStream"/> containing the photo data.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the message does not contain a photo or file path is missing.</exception>
        private static async Task<MemoryStream> DownloadPhotoAsync(ITelegramBotClient botClient, Message message)
        {
            if (message.Photo == null)
                throw new InvalidOperationException("No photo attached to the message.");

            var fileId = message.Photo[^1].FileId;
            var file = await botClient.GetFileAsync(fileId);

            if (string.IsNullOrEmpty(file.FilePath))
                throw new InvalidOperationException("File path is missing.");

            var stream = new MemoryStream();
            await botClient.DownloadFileAsync(file.FilePath, stream);
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Extracts data from a document using the Mindee service.
        /// </summary>
        /// <param name="mindeeService">The Mindee service instance.</param>
        /// <param name="stream">The memory stream containing the document data.</param>
        /// <returns>Extracted data as a string. If extraction fails, returns a fallback message.</returns>
        private static async Task<string> ExtractDataAsync(MindeeService mindeeService, MemoryStream stream)
        {
            var extractedData = await mindeeService.ProcessDocumentAsync(stream.ToArray());
            return extractedData ?? "No data extracted.";
        }

        /// <summary>
        /// Updates the user's session with extracted data and changes the state to waiting for confirmation.
        /// </summary>
        /// <param name="userSession">The user's session object to update.</param>
        /// <param name="extractedData">The extracted data to store.</param>
        private static void UpdateUserSession(UserSession userSession, string extractedData)
        {
            userSession.ExtractedData = extractedData;
            userSession.State = UserState.WaitingForConfirmation;
        }
    }
}
