FROM mcr.microsoft.com/dotnet/aspnet:7.0.10-jammy-amd64 AS base
WORKDIR /app
EXPOSE 5166
EXPOSE 7166

# Set ASP.NET Core to listen on both HTTP and HTTPS ports
ENV ASPNETCORE_URLS=http://+:5166;https://+:7166
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="password"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0.400-jammy-amd64 AS build
ARG configuration=Release
WORKDIR /src
COPY ["source/CogniVault.Api.Identity/CogniVault.Api.Identity.csproj", "source/CogniVault.Api.Identity/"]
RUN dotnet restore "source/CogniVault.Api.Identity/CogniVault.Api.Identity.csproj"
COPY . .
WORKDIR "/src/source/CogniVault.Api.Identity"
RUN dotnet build "CogniVault.Api.Identity.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "CogniVault.Api.Identity.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Assuming you have an aspnetapp.pfx certificate for dev, you'll copy it into your image
COPY .cert/cognivault.pfx /https/aspnetapp.pfx

ENTRYPOINT ["dotnet", "CogniVault.Api.Identity.dll"]
