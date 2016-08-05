# TVDBapi
This is a C# library that uses the TVDB api to get information about TV Shows.

##Usage

First set the token with an apikey provided by TVDB

```csharp
TVDB tvdb = new TVDB();
await tvdb.SetTokenFromApiKey("apikeyhere");
```

Then you can use Search() to find a show and return a list of shows that matched

```csharp
ShowData showResults = await tvdb.Search("battlestar");
```
