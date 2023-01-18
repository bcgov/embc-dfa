FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENV DOTNET_gcServer=1
ARG VERSION
ENV VERSION=$VERSION

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# install diagnostics tools
RUN dotnet tool install --tool-path /tools dotnet-trace
RUN dotnet tool install --tool-path /tools dotnet-counters
RUN dotnet tool install --tool-path /tools dotnet-dump

WORKDIR /src
COPY ["EMBC.DFA.Api/EMBC.DFA.Api.csproj", "EMBC.DFA.Api/"]
COPY ["EMBC.Utilities/EMBC.Utilities.csproj", "EMBC.Utilities/"]
COPY ["EMBC.Tests.Integration.DFA.Api/EMBC.Tests.Integration.DFA.Api.csproj", "EMBC.Tests.Integration.DFA.Api/"]
COPY ["EMBC.DFA.sln", ".editorconfig", "./"]
RUN dotnet restore .
COPY . .

# run unit tests
RUN dotnet test -c Release

# build
FROM build AS publish
RUN dotnet publish "EMBC.DFA.Api/EMBC.DFA.Api.csproj" -c Release -o /app/publish --runtime linux-musl-x64 --no-self-contained

FROM base AS final
# copy diagnostics tools
WORKDIR /tools
COPY --from=build /tools .
# copy app
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EMBC.DFA.Api.dll"]