# TvMazeApp

TVMazeApp has two main applications to run.

1. **Scraper APP**
2. **TvMaze Client API**

The solution has a multi-layered architecture.
It has `TvMazeApp.API` which provides a client API to get the stored data that was fetched
from [TvMaze API](https://www.tvmaze.com/api). TvMaze.API has access to the Azure SQL Db connection (You can use
any SQL Db connection). The `TVMazeApp.API.BusinessLayer` includes the business logic of the client API and
access to the database through the `TvMazeApp.DataAccess` layer. This layer has the **_Repository_** and **_UnitOfWork_**
patterns to access the Db with the help of EF.Core. It has two different functions;

* Searching for a TV show by part of its name.
* Getting a list of episodes for a show by the Id of the TV show.

The entities that are needed to create the DbContext models are placed on the `TvMazeApp.Entity` layer. On the other
side, there is `TvMazeApp.Scraper.App` that has a `React application` and it has a small UI to add the **Tv Shows** 
and its **Episodes** with the show name entered. You are able to scrape the shows and episodes with the help of this small app.
The `TvMazeApp.Scraper.BusinessLayer` exists as the business logic which accesses the TvMaze API and stores it in the database.


For the testing purposes of the business logic, there is a layer with the name `TvMazeApp.UnitTests`.

### TvMazeApp.API Endpoints

1) Search Tv Shows by Show Name.
- http://localhost:5256/api/TvShows/{showName}

2) Get Episodes by ShowId
- http://localhost:5256/api/TvShows/{showId}/Episodes

You can change the port from the **_appsettings.json_** file of the Client API. You can also change the connection string
of the application from the file and for the `TvMaze.Scraper.App` also from the **_appsettings.json_** file. 

### TvMazeApp.ApiDataSyncWorker

The worker application is an `Azure Function App`. This application is triggered by the HTTP call. You can run it with **Visual Studio**.
This Function App takes the Api Id parameter which is the exact Id of the API data that is stored in our db. It searches for the Tv Shows
according to their existence in our Db and also checks the `Updated` field in the Db to see if it changed in time. If it is changed, the function will just update the Tv Show and it's Episodes.
