FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GameRankPaymentSystem/GameRankPaymentSystem.csproj", "GameRankPaymentSystem/"]
RUN dotnet restore "GameRankPaymentSystem/GameRankPaymentSystem.csproj"
COPY . .
WORKDIR "/src/GameRankPaymentSystem"
RUN dotnet build "./GameRankPaymentSystem.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GameRankPaymentSystem.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameRankPaymentSystem.dll"]
