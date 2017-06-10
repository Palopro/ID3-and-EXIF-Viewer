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

        private void Form1_Load(object sender, EventArgs e)
        {
            TagLib.File file = TagLib.File.Create("01. Take This Life.mp3");
            String title = file.Tag.Title;
            String [] artist = file.Tag.Performers;
            String album = file.Tag.Album;
            String length = file.Properties.Duration.ToString();

            label1.Text = title;
            label2.Text = album;
            label3.Text = length;
            for(int i=0; i<artist.Length; i++)
            {
                label4.Text = artist[i];
            }
            

        }
    }
}
