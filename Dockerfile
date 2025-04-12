
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=release
WORKDIR /src
COPY ["ReceiptsAPI.csproj", "."]
RUN dotnet restore "./ReceiptsAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ReceiptsAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=release
RUN dotnet publish "ReceiptsAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReceiptsAPI.dll"]
