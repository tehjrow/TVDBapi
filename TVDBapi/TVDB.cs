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
        //private string token;
        private Token token;
        private string apiKey;
        
        public TVDB(string apiKey)
        {
            this.apiKey = apiKey;
        }

        


        private async Task<Token> getToken(string ApiKey)
        {
            token = await "https://api.thetvdb.com/login"
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(new { apikey = ApiKey })
                //.ReceiveString();
                .ReceiveJson<Token>();
            return token; 
                     
        }

        public async Task<ShowData> Search()
        {
            await getToken(apiKey);

            ShowData showData;
            try
            {
                showData = await "https://api.thetvdb.com/search/series?name=battlestar"
                    .WithHeader("Accept", "application/json")
                    .WithHeader("Authorization", " Bearer " + this.token.token)
                    .GetAsync()
                    .ReceiveJson<ShowData>();
                    
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.Response != null)                    
                    throw ex;
                else
                    throw ex;
            }

            return showData;
        }

    }
}
