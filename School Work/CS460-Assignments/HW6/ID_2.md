# ID 2
<hr>

## Title

*As an employee working at one of the fulfillment stations I need to be able to see items ordered from my station so I can make them*

## Description

An employee is working at a fulfillment station, let's say the espresso station.  Orders come in from the order system that may contain espresso orders.  Each item that needs to be made at my station should be displayed on my screen as it comes in.  I'll see that, pull shots of espresso as needed to make the coffee item, send it off to the integration area, and then use the screen to check off that the item has been made and delivered.  Once checked off it disappears from my screen, keeping the display showing only items that I need to make. 

### Details

1. We have multiple stations so the fulfillment page needs to have a way to select which station the employee is working at.
2. Items need to be displayed as soon as they come in
3. Show the item name, description and quantity
4. Items need to be able to be checked off in a very simple manner to keep things moving efficiently, i.e. one click
5. Items must of course match the station being displayed (can't have a chocolate donut being displayed at the soft drink station!)

### Implementation Details

1. Use AJAX to display the orders
2. Update the page frequently

### Acceptance Criteria
See the associated `.feature` file

### Assumptions/Preconditions
We need to know the possible fulfillment stations and which items go with which station

### Dependencies
ID_1

### Effort Points
4

### Owner
Ryan Hanson

### Git feature branch name
`DDBB-US-2`

### Modeling and Other Documents

1. UI sketch [Fulfillment_page_sketch](CS460_RyanHanson_HW6_FulfillmentWireframe.png)
2. Table showing fulfillment stations and items that go to each station [Table_of_stations](Table_Of_Stations.md), updated item list [items.md](items.md) 
3. Updated database model [DbDiagram.io link](https://dbdiagram.io/d/CS460_RyanHanson_HW6DBModel-65640aa93be1495787c671d9) - [DbDiagram PDF](CS460_RyanHanson_HW6DBModel_ID2.pdf)
4. UML diagram of repository/service layer to build [ID_2_UML.md](ID_2_UML.md) - [ID_2_UML.png](ID_2_UML.png)

### Tasks

1. [Update Database](DoughnutDreamsBrewedBeans/DoughnutDreamsBrewedBeans/Data/) ***Note: Unchanged from ID 1*** 
2. [Scaffold Database](DoughnutDreamsBrewedBeans/DoughnutDreamsBrewedBeans/Models/)
3. [Create new View Models for Station Specific Orders](DoughnutDreamsBrewedBeans/DoughnutDreamsBrewedBeans/ViewModels/)
4. [Implement Method 'GetStationOrdersVM' to Order Repository for returning orders from a specific station in new station related view models](DoughnutDreamsBrewedBeans/DoughnutDreamsBrewedBeans/DAL/Concrete/OrderRepository.cs)
5. [Implement Endpoint in DDBB API Controller to get & return orders from specific stations](DoughnutDreamsBrewedBeans/DoughnutDreamsBrewedBeans/Controllers/DDBBAPIController.cs)
6. [Implement Fulfillment Page](DoughnutDreamsBrewedBeans/DoughnutDreamsBrewedBeans/Views/Home/Fulfillment.cshtml)
7. [Add Fulfillment Page method to HomeController](DoughnutDreamsBrewedBeans/DoughnutDreamsBrewedBeans/Controllers/HomeController.cs)
8. [Add JS for Fulfillment Page](DoughnutDreamsBrewedBeans/DoughnutDreamsBrewedBeans/wwwroot/js/fulfillment.js)
9. [Style Fulfillment Page](DoughnutDreamsBrewedBeans/DoughnutDreamsBrewedBeans/wwwroot/css/site.css)
10. [Test new 'GetStationOrdersVM' method in Order Repository](DoughnutDreamsBrewedBeans/DoughnutDreamsBrewedBeans_Tests/OrderRepository_Tests.cs)
