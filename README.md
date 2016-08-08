# TVDBapi
This is a C# library that uses the TVDB api to get information about TV Shows.

##Get
Nuget!  https://www.nuget.org/packages/TVDBapi.dll

##Usage

First set the token with an apikey provided by TVDB

```csharp
TVDB tvdb = new TVDB();
await tvdb.SetTokenFromApiKey("apikeyhere");
```

Then you can use Search() to find a show and return `ShowData` which contains a List<Show> called `data` of shows that matched

```csharp
ShowData showResults = await tvdb.Search("battlestar");
```
![alt tag](https://github.com/tehjrow/TVDBapi/blob/master/TVDBapi/Images/ShowData.PNG)
The search results contain an id which can then be used to get details about a specific show

```csharp
SeriesData seriesData = await tvdb.GetSeriesById(id);
```

