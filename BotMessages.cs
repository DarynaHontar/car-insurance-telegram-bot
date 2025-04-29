namespace CarInsuranceTelegramBot
{
    /// <summary>
    /// Contains the predefined messages sent by the bot to users.
    /// These messages include responses for various stages of the insurance process.
    /// </summary>
    public static class BotMessages
    {
        /// <summary>
        /// Message sent to the user when they start the conversation with the bot.
        /// </summary>
        public const string StartMessage = "Hello! 👋 I will help you purchase car insurance. Please send me photos of your passport and car document.";

        /// <summary>
        /// Prompt message sent to the user after they confirm the extracted data.
        /// </summary>
        public const string ConfirmPrompt = "✅ Data confirmed. The insurance cost is 100$. Do you agree? Type /acceptprice or /declineprice.";

        /// <summary>
        /// Message asking the user to send the photos again if they were not clear or valid.
        /// </summary>
        public const string RetryPrompt = "Please send the photos again 📷";

        /// <summary>
        /// Message informing the user that the insurance policy has been generated.
        /// </summary>
        public const string PolicyGenerated = "Your insurance policy has been generated 📄";

        /// <summary>
        /// Caption for the insurance policy document sent to the user.
        /// </summary>
        public const string PolicyCaption = "Here is your insurance policy.";

        /// <summary>
        /// Message informing the user that the insurance is only available at the fixed price of 100$.
        /// </summary>
        public const string DeclineMessage = "Unfortunately, the insurance is available only at the fixed price of 100$. If you want to proceed, type /acceptprice.";

        /// <summary>
        /// Message sent when the user sends an unknown command.
        /// </summary>
        public const string UnknownCommand = "Please use the commands or send documents.";

        /// <summary>
        /// Message sent when the bot is waiting for confirmation or a command, not a new photo.
        /// </summary>
        public const string UnexpectedPhoto = "I am currently waiting for confirmation or a command, not new photos.";
    }
}
