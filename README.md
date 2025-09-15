# 🚗 Vehicle Catalog API

## 📋 Descrição do Projeto

API RESTful desenvolvida em **.NET 8** para gerenciamento de catálogo de veículos automotores. O sistema permite cadastro, edição, consulta, busca avançada e remoção de veículos, além de receber notificações de vendas através de webhook. Focado na gestão centralizada do catálogo de veículos para revenda.

### 🎯 Objetivos
- Fornecer um catálogo centralizado e robusto para veículos
- Implementar operações CRUD completas com validações rigorosas
- Integrar com sistema de vendas via webhook para atualizações de status
- Garantir escalabilidade e manutenibilidade com Clean Architecture

## 🏗️ Arquitetura do Projeto VehicleCatalog

O projeto segue a **Clean Architecture** com separação clara de responsabilidades:

```
VehicleCatalog/
├── 🎯 VehicleCatalog.API          # Camada de Apresentação
├── 🧠 VehicleCatalog.Application  # Camada de Aplicação
├── 💎 VehicleCatalog.Domain       # Camada de Domínio
└── 🔌 VehicleCatalog.Infrastructure # Camada de Infraestrutura
```

### 🎯 VehicleCatalog.API (Camada de Apresentação)
**Responsabilidade:** Interface externa da aplicação

- **Controllers** 📡 - VehiclesController com endpoints REST completos
- **Health** 💊 - Health checks para monitoramento
- **Program.cs** ⚙️ - Configuração da aplicação, DI e middleware
- **appsettings.json** 📄 - Configurações (PostgreSQL, CORS, Logging)

### 🧠 VehicleCatalog.Application (Camada de Aplicação)
**Responsabilidade:** Casos de uso e orquestração de negócio

- **Controllers** 🎯 - VehicleUseCaseController (orquestração dos casos de uso)
- **DTOs** 📦 - Objetos de transferência (CreateVehicleDto, UpdateVehicleDto, etc.)
- **Gateways** 🔗 - Interfaces para acesso a dados e serviços externos
- **Presenters** 📋 - Formatação e apresentação de dados
- **UseCases** 📤 - Implementação da lógica de negócio específica

### 💎 VehicleCatalog.Domain (Camada de Domínio)
**Responsabilidade:** Regras de negócio puras

- **Entities** 🏛️ - Vehicle (entidade principal)
- **Enums** 📋 - PaymentStatus, VehicleStatus
- **Interfaces** 🔗 - Contratos para gateways e serviços

### 🔌 VehicleCatalog.Infrastructure (Camada de Infraestrutura)
**Responsabilidade:** Implementações técnicas e persistência

- **Data** 🗄️ - Contexto do Entity Framework e configurações
- **Gateways** 📚 - Implementações concretas das interfaces
- **Migrations** 📋 - Scripts de migração do PostgreSQL
- **Repositories** 📁 - Padrão Repository para acesso aos dados
- **Seeders** 🌱 - Dados iniciais para desenvolvimento/testes

## 🔄 Fluxo de Operações

```
1. 📱 Cliente faz requisição → VehiclesController
2. 🎯 Controller valida entrada básica
3. 🧠 UseCaseController processa caso de uso
4. 💎 Domain aplica regras de negócio
5. 🔌 Infrastructure persiste no PostgreSQL
6. 📋 Presenter formata resposta
7. 🔄 Resposta estruturada retorna ao cliente
```

## 🚀 Funcionalidades

### 📊 Gestão de Veículos
- ✅ **Cadastrar veículo** - Registra novo veículo no catálogo
- ✅ **Atualizar veículo** - Modifica informações do veículo
- ✅ **Buscar por ID** - Consulta veículo específico
- ✅ **Remover veículo** - Exclui veículo do catálogo (apenas se não vendido)

### 📋 Consultas e Filtros
- ✅ **Listar disponíveis** - Veículos à venda ordenados por preço
- ✅ **Listar vendidos** - Histórico de vendas com detalhes
- ✅ **Busca avançada** - Filtros por marca, modelo, preço, ano, cor
- ✅ **Filtros combinados** - Múltiplos critérios de busca

### 🔔 Integrações
- ✅ **Webhook de status** - Recebe atualizações de pagamento
- ✅ **Notificação de vendas** - Atualiza status quando vendido
- ✅ **Sincronização** - Mantém consistência com sistema de vendas

## 🛠️ Tecnologias Utilizadas

- **.NET 8** - Framework principal
- **Entity Framework Core 8** - ORM para acesso a dados
- **PostgreSQL** - Banco de dados relacional
- **Docker** - Containerização
- **Kubernetes** - Orquestração de containers
- **Swagger/OpenAPI** - Documentação da API
- **Health Checks** - Monitoramento de saúde

## 📦 Como Executar

### Pré-requisitos

- Docker e Docker Compose instalados
- .NET 8 SDK (desenvolvimento)
- Kubernetes (kubectl) configurado
- PostgreSQL (local ou via Docker)
- Minikube (para deploy local)

### 💻 Executando Localmente (Desenvolvimento)

```bash
# Clone o repositório
git clone https://github.com/ohntrebor/vehicle-catalog
cd vehicle-catalog

# Instale as dependências
dotnet restore

# Configure PostgreSQL local ou ajuste connection string
# Execute as migrations
dotnet ef database update -p VehicleCatalog.Infrastructure -s VehicleCatalog.API

# Execute a aplicação
dotnet run --project VehicleCatalog.API

# Abrirá em: https://localhost:7157/swagger/index.html
```

### 🐋 Executando com Docker

```bash
# Força rebuild e sobe em background
docker compose up -d --build

# Acesse em: http://localhost:5000/swagger/index.html

# Parar containers
docker compose down
```

