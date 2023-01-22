using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
