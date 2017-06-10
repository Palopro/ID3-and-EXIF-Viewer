using System;
using System.Windows.Forms;
using TagLib;

namespace ID3_and_EXIF_Viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "аудио файлы (*.mp3)|*.mp3";
            openFileDialog.RestoreDirectory = true;

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (openFileDialog.OpenFile() != null)
                    {
                        string path=openFileDialog.FileName.ToString();
                        TagLib.File file = TagLib.File.Create(path);
                        String[] artist = file.Tag.Performers;
                        String album = file.Tag.Album;
                        String title = file.Tag.Title;
                        String[] genres = file.Tag.Genres;
                        String year = file.Tag.Year.ToString();
                        
                        String length = file.Properties.Duration.ToString();

                        foreach ( string art in artist)
                            textBox1.Text = art;

                        textBox2.Text = album;
                        textBox3.Text = title;

                        foreach (string genre in genres)
                            textBox4.Text = genre;

                        textBox5.Text = year;
                        textBox6.Text = length;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка чтения файла!");
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}
