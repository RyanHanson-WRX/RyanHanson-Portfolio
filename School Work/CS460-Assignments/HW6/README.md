# Created by Ryan Hanson
# To Run:

1. On a local MS SQL server, run the `up.sql` and `seed.sql` found in `/Data/`
2. Configure the connection string to your local DB in `appsettings.json`, `Program.cs` - line 18, `Models/DDBBDbContext.cs` - line 36
3. From a terminal, cd into `/HW6/DoughnutDreamsBrewedBeans/DoughnutDreamsBrewedBeans`
4. Run `dotnet build`
5. Run `dotnet run`
6. Navigate to `localhost:5111` in a web browser
7. To add orders, run `python3 order_generator.py localhost:5111/api/order [# of orders per second, ex: 0.5] 40` **Note: last argument must be the # of menu items in the DB, for the current seed there are 40 items**


## Used:
- https://www.figma.com/ for wireframes
- https://dbdiagram.io/ for DB Design
- https://plantuml.com for UMLs
- Github Copilot
- Azure
