<img src="https://www.sympli.com.au/wp-content/uploads/sympli-logo-black.svg">

------------------------------------

# Web Scapper

This web scrapper is a solution to the problem stated below. The scrapper is pretty simple to use and returns the values as JSON objects.

## How To Launch Web Scrapper
Sympli (see what I did there !) clone this git repository and load the sln file into your visual studio and launch. If you like the terminal, then you can use :
```
git clone <URL>
cd SympliDevelopmentProject
dotnet run --project .\src\SympliDevelopment.Api\SympliDevelopment.Api.csproj
```
Then just launch [Postman](https://www.postman.com/), launch a workspace and set the method type to GET. In the URL pass `http://localhost:5180/search/keywords`. In the query params set key as `keywords` and in the values pass a comma separated list of key words you want to search for.

<img src=".\first-look.jpg">

### The call without caching the results
The call without caching takes considerably longer than the one with caching as can be seen from the screenshot below. 

<img src=".\without-caching.jpg">

### The call with caching
The call once we have cached results is much faster as can be seen from the screenshot below. 

<img src=".\caching.jpg">

## What I would add to improve the web scrapper
- This can be a useful tool in a lot of places where web scrapping may be beneficial so the generalized version would include flexibility around host address, and the endpoint being called (which is currently a hardcoded value within the system).
- Parsing and matching with a regular expression for the whole document returned by the httpClient is currently taking a long time (roughly 2 seconds). I would research and look into possible ways to make the parsing quicker.
- Make this a background service which runs at regular intervals throughout the day (configurable) and sends an email to the concerned personnel in the morning informing of previous days performance.

### PS: To add more search engines simply add their URL to the `appsettings.json` file under the key `SearchEngines`.
