# Assignment

Before running:

Copy `Assignment.API/appsettings.example.json` to `Assignment.API/appsettings.json` and edit the **ConnectionStrings**.

```
cp Assignment.API/appsettings.example.json Assignment.API/appsettings.json
```
### Create the DB:

Run `Assignment.Core/db/0000-CreateDatabase.sql` script to create the database. _Edit the paths before running._

### Run the migrations:

Use the rest of the scripts in `Assignment.Core/db` folder.

**Or** in Package Manager Console run:

```
Update-Database
```
### To run the API:

Set `Assignment.API` as the Startup Project.

Run the app with `IIS Express`.

Test the endpoints using `Assignment.API/Assignment.API.http` in Visual Studio (**version 17.8** or later).

or by visiting (https://localhost:44370/swagger)[https://localhost:44370/swagger].

### Unit Tests:

Included in the `Assignment.Tests` project.



