FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM armutcom/aspnet-core
RUN apt-get update && apt-get install -y libc6-dev libgdiplus libx11-dev
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 5001
ENTRYPOINT ["dotnet", "betauia.dll"]

