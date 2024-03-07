FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Auth.Server/Auth.Server.csproj", "Auth.Server/"]
RUN dotnet restore "./Auth.Server/Auth.Server.csproj"
COPY . .
WORKDIR "/src/Auth.Server"
RUN dotnet build "./Auth.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Auth.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
VOLUME /app/Files
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.Server.dll"]