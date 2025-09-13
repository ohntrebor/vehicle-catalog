# ===========================================
# DOCKERFILE - Vehicle Catalog API
# ===========================================
# Este arquivo define como construir a imagem Docker da aplicação
# Usa multi-stage build para otimizar o tamanho final da imagem

# STAGE 1: Build - Compila o código
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos .csproj primeiro (para cache de layers do Docker)
# Isso permite que o Docker reutilize layers se as dependências não mudaram
COPY ["VehicleCatalog.API/VehicleCatalog.API.csproj", "VehicleCatalog.API/"]
COPY ["VehicleCatalog.Application/VehicleCatalog.Application.csproj", "VehicleCatalog.Application/"]
COPY ["VehicleCatalog.Domain/VehicleCatalog.Domain.csproj", "VehicleCatalog.Domain/"]
COPY ["VehicleCatalog.Infrastructure/VehicleCatalog.Infrastructure.csproj", "VehicleCatalog.Infrastructure/"]

# Restaura as dependências NuGet
RUN dotnet restore "VehicleCatalog.API/VehicleCatalog.API.csproj"

# Copia todo o código fonte
COPY . .

# Navega para o diretório da API
WORKDIR "/src/VehicleCatalog.API"

# Compila a aplicação em modo Release
RUN dotnet build "VehicleCatalog.API.csproj" -c Release -o /app/build

# STAGE 2: Publish - Prepara os arquivos para produção
FROM build AS publish
RUN dotnet publish "VehicleCatalog.API.csproj" -c Release -o /app/publish

# STAGE 3: Runtime - Imagem final otimizada apenas com runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Expõe as portas que a aplicação usa

 # HTTP
EXPOSE 80  

 # HTTPS
EXPOSE 443

# Copia os arquivos publicados do stage anterior
COPY --from=publish /app/publish .

# Define o comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "VehicleCatalog.API.dll"]

# Healthcheck opcional (descomente se quiser)
# HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
#   CMD curl -f http://localhost/api/health || exit 1