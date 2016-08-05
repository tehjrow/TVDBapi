using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net.Http.Headers;

namespace TVDBapi
{
    public class TVDB
    {        
        private Token _token;           
        //This is public for testing     
        public string tokenString;    
           
        
        

        /// <summary>
        /// Sets the token that gets sent in all requests
        /// </summary>
        /// <param name="ApiKey">APIKEY you get from the TVDB site</param>
        /// <returns></returns>
        public async Task SetTokenFromApikey(string ApiKey)
        {
            using (HttpClient client = new HttpClient())
            {
                //Url to TVDB
                Uri url = new Uri("https://api.thetvdb.com/login");
                
                //Set Accept request header
                client.DefaultRequestHeaders                    
                      .Add("Accept", "application/json");                

                //Setup request message with json apikey and content-type header
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent("{\"apiKey\":\""+ ApiKey +"\"}",
                                                    Encoding.UTF8,
                                                    "application/json");

                //Send via post, get response, read content into string
                HttpResponseMessage resp = await client.SendAsync(request);
                string respString = await resp.Content.ReadAsStringAsync();

                //Read into byte array and them create a memory stream
                byte[] byteArray = Encoding.UTF8.GetBytes(respString);
                MemoryStream stream1 = new MemoryStream(byteArray);

                //Create serializer and read the stream into a Token object
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Token));
                _token = serializer.ReadObject(stream1) as Token;

                //Set global token for debugging
                tokenString = _token.token;
            }
        }

        /// <summary>
        /// Search for a show
        /// </summary>
        /// <param name="nameToSearch">Name of show to search for</param>
        /// <returns>Returns ShowData which contains a list of Show (.data)</returns>
        public async Task<ShowData> Search(string nameToSearch)
        {
            using (HttpClient client = new HttpClient())
            {
                //Put token in header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.token);

                //Url to series search with nametosearch
                Uri url = new Uri("https://api.thetvdb.com/search/series?name=" + nameToSearch);

                //Set Accept request header
                client.DefaultRequestHeaders
                      .Add("Accept", "application/json");

                //Send via get, get response, read it into a string
                HttpResponseMessage resp = await client.GetAsync(url);
                string respString = await resp.Content.ReadAsStringAsync();

                //Read into byte array and them create a memory stream
                byte[] byteArray = Encoding.UTF8.GetBytes(respString);
                MemoryStream stream1 = new MemoryStream(byteArray);

                //Create serializer and read the stream into a ShowData object
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ShowData));
                ShowData showData = serializer.ReadObject(stream1) as ShowData;

                //Return ShowData object
                return showData;
            }
        }

        /// <summary>
        /// Gets info about a series using the id (foubd by searching with Search())
        /// </summary>
        /// <param name="id">TVDB show id</param>
        /// <returns>SeriesData containing the series</returns>
        public async Task<SeriesData> GetSeriesById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                //Put token in header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.token);

                //Url to series info with id
                Uri url = new Uri("https://api.thetvdb.com/series/" + id);

                //Set Accept request header
                client.DefaultRequestHeaders
                      .Add("Accept", "application/json");

                //Send via get, get response, read it into a string
                HttpResponseMessage resp = await client.GetAsync(url);
                string respString = await resp.Content.ReadAsStringAsync();

                //Read into byte array and them create a memory stream
                byte[] byteArray = Encoding.UTF8.GetBytes(respString);
                MemoryStream stream1 = new MemoryStream(byteArray);

                //Create serializer and read the stream into a SeriesData object
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SeriesData));
                SeriesData seriesData = serializer.ReadObject(stream1) as SeriesData;

                //Return SeriesData object
                return seriesData;
            }
        }        
    }
}
