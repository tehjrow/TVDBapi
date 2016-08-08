using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVDBapi
{
    public class Episode
    {
        public int? absoluteNumber { get; set; }
        public int airedEpisodeNumber { get; set; }
        public int airedSeason { get; set; }
        public int airedSeasonID { get; set; }
        public int? dvdEpisodeNumber { get; set; }
        public int? dvdSeason { get; set; }
        public string episodeName { get; set; }
        public string firstAired { get; set; }
        public int id { get; set; }
        public string overview { get; set; }
    }

    public class EpisodeData
    {
        public List<Episode> data { get; set; }
    }
}
