# 123Vendas - Sistema de Gestão de Vendas

Este projeto é uma API para gestão de vendas desenvolvida em **ASP.NET Core 8**, utilizando os princípios de **DDD (Domain-Driven Design)**, **SOLID** e integração com **Envio de Eventos**.

## 📋 Pré-requisitos

Antes de iniciar o projeto, certifique-se de ter os seguintes itens instalados:

- [**.NET 8 SDK**](https://dotnet.microsoft.com/download/dotnet/8.0)
- [**Entity Framework CLI**](https://learn.microsoft.com/ef/core/cli/) (já incluído no .NET SDK)
- [**PostgreSQL**](https://www.postgresql.org/download/)

---

## 🛠️ Configuração do Banco de Dados

1. **Configurar a string de conexão**  
   No arquivo `appsettings.Development.json`, insira sua string de conexão com o PostgreSQL, conforme o exemplo abaixo:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=AndreiLima123vendas;Username=seu_usuario;Password=sua_senha"
     }
   }


2. Atualizar o banco de dados
No terminal ou prompt de comando, navegue até a pasta raiz do projeto e execute o comando abaixo para aplicar as migrações:
Atualizar o banco de dados
No terminal ou prompt de comando, navegue até a pasta raiz do projeto e execute o comando abaixo para aplicar as migrações:

dotnet ef database update -s .\src\AndreiLima.123Vendas.API


🚀 Tecnologias e Práticas Utilizadas
ASP.NET Core 8: Framework principal para desenvolvimento da API.
Entity Framework Core: ORM para mapeamento e gestão do banco de dados.
PostgreSQL: Banco de dados relacional.
DDD (Domain-Driven Design): Organização da estrutura do projeto baseada em domínios.
SOLID: Aplicação de princípios para código limpo e manutenível.
Eventos: Integração para envio e tratamento de eventos no sistema.
📚 Estrutura do Projeto
A estrutura do projeto segue os princípios do DDD, dividindo responsabilidades entre camadas específicas:

Domain: Regras de negócio e entidades.
Application: Casos de uso e serviços de aplicação.
Infrastructure: Acesso a dados e integração com serviços externos.
API: Interface REST para consumo da aplicação.
