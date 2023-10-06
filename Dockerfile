FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.18 AS build
WORKDIR /app

COPY UserService.csproj .
COPY UserService.sln .
RUN dotnet restore

COPY . .
RUN dotnet build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine3.18 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "UserService.dll"]