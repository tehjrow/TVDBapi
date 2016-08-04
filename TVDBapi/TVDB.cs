using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVDBapi
{
    public class TVDB
    {        
        private Token token;           
        //This is public for testing     
        public string tokenString;    
            
        /// <summary>
        /// Sets the token that gets sent in all requests.
        /// </summary>
        /// <param name="ApiKey">APIKEY you get from the TVDB site.</param>
        /// <returns></returns>
        public async Task SetTokenFromApikey(string ApiKey)
        {
            token = await "https://api.thetvdb.com/login"
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(new { apikey = ApiKey })            
                .ReceiveJson<Token>();
            tokenString = token.token;
        }

        /// <summary>
        /// Search for a show.  Returns ShowData which contains a list of Shows (.data).
        /// </summary>
        /// <param name="nameToSearch">Name of show to search for.</param>
        /// <returns></returns>
        public async Task<ShowData> Search(string nameToSearch)
        {
            var url = "https://api.thetvdb.com/search/series";
            ShowData showData;

            try
            {
                showData = await url.SetQueryParam("name", nameToSearch)
                    .WithHeader("Accept", "application/json")
                    .WithHeader("Authorization", " Bearer " + token.token)
                    .GetAsync()
                    .ReceiveJson<ShowData>();
                    
            }
            catch (FlurlHttpException ex)
            {
                throw ex;
            }

            return showData;
        }

    }
}
