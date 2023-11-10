# Developer Coding Test

## How to Run

*Note: on start up and every 60 seconds (configurable) after, the wrapper API web application will be polling source API for new data.
During these periods, the application will be making a number of requests to source API as quickly as possible in controlled batches making up to 8 (configurable) parrallel requests at a time.*

### Development prerequisites

| Tool / SDK | Download URL | Note |
| - | - | - |
| git | https://git-scm.com/downloads | Optional</br><sub>(code can be downloaded as a ZIP file from GitHub)</sub> |
| Docker | https://docs.docker.com/desktop/install/windows-install | Optional</br><sub>(you can run the API on the host machine instead)</sub> |
| .NET 7 | https://dotnet.microsoft.com/en-us/download/dotnet/7.0 | Mandatory |

### How to run if you are using an IDE, e.g. Visual Studio 2022

* Clone the repository: https://github.com/dsaf/denys-news.git
* Ensure correct start up project is selected: `Denys.News.Api`
* Select the desired debug target: `http`, `https`, `Docker` etc.
* Run/debug the API
* A Swagger UI page should open up automatically in your default browser: http://localhost:5073/swagger
* Alternatively, you can invoke the endpoint directly using your preferred tool: http://localhost:5073/api/v1/stories?n=10

### How to run if you prefer the command line

```cmd
mkdir C:\Projects\_github\DenysNews
cd C:\Projects\_github\DenysNews
git clone https://github.com/dsaf/denys-news.git
start http://localhost:5073/swagger
dotnet run --project denys-news\src\api\Denys.News.Api\Denys.News.Api.csproj
```

You should see Swagger UI opening up in your default browser: http://localhost:5073/swagger .
Alternatively, you can invoke the endpoint directly using your preferred tool: http://localhost:5073/api/v1/stories?n=10 .

## Assumptions Made

* .NET 7 target is acceptible despite being an STS release.
* When more than 200 stories are requested, there is no error produced but only 200 stories are returned, due to source API limitations
* It is acceptable to serve a snapshot of versions refreshed every 60 seconds (configurable)
* Relative transactionality of data is important, so the list of best stories should be refreshed at approximately same time as story details
* Implications of scaling out the web server are not considered

## Possible Enhancements

Ideally, further requirements should be received to form a backlog. Once the end goal of the hypothetical project is known, a more optimal technical solution can planned and implemented.

The following tactical improvements could be made meanwhile:

* Come up with an approach to extend the number of best stories beyond 200, though this might not be possible with 100% accuracy (e.g. compared to source ranking that takes into account downvotes)
* Improve unit, integration and load test coverage
* Introduce/evolve a domain layer
* Implement more robust API versioning using https://github.com/dotnet/aspnet-api-versioning
* Handle possible error states of source API and map them accordingly
* Add "anticorruption layer" in front of source API to validate source DTOs
* Consider using additional third-party packages depending on team's preferences (FluentValidation, AutoMapper, Autofac, Polly)
* Depending on consistency requirements consider more sophisticated optimisation, e.g. updating more recent best stories more frequently
* Carefully consider additional web-scraping from the original source: https://news.ycombinator.com/best?p=

Additional strategic improvements:

* Introduce authentication if needed
* Clarify target client usage and create a sample client code / application
* Introduce continuous integration
* Clarify SLA and introduce persistent cache
* Add structured logging and performance metrics capture
* Introduce notification mechanism for pushing updates

## Action Screenshots

#### Running on host machine:
![Screenshot - running dotnet on host machine](/doc/action_dotnet_host.png?raw=true "Running on host machine")

#### Running in a container:
![Screenshot - container running in Docker Desktop](/doc/action_docker_desktop.png?raw=true "Running in a container")

#### Invoking API endpoint:
![Screenshot - invoking API endpoint via Swagger UI](/doc/action_swagger_ui.png?raw=true "Invoking API endpoint")

## Internal Architecture

![Screenshot - diagram of main types and dependencies between them](/doc/main_type_dependencies.png?raw=true "Main types and dependencies")