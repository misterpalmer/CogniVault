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

# Copy your csproj files and restore any dependencies
COPY ["source/CogniVault.Platform.Core/CogniVault.Platform.Core.csproj", "CogniVault.Platform.Core/"]
COPY ["source/CogniVault.Application/CogniVault.Application.csproj", "CogniVault.Application/"]
COPY ["source/CogniVault.Application.VirtualFileSystem/CogniVault.Application.VirtualFileSystem.csproj", "CogniVault.Application.VirtualFileSystem/"]
COPY ["source/CogniVault.Api.VirtualFileSystem/CogniVault.Api.VirtualFileSystem.csproj", "CogniVault.Api.VirtualFileSystem/"]

COPY . .
WORKDIR ./source/CogniVault.Api.VirtualFileSystem
RUN dotnet build "CogniVault.Api.VirtualFileSystem.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "CogniVault.Api.VirtualFileSystem.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy your certificates
COPY ../../.certs/CogniVaultWebUI.pfx /https/CogniVaultWebUI.pfx

ENTRYPOINT ["dotnet", "CogniVault.Api.VirtualFileSystem.dll"]
