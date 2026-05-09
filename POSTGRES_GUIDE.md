# Guía rápida: conectar el backend de InCleanHome a PostgreSQL

## 1. Instalar PostgreSQL

Elige **uno** de estos caminos:

### Opción A — Instalador oficial (Windows / macOS / Linux)
1. Descarga de https://www.postgresql.org/download/
2. Durante la instalación toma nota del **puerto** (por defecto `5432`) y la **contraseña del usuario `postgres`**.
3. La instalación incluye **pgAdmin 4** (GUI) y `psql` (CLI).

### Opción B — Docker (rápido para desarrollo)
```bash
docker run --name incleanhome-pg \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=postgres \
  -e POSTGRES_DB=incleanhome \
  -p 5432:5432 \
  -v incleanhome_pgdata:/var/lib/postgresql/data \
  -d postgres:16
```
- Para arrancar/parar después: `docker start incleanhome-pg` / `docker stop incleanhome-pg`.

## 2. Crear la base de datos

Si usaste el instalador oficial, abre **psql** o **pgAdmin** y ejecuta:

```sql
CREATE DATABASE incleanhome;
-- (opcional) usuario dedicado:
CREATE USER incleanhome_user WITH ENCRYPTED PASSWORD 'change_me';
GRANT ALL PRIVILEGES ON DATABASE incleanhome TO incleanhome_user;
```

> Si usaste Docker con `POSTGRES_DB=incleanhome`, este paso ya está hecho.

## 3. Configurar el connection string

Edita `HomeLink.InCleanHome.API/appsettings.json` (o `appsettings.Development.json`):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=incleanhome;Username=postgres;Password=postgres"
  }
}
```

**Formato de connection string (Npgsql):**
- `Host` — servidor (`localhost`, IP, o `db.empresa.com`).
- `Port` — `5432` por defecto.
- `Database` — nombre de la BD que creaste.
- `Username` / `Password` — credenciales.
- Otros opcionales útiles: `SSL Mode=Require` (producción), `Pooling=true`, `Maximum Pool Size=100`, `Timeout=30`.

Ejemplo producción con SSL:
```
Host=db.incleanhome.pe;Port=5432;Database=incleanhome;Username=app;Password=***;SSL Mode=Require;Trust Server Certificate=true
```

## 4. Configurar el `TokenSettings.Secret`

En el mismo `appsettings.json`, reemplaza el placeholder por una cadena aleatoria de **al menos 32 caracteres**:
```json
"TokenSettings": {
  "Secret": "una_clave_super_larga_y_aleatoria_de_32_caracteres_o_mas_aqui"
}
```
Puedes generar una con:
- Linux/macOS: `openssl rand -base64 48`
- Windows (PowerShell): `[Convert]::ToBase64String([Security.Cryptography.RandomNumberGenerator]::GetBytes(48))`

## 5. Restaurar paquetes y correr

```bash
cd HomeLink.InCleanHome.API
dotnet restore
dotnet run
```

En el primer arranque, `context.Database.EnsureCreated()` (en `Program.cs`) creará automáticamente todas las tablas en `incleanhome` aplicando la convención `snake_case` (Humanizer).

Abre Swagger:
- HTTP: http://localhost:5230/swagger
- HTTPS: https://localhost:7230/swagger

## 6. Verificación rápida

### Desde psql
```bash
psql -h localhost -U postgres -d incleanhome
\dt
```
Deberías ver tablas como `users`, `client_profiles`, `worker_profiles`, `service_categories`, `worker_services`, `availability_slots`, `booking_requests`, `payment_methods`, `monthly_commissions`, `reviews`, `profile_reports`.

### Desde Rider / pgAdmin
1. **Rider** → "Database" tool window → `+` → Data Source → PostgreSQL.
2. Host `localhost`, port `5432`, database `incleanhome`, user `postgres`, pass `postgres`.
3. Test Connection → OK.
4. Si Rider pide bajar el driver de PostgreSQL, acepta.

### Probar un endpoint
```bash
curl -X POST http://localhost:5230/api/v1/authentication/sign-up \
  -H "Content-Type: application/json" \
  -d '{"email":"melisa@incleanhome.pe","password":"Test1234!","role":"CLIENT"}'
```

## 7. Migraciones (cuando ya estés en producción)

Mientras desarrollas puedes seguir con `EnsureCreated()`. Cuando tengas datos reales, conviene migraciones:

```bash
# Instalar la CLI una vez:
dotnet tool install --global dotnet-ef

cd HomeLink.InCleanHome.API
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Para que esto funcione, **reemplaza** en `Program.cs`:
```csharp
context.Database.EnsureCreated();
```
por:
```csharp
context.Database.Migrate();
```

## 8. Problemas comunes

| Error | Causa probable | Solución |
|---|---|---|
| `Connection refused` | Postgres apagado o puerto distinto | `pg_ctl status` / revisar puerto |
| `password authentication failed` | Usuario o pass incorrectos | Editar connection string |
| `database "incleanhome" does not exist` | No creaste la BD | `CREATE DATABASE incleanhome;` |
| `relation does not exist` al consultar | Aún no se ejecutó el `EnsureCreated` | Reinicia la API una vez |
| `SSL connection is required` (cloud) | Falta SSL en el string | Agrega `SSL Mode=Require` |
| Caracteres con tilde/ñ corruptos | Encoding | Crear DB con `WITH ENCODING 'UTF8' LC_COLLATE='es_PE.UTF-8' LC_CTYPE='es_PE.UTF-8'` |

## 9. Diferencias con la versión MySQL

| Aspecto | MySQL (versión anterior) | PostgreSQL (esta versión) |
|---|---|---|
| Paquete NuGet | `MySql.EntityFrameworkCore` | `Npgsql.EntityFrameworkCore.PostgreSQL` |
| Llamada en Program.cs | `options.UseMySQL(...)` | `options.UseNpgsql(...)` |
| Connection string | `server=...;user=...;password=...;database=...` | `Host=...;Port=...;Username=...;Password=...;Database=...` |
| Puerto por defecto | 3306 | 5432 |
| Tipos JSON nativos | `JSON` | `jsonb` (más eficiente) |
| Auto-increment | `AUTO_INCREMENT` | `SERIAL` / `IDENTITY` |
| Mayúsculas/minúsculas en identificadores | insensible | sensible (otra razón para `snake_case`) |
