namespace CarInsuranceTelegramBot
{
    /// <summary>
    /// Represents a session for a user interacting with the car insurance bot.
    /// </summary>
    public class UserSession
    {
        /// <summary>
        /// The current state of the user in the insurance process.
        /// Defaults to None when the session starts.
        /// </summary>
        public UserState State { get; set; } = UserState.None;

        /// <summary>
        /// The extracted data from the user's uploaded documents.
        /// This may include the user's name, vehicle number, etc., after processing the documents.
        /// </summary>
        public string? ExtractedData { get; set; }
    }
}
