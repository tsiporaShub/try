# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["webApi/WEBAPI.csproj", "webApi/"]
COPY ["bll/bll.csproj", "bll/"]
COPY ["dal/dal.csproj", "dal/"]

RUN dotnet restore "./webApi/WEBAPI.csproj"
COPY . .
WORKDIR "/src/webApi"
RUN dotnet build "./webApi.csproj" -c %BUILD_CONFIGURATION% -o /app/build --force

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WEBAPI.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false --force

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WEBAPI.dll"]

# Add user-related configuration commands
RUN net user /add ContainerUser
USER ContainerUser