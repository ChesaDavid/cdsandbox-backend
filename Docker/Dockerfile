FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish -c Relese -o logout

FROM mcr.microsoft.com/dotnet/appnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","YourBackendApp.dll"]