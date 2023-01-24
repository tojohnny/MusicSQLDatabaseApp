using MySqlX.XDevAPI.Common;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace MusicSQLDatabaseApp
{
    public partial class Form1 : Form
    {
        BindingSource albumBindingSource = new BindingSource();
        BindingSource trackBindingSource = new BindingSource();

        List<Album> albums = new List<Album>();

        public Form1()
        {
            InitializeComponent();
        }

        // Load albums
        private void button1_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();

            albums = albumsDAO.getAllAlbums();

            albumBindingSource.DataSource = albums;
            
            dataGridView1.DataSource = albumBindingSource;
            dataGridView1.Columns[0].HeaderText = "Album ID";
            dataGridView1.Columns[1].HeaderText = "Album Title";
            dataGridView1.Columns[2].HeaderText = "Album Artist";
            dataGridView1.Columns[3].HeaderText = "Release Year";
            dataGridView1.Columns[4].HeaderText = "Cover Art Image URL";
            dataGridView1.Columns[5].HeaderText = "Genre";

        }

        // Search titles
        private void button2_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();

            albumBindingSource.DataSource = albumsDAO.searchTitles(textBox1.Text);

            dataGridView1.DataSource = albumBindingSource;
        }

        // Grab and display information when navigating Album Database
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            int rowClicked = dataGridView.CurrentRow.Index;
            var valueRetrieved = dataGridView.Rows[rowClicked].Cells[4].Value;
            string imageURL = null;

            if (valueRetrieved != null)
            {
                imageURL = valueRetrieved.ToString();

                // Load album text box
                int albumID = (int)dataGridView1.Rows[rowClicked].Cells[0].Value;
                string albumTitle = (string)dataGridView1.Rows[rowClicked].Cells[1].Value;
                string albumArtist = (string)dataGridView1.Rows[rowClicked].Cells[2].Value;
                int releaseYear = (int)dataGridView1.Rows[rowClicked].Cells[3].Value;
                string imageUrl = (string)dataGridView1.Rows[rowClicked].Cells[4].Value;
                string albumGenre = (string)dataGridView1.Rows[rowClicked].Cells[5].Value;

                textBox2.Text = albumTitle.ToString();
                textBox3.Text = albumArtist.ToString();
                textBox4.Text = releaseYear.ToString();
                textBox5.Text = imageUrl.ToString();
                textBox6.Text = albumGenre.ToString();

                // Load picture
                pictureBox1.Load(imageURL);

                trackBindingSource.DataSource = albums[rowClicked].Tracks;

                dataGridView2.DataSource = trackBindingSource;
            }
            else
            {
                MessageBox.Show("This album does not exist, try another album.");
            }

            //dataGridView2.Columns[0].HeaderText = "Track ID";
            dataGridView2.Columns[0].HeaderText = "Track #";
            dataGridView2.Columns[1].HeaderText = "Track Title";
            dataGridView2.Columns[2].HeaderText = "YouTube URL";
            dataGridView2.Columns[3].HeaderText = "Track Lyrics";
            dataGridView2.Columns[4].HeaderText = "Album ID";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // delete later
        }

        // Add new album.
        private void button3_Click(object sender, EventArgs e)
        {
            Album album = new Album
            {
                albumTitle = textBox2.Text,
                albumArtist = textBox3.Text,
                releaseYear = Int32.Parse(textBox4.Text),
                imageUrl = textBox5.Text,
                albumGenre = textBox6.Text,
            };

            AlbumsDAO albumsDAO = new AlbumsDAO();
            int result = albumsDAO.addOneAlbum(album);
            MessageBox.Show("Album has been added.");
        }

        // Delete track
        private void button4_Click(object sender, EventArgs e)
        {
            int rowClicked = dataGridView2.CurrentRow.Index;
            int trackID = (int) dataGridView2.Rows[rowClicked].Cells[0].Value;

            AlbumsDAO albumsDAO = new AlbumsDAO();

            DialogResult dialogResult = MessageBox.Show(
                "You have selected track " + trackID + ".\n" +
                "Are you sure you want to delete the current selection?",
                "Delete Confirmation", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                int result = albumsDAO.deleteTrack(trackID);
                MessageBox.Show("You have deleted " + result + ".");

                dataGridView2.DataSource = null;
                albums = albumsDAO.getAllAlbums();
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("You have canceled deletion of track " + trackID + ".");
            }
        }

        // Empty label
        private void label9_Click(object sender, EventArgs e)
        {
            // delete later
        }

        // Delete album
        private void button5_Click(object sender, EventArgs e)
        {
            int rowClicked = dataGridView1.CurrentRow.Index;
            int albumID = (int)dataGridView1.Rows[rowClicked].Cells[0].Value;

            AlbumsDAO albumsDAO = new AlbumsDAO();

            DialogResult dialogResult = MessageBox.Show(
                "You have selected album " + albumID + ".\n" +
                "Are you sure you want to delete the current selection?",
                "Delete Confirmation", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                int result = albumsDAO.deleteAlbum(albumID);
                MessageBox.Show("You have deleted " + result + ".");

                dataGridView1.DataSource = null;
                albums = albumsDAO.getAllAlbums();
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("You have canceled deletion of album " + albumID + ".");
            }
        }

        // Add new track
        private void button6_Click(object sender, EventArgs e)
        {
            Track track = new Track
            {
                trackId = Int32.Parse(textBox7.Text),
                trackTitle = textBox8.Text,
                trackUrl = textBox9.Text,
                trackLyrics = textBox10.Text,
                albumId = Int32.Parse(textBox11.Text),
            };

            AlbumsDAO albumsDAO = new AlbumsDAO();
            int result = albumsDAO.addOneTrack(track);
            MessageBox.Show("Track has been added.");
        }

        // Update album
        private void button7_Click(object sender, EventArgs e)
        {
            int rowClicked = dataGridView1.CurrentRow.Index;
            int albumID = (int)dataGridView1.Rows[rowClicked].Cells[0].Value;

            Album album = new Album
            {
                albumTitle = textBox2.Text,
                albumArtist = textBox3.Text,
                releaseYear = Int32.Parse(textBox4.Text),
                imageUrl = textBox5.Text,
                albumGenre = textBox6.Text,
                id = albumID,
            };

            AlbumsDAO albumsDAO = new AlbumsDAO();
            int result = albumsDAO.updateOneAlbum(album);
            MessageBox.Show("Album has been updated.");
        }

        // Grab and display information when navigating track database
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            int rowClicked = dataGridView2.CurrentRow.Index;
            string imageURL = dataGridView2.Rows[rowClicked].Cells[4].Value.ToString();

            // Load album text box
            int trackId = (int)dataGridView2.Rows[rowClicked].Cells[1].Value;
            string trackTitle = (string)dataGridView2.Rows[rowClicked].Cells[2].Value;
            string trackUrl = (string)dataGridView2.Rows[rowClicked].Cells[3].Value;
            string trackLyrics = (string)dataGridView2.Rows[rowClicked].Cells[4].Value;
            int albumId = (int)dataGridView2.Rows[rowClicked].Cells[5].Value;

            textBox7.Text = trackId.ToString();
            textBox8.Text = trackTitle.ToString();
            textBox9.Text = trackUrl.ToString();
            textBox10.Text = trackLyrics.ToString();
            textBox11.Text = albumId.ToString();
        }

        // Update tracks
        private void button8_Click(object sender, EventArgs e)
        {
            int rowClicked = dataGridView2.CurrentRow.Index;
            int trackID = (int)dataGridView2.Rows[rowClicked].Cells[0].Value;

            Track track = new Track
            {
                trackId = Int32.Parse(textBox7.Text),
                trackTitle = textBox8.Text,
                trackUrl = textBox9.Text,
                trackLyrics = textBox10.Text,
                albumId = Int32.Parse(textBox11.Text),
                id = trackID,
            };

            AlbumsDAO albumsDAO = new AlbumsDAO();
            int result = albumsDAO.updateOneTrack(track);
            MessageBox.Show("Track has been updated.");
        }
    }
}