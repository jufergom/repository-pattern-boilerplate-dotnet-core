# Repository Pattern Boilerplate in .NET Core Web API

A .NET Core Web API application containing a Repository Pattern boilerplate code. Sample application is a blog that contains posts. These entities are stored in a sqlite database. The idea is to make this project customizable and modify it to support any SQL database with Entity Framework.

## Resources used to create this project

- https://codewithmukesh.com/blog/repository-pattern-in-aspnet-core/
- https://codewithmukesh.com/blog/entity-framework-core-in-aspnet-core/
- https://github.com/Unitec-Aplicaciones-Vanguardia/ProductsCatalogApi
- https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
- https://codereview.stackexchange.com/questions/171162/generic-repository-and-generic-service
- https://brianbu.com/2019/09/25/the-repository-pattern-isnt-an-anti-pattern-youre-just-doing-it-wrong/

## Commands used for database migration

Run the following commands inside the Package Manager Console to execute the sqlite database migration.

```sh
add-migration Initial
update-database
```