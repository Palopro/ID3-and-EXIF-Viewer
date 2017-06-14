using System;
using System.Windows.Forms;
using System.Drawing;
using Mp3Lib;

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
                    System.IO.Stream fileStream = openFileDialog.OpenFile();

                    using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                    {
                        foreach (string path in openFileDialog.FileNames)
                        {
                            Mp3File file = new Mp3File(path);

                            fileName = path;

                            string artist = file.TagHandler.Artist;
                            string album = file.TagHandler.Album;
                            string song = file.TagHandler.Song;
                            string num = file.TagHandler.Track;
                            string genres = file.TagHandler.Genre;
                            string year = file.TagHandler.Year;
                            Image thumb = file.TagHandler.Picture;

                            textBox1.Text = artist;
                            textBox2.Text = album;
                            textBox3.Text += song;
                            textBox4.Text = num;
                            textBox5.Text = genres;
                            textBox6.Text = year;

                            pictureBox1.Image = thumb;
                        }
                    }

                    fileStream.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка чтения файла!");
                }
            }
            else
            {
                MessageBox.Show("Ошибка чтения информации");
            }
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

            label7.Visible = true;
            label7.Text = "ID3";
 
            pictureBox1.Visible = true;
            pictureBox1.Size = new Size(120,120);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
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
            if (fileName != null)
            {
                Mp3File targetFile = new Mp3File(@fileName);

                try
                {
                    string artist = textBox1.Text;
                    string album = textBox2.Text;
                    string song = textBox3.Text;
                    string num = textBox4.Text;
                    string genre = textBox5.Text;
                    string year = textBox6.Text;

                    targetFile.TagHandler.Artist = artist;
                    targetFile.TagHandler.Album = album;
                    targetFile.TagHandler.Song = song;
                    targetFile.TagHandler.Track = num;
                    targetFile.TagHandler.Genre = genre;
                    targetFile.TagHandler.Year = year;
                    
                    targetFile.UpdatePacked();
                    MessageBox.Show("Сохранено");
                }
                catch
                {
                    MessageBox.Show("Ошибка сохранения");
                }
            }
            else
            {
                MessageBox.Show("Нет файла");
            }     
        }


        private void открытьToolStripMenuItem1_Click(object sender, EventArgs e)
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
                //код 
                
            }
            else
            {
                MessageBox.Show("Ошибка чтения информации");
            }
        }

    }
}