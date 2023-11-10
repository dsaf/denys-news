# Developer Coding Test

## How to Run

### Prerequisites

* git https://git-scm.com/downloads
* .NET 7 https://dotnet.microsoft.com/en-us/download/dotnet/7.0

### How to run if you are using an IDE, e.g. Visual Studio 2022

* Clone the repository: https://github.com/dsaf/denys-news.git.
* Ensure correct start up project is selected: Denys.News.Api.
* Select the desired debug target: http, https, Docker etc.
* Run/debug the API.
* A Swagger UI page should open up automatically in your default browser: http://localhost:5073/swagger .
* Alternatively, you can invoke the endpoint directly using your preferred tool: http://localhost:5073/api/v1/stories?n=10 .

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

TODO

## Possible Enhancements

TODO