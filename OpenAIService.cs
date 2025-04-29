using OpenAI_API;
using OpenAI_API.Completions;

namespace CarInsuranceTelegramBot
{
    /// <summary>
    /// Service class for interacting with the OpenAI API to generate responses.
    /// </summary>
    public class OpenAIService
    {
        private readonly OpenAIAPI _api;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenAIService"/> class.
        /// </summary>
        /// <param name="apiKey">The API key to authenticate with the OpenAI API.</param>
        public OpenAIService(string apiKey)
        {
            _api = new OpenAIAPI(apiKey);
        }

        /// <summary>
        /// Asynchronously generates a response from OpenAI based on the provided user message.
        /// </summary>
        /// <param name="userMessage">The message from the user to be processed by OpenAI.</param>
        /// <returns>A string containing the generated response from OpenAI.</returns>
        /// <remarks>
        /// This method sends the user's message to the OpenAI API and receives a response.
        /// The response is processed and returned after removing any leading or trailing whitespace.
        /// </remarks>
        public async Task<string> GetResponseAsync(string userMessage)
        {
            var request = new CompletionRequest
            {
                Prompt = userMessage,
                MaxTokens = 150,
                Temperature = 0.7,
                TopP = 1.0,
            };

            var result = await _api.Completions.CreateCompletionAsync(request);

            return result.Completions[0].Text.Trim();
        }
    }
}
