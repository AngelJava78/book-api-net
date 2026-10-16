# BooksApiDemo

API REST didáctica de libros con ASP.NET Core, .NET 10, C#, Dapper y SQL Server.

## Arquitectura

- Controllers: protocolo HTTP.
- DTOs: datos de entrada para crear y actualizar.
- Models: modelo `Book`.
- Repositories: todo el acceso SQL mediante Dapper.
- Dependency Injection: `IBookRepository` -> `BookRepository`.
- Services: no se usan porque el CRUD no tiene lógica de negocio adicional.

## 1. Crear la base de datos

Ejecuta en SQL Server:

`Sql/01-create-database.sql`

El script crea la base `BooksDemo`, la tabla `dbo.Books` y tres registros de ejemplo.

## 2. Configurar la conexión

Ajusta `ConnectionStrings:BooksDatabase` en `appsettings.json` según tu SQL Server.

Ejemplo con autenticación de Windows:

`Server=localhost;Database=BooksDemo;Trusted_Connection=True;TrustServerCertificate=True;`

Ejemplo con usuario SQL:

`Server=localhost;Database=BooksDemo;User Id=TU_USUARIO;Password=TU_PASSWORD;TrustServerCertificate=True;`

No subas contraseñas reales al repositorio.

## 3. Ejecutar

```bash
dotnet restore
dotnet run
```

Perfil HTTPS esperado: `https://localhost:7190`.

## Endpoints

| Método | URL | Descripción |
|---|---|---|
| GET | `/api/books` | Lista libros |
| GET | `/api/books/{id}` | Obtiene un libro |
| POST | `/api/books` | Crea un libro |
| PUT | `/api/books/{id}` | Actualiza un libro |
| DELETE | `/api/books/{id}` | Elimina un libro |

## Autenticación

Esta primera versión no implementa autenticación. La idea es agregarla después para que la aplicación ASP.NET Core MVC autenticada con Auth0 consuma esta API usando un Access Token.
