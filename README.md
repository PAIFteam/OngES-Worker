# OngES-Worker

Worker assíncrono da plataforma **Conexão Solidária**, da ONG Esperança Solidária. Consome os eventos de doação publicados pelo [OngES-Core](https://github.com/PAIFteam/OngES-Core) e credita o valor arrecadado no saldo da respectiva campanha. Projeto do Hackathon final (Fase 5) da pós-graduação em Arquitetura de Sistemas .NET — FIAP, pelo grupo **PAIF Team**.

## Papel na arquitetura

OngES-Core (API) ──publica──▶ RabbitMQ (donation_received_queue)
                                       │
                                       ▼
                         DonationReceivedEventConsumer
                                       │
                                       ▼
                          PostCampaignBalanceUseCase
                                       │
                                       ▼
             UPDATE dbo.campaign SET value_total_collected += valor

A API [OngES-Core](https://github.com/PAIFteam/OngES-Core) nunca atualiza o saldo da campanha diretamente — ela apenas registra a doação e publica um evento. Este Worker é o único responsável por creditar o valor, de forma assíncrona, o que é um requisito arquitetural obrigatório do hackathon (comunicação assíncrona via mensageria em vez de escrita direta no banco pela API).

## Arquitetura interna (Clean Architecture)

| Projeto | Responsabilidade |
|---|---|
| `OngES-Worker.API` | Host, DI, Swagger, configuração do consumidor RabbitMQ |
| `OngES-Worker.Core` | Entidades, casos de uso e interfaces (independente de infraestrutura) |
| `OngES-Worker.Infra` | Repositório (Dapper/SQL Server) e mensageria (MassTransit/RabbitMQ) |

## Tecnologias

- .NET 8
- MassTransit + RabbitMQ (consumidor de mensagens)
- Dapper + SQL Server
- MediatR
- Swagger / Swashbuckle (documentação em ambientes não produtivos)

## Estrutura de pastas

src/
├── Dockerfile
├── OngES-Worker-API.sln
└── Service/OngES-Worker/
    ├── OngES-Worker.API/       # host, DI, Swagger, configuração do consumer
    ├── OngES-Worker.Core/      # entidades, casos de uso, interfaces
    └── OngES-Worker.Infra/     # repositório (Dapper) e mensageria (RabbitMQ)

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- A mesma instância de RabbitMQ e SQL Server usada pelo [OngES-Core](https://github.com/PAIFteam/OngES-Core) (este Worker depende do schema/tabelas criadas por aquele repositório em `docker/db/init/`).

## Configuração

`src/Service/OngES-Worker/OngES-Worker.API/appsettings.json`:

```jsonc
{
  "ConnectionStrings": {
    "DB_SQL_ONGES": "<connection string do SQL Server>"
  },
  "RabbitSettings": {
    "HostName": "localhost",
    "Port": 5672,
    "UserName": "guest",
    "Password": "guest",
    "QueueName": "donation_received_queue",
    "QueueNameConsumer": "donation_received_queue",
    "StartConsumer": true
  }
}

▎ ⚠️ O appsettings.json versionado tem uma senha de banco e uma AdminKey de exemplo commitadas. Para uso real, sobrescreva localmente via appsettings.Local.json (já ignorado pelo Git) e nunca commite credenciais reais.

Como executar localmente

1. Suba o RabbitMQ e o SQL Server (veja o README do OngES-Core para os scripts de schema) — os dois serviços precisam apontar para a mesma infraestrutura.
2. Ajuste o appsettings.json deste repositório com a connection string e as credenciais do RabbitMQ.
3. Execute:
bash
cd src
dotnet restore OngES-Worker-API.sln
dotnet run --project Service/OngES-Worker/OngES-Worker.API
4. Com o OngES-Core também no ar, registre uma doação por lá (POST /donation/api/new) e acompanhe os logs deste Worker consumindo a mensagem e atualizando dbo.campaign.value_total_collected.

A API deste worker sobe em http://localhost:5002, com Swagger em /swagger fora do ambiente de produção — mas sua função real é o consumo em background, não os endpoints HTTP.

Status do projeto / o que ainda falta

O consumo assíncrono da doação e a atualização do saldo estão funcionando de ponta a ponta. Ainda não implementados, e pendentes para os requisitos do hackathon:

- Manifests de Kubernetes.
- Observabilidade (/health//metrics, dashboard Grafana/Zabbix).
- Pipeline de CI/CD (não existe nenhum neste repositório ainda).
- Dockerfile deste repositório referencia payments-api.sln/Payments.API.csproj — precisa ser corrigido para OngES-Worker-API.sln/OngES-Worker.API.csproj antes de gerar imagem.
- docker-compose.yml para subir a stack completa (SQL Server + RabbitMQ + Core + Worker) com um único comando.
- Testes automatizados (xUnit/NUnit).
