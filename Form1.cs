using System.Windows.Forms;

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

        private void button1_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();

            albums = albumsDAO.getAllAlbums();

            albumBindingSource.DataSource = albums;
            
            dataGridView1.DataSource = albumBindingSource;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();

            albumBindingSource.DataSource = albumsDAO.searchTitles(textBox1.Text);

            dataGridView1.DataSource = albumBindingSource;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            int rowClicked = dataGridView.CurrentRow.Index;
            String imageURL = dataGridView.Rows[rowClicked].Cells[4].Value.ToString();

            pictureBox1.Load(imageURL);

            trackBindingSource.DataSource = albums[rowClicked].Tracks;

            dataGridView2.DataSource = trackBindingSource;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

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

        private void button4_Click(object sender, EventArgs e)
        {
            int rowClicked = dataGridView2.CurrentRow.Index;
            MessageBox.Show("You have selected row" + rowClicked);
            int trackID = (int) dataGridView2.Rows[rowClicked].Cells[0].Value;
            MessageBox.Show("You have selected track" + trackID);

            AlbumsDAO albumsDAO = new AlbumsDAO();

            int result = albumsDAO.deleteTrack(trackID);
            MessageBox.Show("You have deleted " + result);

            dataGridView2.DataSource = null;
            albums = albumsDAO.getAllAlbums();

        }
    }
}