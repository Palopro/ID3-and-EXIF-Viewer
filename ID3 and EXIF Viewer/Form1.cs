using System;
using System.Windows.Forms;
using System.Drawing;

namespace ID3_and_EXIF_Viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string fileName;

        private void ОткрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = "аудио файлы (*.mp3)|*.mp3",
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                SetVisibleTextBox();

                try
                {
                    if (openFileDialog.OpenFile() != null)
                    {
                        foreach (string path in openFileDialog.FileNames)
                        {
                            Mp3Lib.Mp3File file = new Mp3Lib.Mp3File(path);

                            fileName = path;
                   
                            string artist = file.TagHandler.Artist;
                            string album = file.TagHandler.Album;
                            string song = file.TagHandler.Song;
                            string num = file.TagHandler.Track;
                            string genres = file.TagHandler.Genre;
                            string year = file.TagHandler.Year;

                            textBox1.Text = artist;
                            textBox2.Text = album;
                            textBox3.Text += song;
                            textBox4.Text = num;
                            textBox5.Text = genres;
                            textBox6.Text = year;
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
            openFileDialog.Dispose();
        }

        private void SetVisibleTextBox()
        {
            label1.Visible = true;
            label1.Text = "Исполнитель";
            textBox1.Visible = true;

            label2.Visible = true;
            label2.Text = "Альбом";
            textBox2.Visible = true;

            label3.Visible = true;
            label3.Text = "Название";
            textBox3.Visible = true;

            label4.Visible = true;
            label4.Text = "Номер";
            textBox4.Visible = true;

            label5.Visible = true;
            label5.Text = "Жанр";
            textBox5.Visible = true;

            label6.Visible = true;
            label6.Text = "Год";
            textBox6.Visible = true;
        }

        private void ShowSaveButton()
        {
            buttonSave.Visible = true;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            ShowSaveButton();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            ShowSaveButton();
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            ShowSaveButton();
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            ShowSaveButton();
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            ShowSaveButton();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            string artist = textBox1.Text;
            string album = textBox2.Text;
            string title = textBox3.Text;
            string genre = textBox4.Text;
            string year = textBox5.Text;
 
            if (fileName != null)
            {
                try
                {
                    // TO-DO
                }
                catch
                {

                }
            }
            else
            {
                MessageBox.Show(fileName);
            }     
        }
    }
}
