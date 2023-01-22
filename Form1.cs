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

            // connect list to the grid view control
            albumBindingSource.DataSource = albumsDAO.getAllAlbums();
            
            dataGridView1.DataSource = albumBindingSource;
        }
    }
}