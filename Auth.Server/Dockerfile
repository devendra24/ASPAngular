FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS with-node
RUN apt-get update
RUN apt-get install curl
RUN curl -sL https://deb.nodesource.com/setup_20.x | bash
RUN apt-get -y install nodejs
RUN npm install -g @angular/cli

FROM with-node AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Auth.Server/Auth.Server.csproj", "Auth.Server/"]
COPY ["auth.client/auth.client.esproj", "auth.client/"]
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