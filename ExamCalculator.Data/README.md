# Database Backend

Some handy commands that you will probably need:

* Installing the EF Core CLI: `dotnet tool install --global dotnet-ef`
* Updating the database to the latest migration state: `dotnet ef database update`
* "All in one" command for experimentation during development:
  ```
  dotnet ef migrations remove --force \
    && dotnet ef migrations add <Migration Name> \
    && dotnet ef database update
  ```