# Car Insurance Telegram Bot

## 1. Setup Instructions and Dependencies

### Required dependencies:
- **.NET 6.0 or newer version**
  To run the bot, you need to have the latest version of .NET SDK installed.
- **Telegram.Bot** — library for working with Telegram API.
      dotnet add package Telegram.Bot
- **OpenAI_API** — library for integrating with OpenAI.    
      dotnet add package OpenAI_API
- **Microsoft.Extensions.Configuration** — library for accessing and managing application settings in .NET.
   dotnet add package Microsoft.Extensions.Configuration
- **Microsoft.Extensions.Configuration.Json** — library for reading configuration settings from a JSON file (appsettings.json).
  dotnet add package Microsoft.Extensions.Configuration.Json

### Setup Steps:
  1.Clone or download the repository.
  2.Install dependencies by running:
      dotnet restore
  3.Get Telegram Bot Token:
     Create a new bot via BotFather on Telegram and get the token
  4.Get OpenAI API key:
     Register on OpenAI and get an API key.
  5.Add these keys to project.
     - To ensure the security of your bot and connected services, do not hardcode sensitive tokens such as the Telegram Bot Token or OpenAI API Key directly into your source code. Instead, store them using environment variables. This approach prevents accidental exposure of credentials in version control systems (e.g., GitHub) and allows easier management across development, testing, and production environments.
    - Set environment variables in your operating system or deployment environment:
        TELEGRAM_BOT_TOKEN
        OPEN_AI_KEY
    - Read tokens in your application like this:
        var botToken = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");
        var openAiKey = Environment.GetEnvironmentVariable("OPEN_AI_KEY");
    6.Run the application:
      dotnet run

## 2. Detailed Description of the Bot Workflow
Initialization: When the bot is launched, it connects to Telegram API using the provided bot token.
Handling Commands: The bot responds to several commands such as /start, /confirm, /retry, /acceptprice, and /declineprice.
Processing Files: Users can send photos of documents. The bot processes them using the Mindee service to extract data.
Policy Generation: After confirming the data, the bot generates an insurance policy and sends it to the user.

## 3. Examples of Interaction Flows with the Bot

Scenario 1: Start Command
User: /start
Bot: "Hello! 👋 I will help you purchase car insurance. Please send a photo of your passport and car documents."

Scenario 2: Confirm Data
User: /confirm
Bot: "✅ Data confirmed. The cost of insurance is $100. Do you agree? Write /acceptprice or /declineprice."

Scenario 3: Generate Policy
User: /acceptprice
Bot: "Your insurance policy has been generated 📄."

Scenario 4: Unknown Command
User: Sends an unknown command.
Bot: "Please use commands or send documents."


## 4. Link to the telegram bot
t.me/BestCarInsurance_bot
