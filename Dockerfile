FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HomeLink.InCleanHome.API/HomeLink.InCleanHome.API.csproj", "HomeLink.InCleanHome.API/"]
RUN dotnet restore "HomeLink.InCleanHome.API/HomeLink.InCleanHome.API.csproj"
COPY . .
WORKDIR "/src/HomeLink.InCleanHome.API"
RUN dotnet build "HomeLink.InCleanHome.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HomeLink.InCleanHome.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeLink.InCleanHome.API.dll"]
