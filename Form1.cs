namespace MusicSQLDatabaseApp
{
    public partial class Form1 : Form
    {
        BindingSource albumBindingSource = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();
            Album a1 = new Album()
            {
                id = 1,
                albumTitle = "first Album",
                albumArtist = "first album artist",
                releaseYear = 2023,
                imageUrl = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_light_color_272x92dp.png",
                albumGenre = "indie",
            };
            Album a2 = new Album()
            {
                id = 2,
                albumTitle = "second Album",
                albumArtist = "second album artist",
                releaseYear = 2023,
                imageUrl = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_light_color_272x92dp.png",
                albumGenre = "indie",
            };

            albumsDAO.albums.Add(a1);
            albumsDAO.albums.Add(a2);

            // connect list to the grid view control
            albumBindingSource.DataSource = albumsDAO.albums;
            
            dataGridView1.DataSource = albumBindingSource;
        }
    }
}