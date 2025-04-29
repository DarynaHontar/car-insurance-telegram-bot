namespace CarInsuranceTelegramBot
{
    /// <summary>
    /// Represents the different states a user can be in during the car insurance process.
    /// </summary>
    public enum UserState
    {
        /// <summary>
        /// Default state when no interaction has taken place yet.
        /// </summary>
        None,

        /// <summary>
        /// State when the bot is waiting for the user to upload documents.
        /// </summary>
        WaitingForDocuments,

        /// <summary>
        /// State when the bot is waiting for the user to confirm or reject the extracted document data.
        /// </summary>
        WaitingForConfirmation,

        /// <summary>
        /// State when the user has confirmed the extracted data as correct.
        /// </summary>
        Confirmed,

        /// <summary>
        /// State when the user has accepted the fixed price for the insurance policy.
        /// </summary>
        PriceAccepted
    }
}

