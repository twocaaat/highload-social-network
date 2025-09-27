FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HighloadSocialNetwork.WebServer/HighloadSocialNetwork.WebServer.csproj", "HighloadSocialNetwork.WebServer/"]
RUN dotnet restore "HighloadSocialNetwork.WebServer/HighloadSocialNetwork.WebServer.csproj"
COPY . .
WORKDIR "/src/HighloadSocialNetwork.WebServer"
RUN dotnet build "./HighloadSocialNetwork.WebServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HighloadSocialNetwork.WebServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HighloadSocialNetwork.WebServer.dll"]
