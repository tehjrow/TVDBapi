using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVDBapi
{
    public class SeasonEpisodeSummary
    {
        public List<string> airedSeasons { get; set; }
        public string airedEpisodes { get; set; }
        public List<string> dvdSeasons { get; set; }
        public string dvdEpisodes { get; set; }
    }

    public class SeasonEpisodeSummaryData
    {
        public SeasonEpisodeSummary data { get; set; }
    }
}
