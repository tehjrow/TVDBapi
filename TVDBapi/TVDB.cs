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
        private Token _token = new Token();  
                 
        //This is public for testing     
        public string tokenString;

        //New "Constructor" because constructors can't be async
        /// <summary>
        /// Initialize library
        /// </summary>
        /// <param name="apiKey">APIKEY from TVDB</param>
        /// <returns></returns>
        public static async Task<TVDB> Init(string apiKey)
        {
            TVDB tvdb = new TVDB();
            tvdb._token.apiKey = apiKey;
            await tvdb.SetToken(tvdb._token.apiKey);
            return tvdb;

        }

        private TVDB()
        {
        }


        //Sets the token from the apikey from tvdb
        private async Task SetToken(string ApiKey)
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
                request.Content = new StringContent("{\"apiKey\":\"" + ApiKey + "\"}",
                                                    Encoding.UTF8,
                                                    "application/json");

                //Send via post, get response, read content into string, check to be sure it was OK
                HttpResponseMessage resp = await client.SendAsync(request);
                string respString = await resp.Content.ReadAsStringAsync();
                if(resp.ReasonPhrase != "OK")
                {
                    throw new Exception(resp.ReasonPhrase);
                }

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
        /// Searches TVDB
        /// </summary>
        /// <param name="nameToSearch">Name of show to search for</param>
        /// <returns>ShowData object containing a List<Show> inside .data</returns>
        public async Task<ShowData> Search(string nameToSearch)
        {
            //Url to series search with nametosearch
            Uri url = new Uri("https://api.thetvdb.com/search/series?name=" + nameToSearch);

            MemoryStream stream1 = await _GetStreamFromUrl(url);
            //Create serializer and read the stream into a ShowData object
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ShowData));
            ShowData showData = serializer.ReadObject(stream1) as ShowData;

            //Return ShowData object
            return showData;
        }


        /// <summary>
        /// Gets info about a series using the id (foubd by searching with Search())
        /// </summary>
        /// <param name="id">TVDB show id</param>
        /// <returns>SeriesData containing the series</returns>
        public async Task<SeriesData> GetSeriesById(int id)
        {            
            //Url to series info with id
            Uri url = new Uri("https://api.thetvdb.com/series/" + id);

            MemoryStream stream1 = await _GetStreamFromUrl(url);
            //Create serializer and read the stream into a SeriesData object
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SeriesData));
            SeriesData seriesData = serializer.ReadObject(stream1) as SeriesData;

            //Return SeriesData object
            return seriesData;
        }

        /// <summary>
        /// Gets a summary of the show (number of episodes and seasons)
        /// </summary>
        /// <param name="id">TVDB show id</param>
        /// <returns>SeasonEpisodeSummaryData containing .data</returns>
        public async Task<SeasonEpisodeSummaryData> GetSeriesSummary(int id)
        {       
            //Url to series info with id
            Uri url = new Uri("https://api.thetvdb.com/series/" + id + "/episodes/summary");

            MemoryStream stream1 = await _GetStreamFromUrl(url);
            //Create serializer and read the stream into a SeriesData object
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SeasonEpisodeSummaryData));
            SeasonEpisodeSummaryData seriesEpisodeSummary = serializer.ReadObject(stream1) as SeasonEpisodeSummaryData;
            
            //Return SeriesData object
            return seriesEpisodeSummary;            
        }
        
        /// <summary>
        /// Gets a list of all episodes from a tv show
        /// </summary>
        /// <param name="id">TVDB show id</param>
        /// <returns>EpisodeData containing .data with a list of all episodes</returns>
        public async Task<EpisodeData> GetEpisodes(int id)
        {
            //Url to series info with id
            Uri url = new Uri("https://api.thetvdb.com/series/" + id + "/episodes");

            MemoryStream stream1 = await _GetStreamFromUrl(url);
            //Create serializer and read the stream into a SeriesData object
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(EpisodeData));
            EpisodeData episodeData = serializer.ReadObject(stream1) as EpisodeData;

            if(episodeData.links.last != 1)
            {
                EpisodeData episodeDataExtra = new EpisodeData();
                episodeDataExtra.links.next = episodeData.links.next;
                while (episodeDataExtra.links.next != null)
                {                    
                    url = new Uri("https://api.thetvdb.com/series/" + id + "/episodes?page=" + episodeDataExtra.links.next);
                    stream1 = await _GetStreamFromUrl(url);
                    serializer = new DataContractJsonSerializer(typeof(EpisodeData));
                    episodeDataExtra = serializer.ReadObject(stream1) as EpisodeData;
                    episodeData.data.AddRange(episodeDataExtra.data);
                }
                
            }

            //Return SeriesData object
            return episodeData;
        }

        //Returns a stream based on url (or throws response message if failed)
        private async Task<MemoryStream> _GetStreamFromUrl(Uri url)
        {
            using (HttpClient client = new HttpClient())
            {
                //Put token in header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.token);

                //Set Accept request header
                client.DefaultRequestHeaders
                      .Add("Accept", "application/json");

                //Send via get, get response, read it into a string
                HttpResponseMessage resp = await client.GetAsync(url);
                string respString = await resp.Content.ReadAsStringAsync();
                if (resp.ReasonPhrase != "OK")
                {
                    throw new Exception(resp.ReasonPhrase);
                }

                //Read into byte array and them create a memory stream
                byte[] byteArray = Encoding.UTF8.GetBytes(respString);
                MemoryStream stream1 = new MemoryStream(byteArray);

                return stream1;                
            }
        }
                        
    }
}
