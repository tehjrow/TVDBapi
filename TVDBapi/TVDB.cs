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
        public string test;

        public async Task<string> getStuff()
        {
            var responseString = await "https://api.thetvdb.com/login"
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(new { apikey = "26EF26F60843B42C"})
                .ReceiveString();
            return responseString;
        }


    }
}
