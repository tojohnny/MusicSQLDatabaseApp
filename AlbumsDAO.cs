using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace MusicSQLDatabaseApp
{
    internal class AlbumsDAO
    {
        string connectionString = "datasource=localhost;port=3306;username=root;password=root;database=music";

        public List<Album> getAllAlbums()
        {
            List<Album> returnAlbums = new List<Album>();

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT ID, ALBUM_TITLE, ALBUM_ARTIST, RELEASE_YEAR, IMAGE_URL, ALBUM_GENRE FROM ALBUMS";
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Album album = new Album
                    {
                        id = reader.GetInt32(0),
                        albumTitle = reader.GetString(1),
                        albumArtist = reader.GetString(2),
                        releaseYear = reader.GetInt32(3),
                        imageUrl = reader.GetString(4),
                        albumGenre = reader.GetString(5)
                    };
                    album.Tracks = getTracksForAlbum(album.id);

                    returnAlbums.Add(album);
                }
            }
            connection.Close();

            return returnAlbums;
        }

        public List<Album> searchTitles(string searchText) 
        {
            List<Album> returnAlbums = new List<Album>();

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string searchWildPhrase = "%" + searchText + "%";

            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT ID, ALBUM_TITLE, ALBUM_ARTIST, RELEASE_YEAR, IMAGE_URL, ALBUM_GENRE FROM ALBUMS WHERE ALBUM_TITLE LIKE @searchText";
            command.Parameters.AddWithValue("@searchText", searchWildPhrase);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Album album = new Album
                    {
                        id = reader.GetInt32(0),
                        albumTitle = reader.GetString(1),
                        albumArtist = reader.GetString(2),
                        releaseYear = reader.GetInt32(3),
                        imageUrl = reader.GetString(4),
                        albumGenre = reader.GetString(5)
                    };
                    returnAlbums.Add(album);
                }
            }
            connection.Close();

            return returnAlbums;
        }

        internal int addOneAlbum(Album album)
        {
            List<Album> returnAlbums = new List<Album>();

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.CommandText = "INSERT INTO `albums`(`ALBUM_TITLE`, `ALBUM_ARTIST`, `RELEASE_YEAR`, `IMAGE_URL`, `ALBUM_GENRE`)" +
                " VALUES (@albumtitle, @albumartist, @releaseyear, @imageurl, @albumgenre)";
            command.Parameters.AddWithValue("@albumtitle", album.albumTitle);
            command.Parameters.AddWithValue("@albumartist", album.albumArtist);
            command.Parameters.AddWithValue("@releaseyear", album.releaseYear);
            command.Parameters.AddWithValue("@imageurl", album.imageUrl);
            command.Parameters.AddWithValue("@albumgenre", album.albumGenre);
            command.Connection = connection;

            int newRows = command.ExecuteNonQuery();
            connection.Close();

            return newRows;

        }

        public List<Track> getTracksForAlbum(int albumId)
        {
            List<Track> returnTracks = new List<Track>();

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM TRACKS WHERE ALBUMS_ID=@albumid";
            command.Parameters.AddWithValue("@albumid", albumId);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Track track = new Track
                    {
                        id = reader.GetInt32(0),
                        trackId = reader.GetInt32(1),
                        trackTitle = reader.GetString(2),
                        trackUrl = reader.GetString(3),
                        trackLyrics = reader.GetString(4),
                        albumId = reader.GetInt32(5)
                    };
                    returnTracks.Add(track);
                }
            }
            connection.Close();

            return returnTracks;
        }

        internal int deleteTrack(int trackID)
        {
            List<Album> returnAlbums = new List<Album>();

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();

            command.CommandText = "DELETE FROM TRACKS WHERE TRACKS.ID = @trackid;";
            command.Parameters.AddWithValue("@trackid", trackID);

            command.Connection = connection;

            int result = command.ExecuteNonQuery();
            connection.Close();

            return result;
        }

        internal int deleteAlbum(int albumID)
        {
            int result = 0;
            try
            {
                List<Album> returnAlbums = new List<Album>();

                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlCommand command = new MySqlCommand();

                command.CommandText = "DELETE FROM ALBUMS WHERE ID = @albumid;";
                command.Parameters.AddWithValue("@albumid", albumID);

                command.Connection = connection;

                result = command.ExecuteNonQuery();
                connection.Close();

                return result;

            } 
            catch (Exception ForeignKeyConstraint) 
            {
                MessageBox.Show("Album is not empty, deletion canceled.");
            }

            return result;
        }

        internal int addOneTrack(Track track)
        {
            List<Track> returnTracks = new List<Track>();

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.CommandText = "INSERT INTO `tracks`(`TRACK_ID`, `TRACK_TITLE`, `TRACK_URL`, `TRACK_LYRICS`, `albums_ID`)" +
                " VALUES (@trackid, @tracktitle, @trackurl, @tracklyrics, @albumid)";
            command.Parameters.AddWithValue("@trackid", track.trackId);
            command.Parameters.AddWithValue("@tracktitle", track.trackTitle);
            command.Parameters.AddWithValue("@trackurl", track.trackUrl);
            command.Parameters.AddWithValue("@tracklyrics", track.trackLyrics);
            command.Parameters.AddWithValue("@albumid", track.albumId);
            command.Connection = connection;

            int newRows = command.ExecuteNonQuery();
            connection.Close();

            return newRows;
        }

        internal int updateOneAlbum(Album album)
        {
            List<Album> returnAlbums = new List<Album>();

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.CommandText = "UPDATE albums SET ALBUM_TITLE = @albumtitle, ALBUM_ARTIST = @albumartist," +
                "RELEASE_YEAR = @releaseyear, IMAGE_URL = @imageurl, ALBUM_GENRE = @albumgenre WHERE ID = @albumid";
            command.Parameters.AddWithValue("@albumtitle", album.albumTitle);
            command.Parameters.AddWithValue("@albumartist", album.albumArtist);
            command.Parameters.AddWithValue("@releaseyear", album.releaseYear);
            command.Parameters.AddWithValue("@imageurl", album.imageUrl);
            command.Parameters.AddWithValue("@albumgenre", album.albumGenre);
            command.Parameters.AddWithValue("@albumid", album.id);
            command.Connection = connection;

            int newRows = command.ExecuteNonQuery();
            connection.Close();

            return newRows;
        }

        internal int updateOneTrack(Track track)
        {
            List<Track> returnTrack = new List<Track>();

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.CommandText = "UPDATE tracks SET TRACK_ID = @trackid, TRACK_TITLE = @tracktitle," +
                "TRACK_URL = @trackurl, TRACK_LYRICS = @tracklyrics, ALBUMS_ID = @albumsid WHERE ID = @id";
            command.Parameters.AddWithValue("@trackid", track.trackId);
            command.Parameters.AddWithValue("@tracktitle", track.trackTitle);
            command.Parameters.AddWithValue("@trackurl", track.trackUrl);
            command.Parameters.AddWithValue("@tracklyrics", track.trackLyrics);
            command.Parameters.AddWithValue("@albumsid", track.albumId);
            command.Parameters.AddWithValue("@id", track.id);
            command.Connection = connection;

            int newRows = command.ExecuteNonQuery();
            connection.Close();

            return newRows;
        }
    }
}
