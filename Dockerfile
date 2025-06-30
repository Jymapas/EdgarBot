# 1. build
FROM mcr.microsoft.com/dotnet/sdk:9.0.301-noble-arm64v8 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# 2. runtime
FROM mcr.microsoft.com/dotnet/runtime:9.0.301-noble-arm64v8
WORKDIR /app

# Copy only builded files
COPY --from=build /app/out ./

# Run bot
ENTRYPOINT ["dotnet", "EdgarBot.dll"]
