namespace CarInsuranceTelegramBot
{
    /// <summary>
    /// Manages user sessions, tracking and storing the session data for each user.
    /// </summary>
    public class SessionManager
    {
        private readonly Dictionary<long, UserSession> _sessions = new();

        /// <summary>
        /// Retrieves the session for a user identified by their chatId. 
        /// If the session does not exist, a new session is created.
        /// </summary>
        /// <param name="chatId">The unique identifier for the user's chat.</param>
        /// <returns>The user session associated with the specified chatId.</returns>
        public UserSession GetSession(long chatId)
        {
            if (!_sessions.ContainsKey(chatId))
            {
                _sessions[chatId] = new UserSession();
            }

            return _sessions[chatId];
        }
    }
}
