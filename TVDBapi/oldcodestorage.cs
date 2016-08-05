using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVDBapi
{
    class oldcodestorage
    {

        //public async Task<SeriesData> GetSeriesById(int id)
        //{
        //    var url = "https://api.thetvdb.com/series";
        //    SeriesData seriesData;

        //    try
        //    {
        //        seriesData = await url
        //            .AppendPathSegment(id)
        //            .WithHeader("Accept", "application/json")
        //            .WithHeader("Authorization", " Bearer " + _token.token)
        //            .GetAsync()
        //            .ReceiveJson<SeriesData>();

        //    }
        //    catch (FlurlHttpException ex)
        //    {
        //        throw ex;
        //    }

        //    return seriesData;
        //}



        //public async Task SetTokenFromApikey(string ApiKey)
        //{
        //    try
        //    {
        //        _token = await "https://api.thetvdb.com/login"
        //            .WithHeader("Accept", "application/json")
        //            .PostJsonAsync(new { apikey = ApiKey })
        //            .ReceiveJson<Token>();
        //        tokenString = _token.token;
        //    }
        //    catch (FlurlHttpException ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<ShowData> Search(string nameToSearch)
        //{

        //    //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
        //    var url = "https://api.thetvdb.com/search/series";
        //    ShowData showData;

        //    try
        //    {
        //        showData = await url
        //            .SetQueryParam("name", nameToSearch)
        //            .WithHeader("Accept", "application/json")
        //            .WithHeader("Authorization", " Bearer " + _token.token)
        //            .GetAsync()
        //            .ReceiveJson<ShowData>();

        //    }
        //    catch (FlurlHttpException ex)
        //    {
        //        throw ex;
        //    }

        //    return showData;
        //}
    }
}
