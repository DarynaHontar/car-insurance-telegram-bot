# Use the official .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build the app
COPY . ./
RUN dotnet publish -c Release -o out

# Use the ASP.NET Core runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "CarInsuranceTelegramBot.dll"]
