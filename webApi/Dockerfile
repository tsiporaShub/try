FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["webApi/webApi.csproj", "webApi/"]
COPY ["bll/bll.csproj", "bll/"]
COPY ["dal/dal.csproj", "dal/"]

RUN dotnet restore "./webApi/webApi.csproj"
COPY . .
WORKDIR "/src/webApi"
RUN dotnet build "./webApi.csproj" -c $BUILD_CONFIGURATION -o /app/build --force

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./webApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --force

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "webApi.dll"]
