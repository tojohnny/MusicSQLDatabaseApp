namespace MusicSQLDatabaseApp
{
    internal class Track
    {
        public int trackId { get; set; }
        public string trackTitle { get; set; }
        public string trackUrl { get; set; }
        public string trackLyrics { get; set; }
        public int albumId { get; set; }

    }
}