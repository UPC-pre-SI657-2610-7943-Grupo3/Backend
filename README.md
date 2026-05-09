# HomeLink.InCleanHome.API — PostgreSQL Edition

Backend monolítico para la plataforma **InCleanHome** (HomeLink) — conexión segura entre clientes y trabajadoras del hogar en Lima Metropolitana.

Construido con **.NET 9 / ASP.NET Core** + **PostgreSQL**, siguiendo **DDD + Clean Architecture**, sin patrón mapping (sin AutoMapper). Las transformaciones se hacen con **Assemblers estáticos**. Esta edición **no integra servicios externos** (Auth0 / Google Maps / Izipay / Twilio / FCM).

## Arquitectura

```
HomeLink.InCleanHome.sln
└── HomeLink.InCleanHome.API/
    ├── IAM/                       (User Management — registro, login, roles)
    │   ├── Application/
    │   ├── Domain/
    │   ├── Infrastructure/
    │   └── Interfaces/
    ├── Profiles/                  (Perfiles de Cliente y Trabajadora)
    │   ├── Application/
    │   ├── Domain/
    │   ├── Infrastructure/
    │   └── Interfaces/
    ├── SearchAndCatalog/          (Categorías, servicios, disponibilidad y búsqueda)
    │   ├── Application/
    │   ├── Domain/
    │   ├── Infrastructure/
    │   └── Interfaces/
    ├── Booking/                   (Solicitudes — accept/reject/reschedule/cancel)
    │   ├── Application/
    │   ├── Domain/
    │   ├── Infrastructure/
    │   └── Interfaces/
    ├── Payments/                  (Métodos de pago + comisión mensual del 10%)
    │   ├── Application/
    │   ├── Domain/
    │   ├── Infrastructure/
    │   └── Interfaces/
    ├── ReviewsAndEvaluation/      (Calificaciones, reseñas y reportes)
    │   ├── Application/
    │   ├── Domain/
    │   ├── Infrastructure/
    │   └── Interfaces/
    ├── Shared/
    │   ├── Domain/
    │   └── Infrastructure/
    ├── Program.cs
    ├── appsettings.json
    └── HomeLink.InCleanHome.API.csproj
```

## Bounded Contexts

| BC | Responsabilidad |
|---|---|
| IAM | Registro, login, roles (CLIENT / WORKER / ADMIN), JWT, BCrypt |
| Profiles | WorkerProfile y ClientProfile, con verificación, biografía, rating |
| SearchAndCatalog | ServiceCategory, WorkerService, AvailabilitySlot — filtros |
| Booking | BookingRequest aggregate con todas las reglas de transición |
| Payments | PaymentMethod (off-platform) y MonthlyCommission (10%) |
| ReviewsAndEvaluation | Review (1-5 estrellas) y ProfileReport (perfiles sospechosos) |

## Dependencias

* Microsoft.EntityFrameworkCore 9.0.5
* **Npgsql.EntityFrameworkCore.PostgreSQL 9.0.4**  ← reemplaza a MySql
* EntityFrameworkCore.CreatedUpdatedDate 8.0.0  (audit automático CreatedAt/UpdatedAt)
* Humanizer 2.14.1                              (snake_case naming)
* Swashbuckle.AspNetCore 8.1.2 (+ Annotations)
* BCrypt.Net-Next 4.0.3
* System.IdentityModel.Tokens.Jwt 8.11.0
* Microsoft.AspNetCore.Authentication.JwtBearer 9.0.5
* Microsoft.IdentityModel.Tokens 8.11.0

## Cómo correr (Rider / dotnet CLI)

1. Tener PostgreSQL ≥ 13 instalado y corriendo (ver `POSTGRES_GUIDE.md`).
2. Configurar `ConnectionStrings.DefaultConnection` y `TokenSettings.Secret` en `appsettings.json` (o `appsettings.Development.json`).
3. Restaurar y correr:

```bash
dotnet restore
dotnet run --project HomeLink.InCleanHome.API
```

4. Abrir Swagger en `https://localhost:7230/swagger`.

## Patrones implementados

* **Clean Architecture** (Domain → Application → Infrastructure / Interfaces)
* **DDD**: Aggregates, ValueObjects, Domain Services, Bounded Contexts
* **CQRS** (Commands vs Queries Services)
* **Repository + Unit of Work**
* **Anti-Corruption Layer (ACL)** vía facades en `Interfaces/ACL/`
* **Static Assemblers** en `Interfaces/REST/Transform/` (NO mapping/AutoMapper)
* **Snake_case naming convention** automática (Humanizer)
* **JWT Bearer Authentication** + middleware custom
