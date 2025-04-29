using Telegram.Bot.Exceptions;
using Telegram.Bot;

namespace CarInsuranceTelegramBot
{
    /// <summary>
    /// Provides error handling functionality for Telegram bot polling.
    /// </summary>
    public static class ErrorService
    {
        /// <summary>
        /// Handles errors that occur during the polling process.
        /// Logs the error details to the console.
        /// </summary>
        /// <param name="botClient">The Telegram bot client instance.</param>
        /// <param name="exception">The exception that occurred.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A completed Task.</returns>
        public static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error: {apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }
    }
}
