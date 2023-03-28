## How to set up Database:
In order to initialize the SubscriptionServices Database, follow these step:
```
1. Run first chunk of commands ('USE master' and 'GO')
2. Run second chunk of commands (From 'IF DB_ID...' through the 'USE [SubscriptionServices] GO')
3. Run each table in order, by itself. The tables which include foreign keys will generate an error if done out of order, best to run each 'CREATE TABLE' independently
4. Run the data inserts.
5. Run the ASP.NET IDENTITY table commands.
6. Run each function and trigger independently, in the order that they are created. Afterwards, the DB should be set up!!
```

## How to set up the application:
In order to set up the application, follow these steps:
```
1. First, run your local database and find the connection string.
2. After obtaining the connection string, in DbProject.Web/appsettings.json, add your connection string as a value with a unique name for the key.
3. Navigate to DbProject.Web/Program.cs, in line 14 change `connectionString = builder.Configuration.GetConnectionString("RyansConnectionString")` to `connectionString = builder.Configuration.GetConnectionString(<Your connection string key name from DbProject.Web/appsettings.json>)`.
4. Navigate to DbProject.Web/Models/SubscriptionServicesContext.cs, in line 35 change 
`        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:SubscriptionServices");` to 
`        => optionsBuilder.UseSqlServer(<Your connection string>);`.
```

## How to run the application:
After setting up the database and configuring the connection strings, now it's time to run the application!
```
1. In a bash or terminal, or other means of a console, CD to the cloned repository.
2. run `$ cd DbProject.Web`
3. run `$ dotnet build`
4. run `$ dotnet run`
5. Now, the application is running at 'http://localhost:5144', navigate to that address in a web browser.
6. The application is now running! Create an account via 'Register', confirm the email, then login to your created account via 'Login'. You are now able to manage your subscriptions! 😁

