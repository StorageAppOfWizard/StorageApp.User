FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /App

# Copia tudo do projeto
COPY . ./

# Restaura pacotes
RUN dotnet restore "StorageApp.User.sln"

# Publica o projeto Api
RUN dotnet publish "./StorageApp.User.Api/StorageApp.User.Api.csproj" -c Release -o /App/publish
# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /App

# Install the agent
RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
&& echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
&& wget https://download.newrelic.com/548C16BF.gpg \
&& apt-key add 548C16BF.gpg \
&& apt-get update \
&& apt-get install -y 'newrelic-dotnet-agent' \
&& rm -rf /var/lib/apt/lists/*

# Enable the agent
ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so \
NEW_RELIC_LICENSE_KEY=925dcc850ec30d000f9cddb8caebf8f2FFFFNRAL \
NEW_RELIC_APP_NAME="backend-user"

# Copia a pasta publicada
COPY --from=build-env /App/publish .

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:5000/
EXPOSE 5000
ENTRYPOINT ["dotnet", "StorageApp.User.Api.dll"]

