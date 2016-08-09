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
        public float? dvdEpisodeNumber { get; set; }
        public int? dvdSeason { get; set; }
        public string episodeName { get; set; }
        public string firstAired { get; set; }
        public int id { get; set; }
        public string overview { get; set; }
    }

    public class Links
    {        
        public int? first { get; set; }
        public int? last { get; set; }
        public int? next { get; set; }
        public int? prev { get; set; }
    }

    public class EpisodeData
    {
        public EpisodeData()
        {
            links = new Links();
        }

        public List<Episode> data { get; set; }
        public Links links { get; set; }
    }
}
