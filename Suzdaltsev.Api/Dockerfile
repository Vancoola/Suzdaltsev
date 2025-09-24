FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Suzdaltsev.Api/Suzdaltsev.Api.csproj", "Suzdaltsev.Api/"]
COPY ["Suzdaltsev.Data/Suzdaltsev.Data.csproj", "Suzdaltsev.Data/"]
COPY ["Suzdaltsev.Business/Suzdaltsev.Business.csproj", "Suzdaltsev.Business/"]
COPY ["Suzdaltsev.Domain/Suzdaltsev.Domain.csproj", "Suzdaltsev.Domain/"]
RUN dotnet restore "Suzdaltsev.Api/Suzdaltsev.Api.csproj"
COPY . .
WORKDIR "/src/Suzdaltsev.Api"
RUN dotnet build "./Suzdaltsev.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Suzdaltsev.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Suzdaltsev.Api.dll"]
