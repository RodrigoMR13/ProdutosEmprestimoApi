# ProdutosEmprestimoApi

[![.NET](https://img.shields.io/badge/.NET-9.0-blueviolet?logo=dotnet)](https://dotnet.microsoft.com/)  
![Build and Tests](https://img.shields.io/github/actions/workflow/status/RodrigoMR13/ProdutosEmprestimoApi/dotnet.yml?label=build&logo=github)  
![Coverage](https://img.shields.io/codecov/c/github/RodrigoMR13/ProdutosEmprestimoApi?logo=codecov)

API para gerenciamento de **produtos de empréstimo** e **simulação de empréstimos**, desenvolvida em **.NET 9** com **Clean Architecture**.

---

## 📋 Sumário

- [ProdutosEmprestimoApi](#produtosemprestimoapi)
  - [📋 Sumário](#-sumário)
  - [Visão Geral](#visão-geral)
  - [Funcionalidades](#funcionalidades)
  - [Arquitetura](#arquitetura)
  - [Requisitos](#requisitos)
  - [Como Executar](#como-executar)
  - [Cache em memória](#cache-em-memória)
  - [Testes](#testes)

---

## Visão Geral

Este projeto foi criado para apoiar cenários de crédito e empréstimos, oferecendo:

- CRUD de **ProdutoEmprestimo**
- **Simulação de empréstimos** (cálculo de parcelas, juros e amortização)
- **Validações** com FluentValidation
- Uso de **MediatR** para Commands e Queries
- Arquitetura em camadas com **Clean Architecture**
- Suporte a **cache em memória** para otimizar consultas

---

## Funcionalidades

✅ Criar, atualizar, consultar e excluir produtos de empréstimo  
✅ Simular empréstimo com cálculo detalhado  
✅ Validações consistentes em todos os endpoints  
✅ Cache em memória para simulações repetidas  
✅ Testes unitários para regras e validações

---

## Arquitetura

```
/Application      → lógica de negócio (Commands, Queries, Handlers, Validators, DTOs, Responses)
/Domain           → entidades de domínio, regras centrais
/Infrastructure   → implementações concretas (persistência, cache, etc.)
/WebApi           → API REST, controllers, configuração
/UnitTests        → testes unitários (xUnit + FluentAssertions + Moq)
```

---

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

---

## Como Executar

1. Clone o repositório:

   ```bash
   git clone https://github.com/RodrigoMR13/ProdutosEmprestimoApi.git
   cd ProdutosEmprestimoApi
   ```

2. Restaure pacotes:

   ```bash
   dotnet restore
   ```

3. Compile:

   ```bash
   dotnet build
   ```

4. Execute a API:

   ```bash
   dotnet run --project WebApi/WebApi.csproj
   ```

5. Acesse a API em:

   ```
   http://localhost:5244/swagger
   ```

---

## Cache em memória

As simulações de empréstimo utilizam **cache em memória** via `ICacheService`.

- 🔑 Chaves são compostas por: `simulacao:{IdProduto}:{ValorSolicitado}:{PrazoMeses}`
- ⏳ Tempo de expiração padrão: **5 minutos**
- ⚡ Evita recalcular simulações iguais

---

## Testes

Os testes unitários estão no projeto **UnitTests**.

Para executar:

```bash
dotnet test
```

São testados:

- Validadores (FluentValidation)
- Handlers (MediatR)
- Pipeline behaviors (ex: ValidationBehavior)

---
