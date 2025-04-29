# Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files (only the .csproj file for restoring dependencies)
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining files and build the application in Release mode
COPY . ./
WORKDIR /src
RUN dotnet publish -c Release -o /app

# Create a runtime image for running the application
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /app ./

# Set the entry point to start the application
ENTRYPOINT ["dotnet", "CarInsuranceTelegramBot.dll"]



