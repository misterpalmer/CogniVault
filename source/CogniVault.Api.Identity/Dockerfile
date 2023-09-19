# Use the ASP.NET Core runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0.10-jammy-amd64 AS base
WORKDIR /app
# Standard HTTPS port
EXPOSE 443

# Set the ASP.NET Core environment to Development
ENV ASPNETCORE_ENVIRONMENT=Development

# Set ASP.NET Core to listen only on the HTTPS port
ENV ASPNETCORE_URLS=https://+:443;
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="CogniVaultWebUI"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/CogniVaultWebUI.pfx

# Trust the custom root certificate
COPY ../../.certs/CogniVaultCA.crt /usr/local/share/ca-certificates/CogniVaultCA.crt
RUN update-ca-certificates

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0.400-jammy-amd64 AS build
ARG configuration=Release
WORKDIR /source

# Adjust the path to your csproj file relative to your Dockerfile's location
COPY ["source/CogniVault.Platform.Core/CogniVault.Platform.Core.csproj", "CogniVault.Platform.Core/"]
COPY ["source/CogniVault.Platform.Identity/CogniVault.Platform.Identity.csproj", "CogniVault.Platform.Identity/"]
COPY ["source/CogniVault.Api.Identity/CogniVault.Api.Identity.csproj", "CogniVault.Api.Identity/"]
RUN dotnet restore "CogniVault.Platform.Core/CogniVault.Platform.Core.csproj"
RUN dotnet restore "CogniVault.Platform.Identity/CogniVault.Platform.Identity.csproj"
RUN dotnet restore "CogniVault.Api.Identity/CogniVault.Api.Identity.csproj"

# # Adjust this path so it copies the entire solution directory content into /source
COPY .. .
WORKDIR ./source/CogniVault.Api.Identity
RUN dotnet build "CogniVault.Api.Identity.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "CogniVault.Api.Identity.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Adjust the path to copy the certificate from the .certs directory relative to Dockerfile's location
COPY ../../.certs/CogniVaultWebUI.pfx /https/CogniVaultWebUI.pfx

ENTRYPOINT ["dotnet", "CogniVault.Api.Identity.dll"]
