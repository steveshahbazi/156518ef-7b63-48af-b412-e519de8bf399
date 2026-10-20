
# Stage 1: Build and publish
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src
COPY ["Directory.Build.props", "./"]

# Copy project files first to improve Docker layer caching.
COPY ["src/IncreasingSequence.Core/IncreasingSequence.Core.csproj", "src/IncreasingSequence.Core/"]
COPY ["src/IncreasingSequence.Console/IncreasingSequence.Console.csproj", "src/IncreasingSequence.Console/"]

# Restore dependencies.
RUN dotnet restore "src/IncreasingSequence.Console/IncreasingSequence.Console.csproj"

# Copy the application source.
COPY ["src/IncreasingSequence.Core/", "src/IncreasingSequence.Core/"]
COPY ["src/IncreasingSequence.Console/", "src/IncreasingSequence.Console/"]

# Publish the console application.
RUN dotnet publish "src/IncreasingSequence.Console/IncreasingSequence.Console.csproj" \
    --configuration Release \
    --output /app/publish \
    --no-restore

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/runtime:10.0 AS final

WORKDIR /app

# Run as the built-in non-root user.
USER $APP_UID

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "IncreasingSequence.Console.dll"]
