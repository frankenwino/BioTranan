# Versioning

Follow the major.minor.patch format:

version-0.2.0-new-authentication-system
version-0.2.1-fix-login-bug
version-0.2.2-patch-bug-fix

# Database

In appsettings.json set the connection string to **Data Source=../BioTranan.Core/BioTranan.db**

## Migration

Need to work out migrations and where to store the .db file. Makes sense for it to be in Core and the other projects to connect to it.

Having a problem. In BioTranan.Core:

`dotnet ef migrations add InitialCreate`

but get the following error:

`Build started...
Build succeeded.
Unable to create a 'DbContext' of type ''. The exception 'Unable to resolve service for type 'Microsoft.EntityFrameworkCore.DbContextOptions1[BioTranan.Core.Database.BioTrananDbContext]' while attempting to activate 'BioTranan.Core.Database.BioTrananDbContext'.' was thrown while attempting to create an instance. For the different patterns supported at design time, see https://go.microsoft.com/fwlink/?linkid=851728`

https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli

## Migrations

dotnet ef migrations add InitialCreate

dotnet ef database update

## ImdbIds

| Film                            | ImdbId    |
| ------------------------------- | --------- |
| Skatetown U.S.A.                | tt0079912 |
| Werewolves on Wheels            | tt0067972 |
| Master of the Flying Guillotine | tt0072913 |
| High Crime                      | tt0070552 |
| Demons                          | tt0089013 |
| Miami Connection                | tt0092549 |
| Xtro                            | tt0086610 |
| Escape from New York            | tt0082340 |
| The 36th Chamber of Shaolin     | tt0078243 |
| Malatesta's Carnival of Blood   | tt0215960 |
| Ghoulies II                     | tt0093091 |
| Creature with the Atom Brain    | tt0047960 |
| Mr. No Legs                     | tt0082775 |
| Nightmare City                  | tt0080931 |
| Django                          | tt0060315 |
| The Garbage Pail Kids Movie     | tt0093072 |
| Satan's Sadists                 | tt0064939 |
| Revenge of the Ninja            | tt0086192 |
| The Beyond                      | tt0082307 |
| Lifeforce                       | tt0089489 |
| The Mack                        | tt0070350 |
| R.O.T.O.R.                      | tt0098156 |
| Zombie Flesh Eaters             | tt0080057 |
| The Clones of Bruce Lee         | tt0075859 |

## Unique constraints

Seems like InMemory does not respect the unique constraint in Film.ImdbId. Works fine for a physical database file.

# Scaffolding

At some point when you're more comfortable with CRUD operaions in Blazor, use [scaffolding](https://learn.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/model?view=aspnetcore-8.0&tabs=visual-studio-code) for convenience.
