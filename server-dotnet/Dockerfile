FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BugTracker.API.csproj", "./"]
RUN dotnet restore "BugTracker.API.csproj"
COPY . .
RUN dotnet build "BugTracker.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BugTracker.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BugTracker.API.dll"] 