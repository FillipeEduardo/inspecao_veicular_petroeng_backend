FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY src/BuildingBlocks ./src/BuildingBlocks
COPY src/Services/InspecaoVeicularPetroeng.API ./src/Services/InspecaoVeicularPetroeng.API

RUN dotnet restore ./src/Services/InspecaoVeicularPetroeng.API/InspecaoVeicularPetroeng.API.csproj

RUN dotnet build ./src/Services/InspecaoVeicularPetroeng.API/InspecaoVeicularPetroeng.API.csproj -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish ./src/Services/InspecaoVeicularPetroeng.API/InspecaoVeicularPetroeng.API.csproj -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "InspecaoVeicularPetroeng.API.dll"]
