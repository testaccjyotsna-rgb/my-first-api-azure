FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

USER app
FROM  mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["my-first-api-azure/my-first-api-azure.csproj", "my-first-api-azure/"]
RUN dotnet restore "my-first-api-azure/my-first-api-azure.csproj"
COPY . .
WORKDIR "/src/my-first-api-azure"
RUN dotnet build "my-first-api-azure.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "my-first-api-azure.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "my-first-api-azure.dll"]