### ☸️ Deploy no Kubernetes

```bash
# Aplique os manifests
kubectl apply -f k8s/

# Verifique o status
kubectl get all -n vehicle-catalog

# Port-forward para teste local
kubectl port-forward -n vehicle-catalog service/vehicle-catalog-service 5000:80

# Acesse em: http://localhost:5000/swagger/index.html
```

### ☸️ Deploy com Minikube (Automatizado)

```bash
# Setup completo com um comando
make k8s-full-deploy

# Acesse em: http://localhost:5000/swagger/index.html

# Para parar port-forwards
make k8s-stop
```

## 🧪 Testando a API

### 📊 Endpoints Principais

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/vehicles` | Cadastrar veículo |
| PUT | `/api/vehicles/{id}` | Atualizar veículo |
| GET | `/api/vehicles/{id}` | Buscar por ID |
| DELETE | `/api/vehicles/{id}` | Remover veículo |
| GET | `/api/vehicles/available` | Listar disponíveis |
| GET | `/api/vehicles/sold` | Listar vendidos |
| GET | `/api/vehicles/search` | Busca com filtros |
| POST | `/api/vehicles/payment-status` | Webhook de status |

### Exemplos de Requisições

#### Cadastrar Veículo
```json
POST /api/vehicles
{
  "brand": "Toyota",
  "model": "Corolla",
  "year": 2022,
  "color": "Prata",
  "price": 95000.00
}
```

#### Atualizar Veículo
```json
PUT /api/vehicles/{id}
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "brand": "Toyota",
  "model": "Corolla",
  "year": 2022,
  "color": "Branco",
  "price": 90000.00
}
```

#### Busca Avançada
```http
GET /api/vehicles/search?brand=Toyota&minPrice=50000&maxPrice=100000&year=2022&isAvailable=true
```

#### Webhook de Status
```json
POST /api/vehicles/payment-status
{
  "vehicleId": "550e8400-e29b-41d4-a716-446655440000",
  "paymentCode": "PAY-ABC123",
  "status": "confirmed"
}
```

### 📝 Importar Coleção Postman

Importe o arquivo `VehicleCatalog.postman_collection.json` no Postman para ter acesso a todos os endpoints configurados.

## 🔗 Integração com Vehicle Sales

O sistema funciona como catálogo centralizado e integra-se com o **Vehicle Sales API**:

- **Fornece dados** - Sales API consulta veículos disponíveis
- **Recebe notificações** - Quando vendas são processadas
- **Atualiza status** - Marca veículos como vendidos
- **Mantém consistência** - Dados sincronizados entre sistemas

### Configuração da Integração

```json
{
  "AllowedOrigins": [
    "http://vehicle-sales-service",
    "http://localhost:5001"
  ]
}
```

## 📊 Monitoramento

### Health Checks

- `/health` - Status geral (PostgreSQL, memória, disco)
- `/health/live` - Liveness probe para Kubernetes
- `/health/ready` - Readiness probe para Kubernetes

### Verificações Incluídas

- ✅ **PostgreSQL** - Conectividade com banco de dados
- ✅ **Memória** - Uso de recursos do sistema
- ✅ **Dependências** - Serviços externos necessários

## 🔒 Segurança

- ✅ **Connection Strings** - Secrets gerenciados via Kubernetes
- ✅ **Validação de Entrada** - DTOs com validação rigorosa
- ✅ **SQL Injection** - Proteção via Entity Framework
- ✅ **HTTPS** - Habilitado em produção
- ✅ **CORS** - Configurado para origens permitidas

## 📈 Performance e Escalabilidade

- **Response Time**: < 200ms para consultas simples
- **Throughput**: Suporta 100+ requisições simultâneas
- **Disponibilidade**: 99.9% com múltiplas réplicas
- **Auto-scaling**: Configurado no Kubernetes baseado em CPU

## 🗄️ Estrutura de Dados

### Vehicle (Entidade Principal)
```csharp
{
  "Id": "Guid",
  "Brand": "string",
  "Model": "string", 
  "Year": "int",
  "Color": "string",
  "Price": "decimal",
  "IsAvailable": "bool",
  "PaymentCode": "string?",
  "PaymentStatus": "PaymentStatus",
  "CreatedAt": "DateTime",
  "UpdatedAt": "DateTime"
}
```

### PaymentStatus (Enum)
- **Pending** - Aguardando pagamento
- **Paid** - Pago e confirmado
- **Cancelled** - Cancelado
- **Failed** - Falha no pagamento

## 🚀 Roadmap

- [ ] **Cache Redis** - Performance de consultas
- [ ] **Autenticação JWT** - Controle de acesso
- [ ] **Auditoria** - Log de todas as operações
- [ ] **Versionamento** - API versioning
- [ ] **Rate Limiting** - Controle de taxa de requisições

## 👥 Autor

**Robert A. dos Anjos**
- Email: robert.ads.anjos@gmail.com
- GitHub: @ohntrebor

## 📞 Suporte

Para suporte técnico, envie um email para: robert.ads.anjos@gmail.com

## 📋 Documentação Técnica

Para documentação técnica detalhada, consulte o arquivo `documentation.md`.

### Gerar PDF da Documentação

```bash
# Instalar dependências (Windows)
choco install pandoc && choco install wkhtmltopdf

# Gerar PDF
pandoc documentation.md -o VehicleCatalogAPI_Documentation.pdf --pdf-engine=wkhtmltopdf --toc --number-sections
```

---

*Sistema de catálogo de veículos desenvolvido com foco em arquitetura limpa, performance e integração com sistemas de vendas.*