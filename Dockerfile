FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster as build
WORKDIR /src
COPY . .
WORKDIR /src
RUN dotnet restore
RUN dotnet publish --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "omission.api.dll"]