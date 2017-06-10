using System;
using System.Windows.Forms;

namespace ID3_and_EXIF_Viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string fileName;

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "аудио файлы (*.mp3)|*.mp3";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (openFileDialog.OpenFile() != null)
                    {
                        foreach (string path in openFileDialog.FileNames)
                        {
                            TagLib.File file = TagLib.File.Create(path);

                            fileName = path;

                            string[] artist = file.Tag.Performers;

                            string album = file.Tag.Album;
                            string title = file.Tag.Title;
                            string[] genres = file.Tag.Genres;
                            string year = file.Tag.Year.ToString();
                            string length = file.Properties.Duration.ToString();

                            foreach (string art in artist)
                                textBox1.Text = art;

                            textBox2.Text = album;
                            textBox3.Text = title;

                            foreach (string genre in genres)
                                textBox4.Text = genre;

                            textBox5.Text = year;
                            textBox6.Text = length;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Файл не существует!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка чтения файла!");
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private void showSaveButton()
        {
            buttonSave.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            showSaveButton();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            showSaveButton();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            showSaveButton();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            showSaveButton();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            showSaveButton();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string artist = textBox1.Text;
            string album = textBox2.Text;
            string title = textBox3.Text;
            string genre = textBox4.Text;
            string year = textBox5.Text;
            string duration = textBox6.Text;


            MessageBox.Show(fileName);
            if (fileName != null)
            {
                try
                {
                    TagLib.File file = TagLib.File.Create(fileName);
                    file.Tag.Year = Convert.ToUInt32(year);
                    file.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка сохранения");
                }
            }
            else
            {
                MessageBox.Show(fileName);
            }

           
        }
    }
}
