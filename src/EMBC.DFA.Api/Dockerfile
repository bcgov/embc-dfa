#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM registry.access.redhat.com/ubi8/dotnet-60-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EMBC.DFA.Api/EMBC.DFA.Api.csproj", "EMBC.DFA.Api/"]
RUN dotnet restore "EMBC.DFA.Api/EMBC.DFA.Api.csproj"
COPY . .
WORKDIR "/src/EMBC.DFA.Api"
RUN dotnet build "EMBC.DFA.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EMBC.DFA.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EMBC.DFA.Api.dll"]