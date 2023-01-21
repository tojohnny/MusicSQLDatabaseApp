using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSQLDatabaseApp
{
    internal class Album
    {
        public int id { get; set; }
        public string albumTitle { get; set; }
        public string albumArtist { get; set; }
        public int releaseYear { get; set; }
        public string imageUrl { get; set; }
        public string albumGenre { get; set; }
    }
}
