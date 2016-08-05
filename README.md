# TVDBapi
This is a C# library that uses the TVDB api to get information about TV Shows.

##Usage

First set the token with an apikey provided by TVDB

```csharp
TVDB tvdb = new TVDB();
await tvdb.SetTokenFromApiKey("apikeyhere");
```
