# TVDBapi
This is a C# library that uses the TVDB api to get information about TV Shows.

##Get
Nuget!  https://www.nuget.org/packages/TVDBapi.dll

##Usage

First set the token with an apikey provided by TVDB

| Command | Description |
| ------- | ----------- |
| `TVDB tvdb = new TVDB();` | Create new instance |
| `await tvdb.SetTokenFromApiKey("apikeyhere");` | Set token to allow TVDB usage |
| `ShowData showResults = await tvdb.Search(string);` | Search the TVDB and return a List<Show> of shows |
| `SeriesData seriesData = await tvdb.GetSeriesById(id);` | Get details about a show based on the `id` returned from `Search()` |
| `SeasonEpisodeSummaryData summaryData = await tvdb.GetSeriesSummary(id);` | Get a summary of seasons/episods of a show by `id` |


##Examples

```csharp
ShowData showResults = await tvdb.Search("battlestar");
```
![alt tag](https://github.com/tehjrow/TVDBapi/blob/master/TVDBapi/Images/ShowData.PNG)


```csharp
SeriesData seriesData = await tvdb.GetSeriesById(78874);
```
![alt tag](https://github.com/tehjrow/TVDBapi/blob/master/TVDBapi/Images/SeriesData.PNG)
