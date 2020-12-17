#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["BillionMessagesQueuer/BillionMessagesQueuer.csproj", "BillionMessagesQueuer/"]
COPY ["BillionMessagesQueuer.Providers/BillionMessagesQueuer.Providers.csproj", "BillionMessagesQueuer.Providers/"]
COPY ["BillionMessagesQueuer.Core/BillionMessagesQueuer.Core.csproj", "BillionMessagesQueuer.Core/"]
RUN dotnet restore "BillionMessagesQueuer/BillionMessagesQueuer.csproj"
COPY . .
WORKDIR "/src/BillionMessagesQueuer"
RUN dotnet build "BillionMessagesQueuer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BillionMessagesQueuer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BillionMessagesQueuer.dll"]