﻿using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using ExifLib;
using Mp3Lib;

namespace ID3_and_EXIF_Viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private string _fileName;

        private void ОткрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = @"аудио файлы (*.mp3)|*.mp3",
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                label7.Text = openFileDialog.FileName;
                SetId();

                try
                {
                    var fileStream = openFileDialog.OpenFile();

                    using (new StreamReader(fileStream))
                    {
                        foreach (var path in openFileDialog.FileNames)
                        {
                            var file = new Mp3File(path);

                            _fileName = path;

                            var artist = file.TagHandler.Artist;
                            var album = file.TagHandler.Album;
                            var song = file.TagHandler.Song;
                            var num = file.TagHandler.Track;
                            var genres = file.TagHandler.Genre;
                            var year = file.TagHandler.Year;
                            var thumb = file.TagHandler.Picture;

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
                    MessageBox.Show(@"Ошибка чтения файла!");
                }
            }
            else
            {
                MessageBox.Show(@"Ошибка чтения информации");
            }
        }

        private void SetId()
        {
            label1.Visible = true;
            label1.Text = @"Исполнитель";
            textBox1.Visible = true;
            textBox1.Clear();

            label2.Visible = true;
            label2.Text = @"Альбом";
            textBox2.Visible = true;
            textBox2.Clear();

            label3.Visible = true;
            label3.Text = @"Название";
            textBox3.Visible = true;
            textBox3.Clear();

            label4.Visible = true;
            label4.Text = @"Номер";
            textBox4.Visible = true;
            textBox4.Clear();

            label5.Visible = true;
            label5.Text = @"Жанр";
            textBox5.Visible = true;
            textBox5.Clear();

            label6.Visible = true;
            label6.Text = @"Год";
            textBox6.Visible = true;
            textBox6.Clear();

            label7.Visible = true;

            pictureBox1.Visible = true;
            pictureBox1.Size = new Size(128, 128);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void SetExif()
        {
            label1.Visible = true;
            label1.Text = @"Производитель";
            textBox1.Visible = true;
            textBox1.Clear();

            label2.Visible = true;
            label2.Text = @"Устройство";
            textBox2.Visible = true;
            textBox2.Clear();

            label3.Visible = true;
            label3.Text = @"Камера";
            textBox3.Visible = true;
            textBox3.Clear();

            label4.Visible = true;
            label4.Text = @"Дата и время";
            textBox4.Visible = true;
            textBox4.Clear();

            label5.Visible = true;
            label5.Text = @"Ширина";
            textBox5.Visible = true;
            textBox6.Clear();

            label6.Visible = true;
            label6.Text = @"Высота";
            textBox6.Visible = true;
            textBox6.Clear();

            label7.Visible = true;

            pictureBox1.Visible = true;
            pictureBox1.Size = new Size(240, 180);
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
            if (_fileName != null)
            {
                var targetFile = new Mp3File(_fileName);

                try
                {
                    var artist = textBox1.Text;
                    var album = textBox2.Text;
                    var song = textBox3.Text;
                    var num = textBox4.Text;
                    var genre = textBox5.Text;
                    var year = textBox6.Text;

                    targetFile.TagHandler.Artist = artist;
                    targetFile.TagHandler.Album = album;
                    targetFile.TagHandler.Song = song;
                    targetFile.TagHandler.Track = num;
                    targetFile.TagHandler.Genre = genre;
                    targetFile.TagHandler.Year = year;

                    targetFile.UpdatePacked();
                    MessageBox.Show(@"Сохранено");
                }
                catch
                {
                    MessageBox.Show(@"Ошибка сохранения");
                }
            }
            else
            {
                MessageBox.Show(@"Нет файла");
            }
        }

        private void открытььToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label7.Visible = true;
            OpenFileDialog dlg = new OpenFileDialog
            {
                FileName = label7.Text,
                Filter = @"JPEG Images (*.jpg)|*.jpg"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                label7.Text = dlg.FileName;
                GetExifData();
                buttonSave.Visible = false;
            }
        }

        private void GetExifData()
        {
            if (!File.Exists(label7.Text))
            {
                MessageBox.Show(this, @"Пожалуйста, введите корректное имя файла", @"Файл не найден",
                    MessageBoxButtons.OK);
                return;
            }

            try
            {
                SetExif();
                using (var reader = new ExifReader(label7.Text))
                {
                    // Get the image thumbnail (if present)
                    var thumbnailBytes = reader.GetJpegThumbnailBytes();

                    if (thumbnailBytes == null)
                        pictureBox1.Image = null;
                    else
                    {
                        using (var stream = new MemoryStream(thumbnailBytes))
                            pictureBox1.Image = Image.FromStream(stream);
                    }

                    reader.GetTagValue<string>(ExifTags.Make, out var make);
                    textBox1.Text = make;

                    reader.GetTagValue<string>(ExifTags.Model, out var model);
                    textBox2.Text = model;

                    reader.GetTagValue<string>(ExifTags.LensModel, out var lensModel);
                    textBox3.Text = lensModel;

                    reader.GetTagValue<DateTime>(ExifTags.DateTimeOriginal, out var date);
                    textBox4.Text = date.ToString(CultureInfo.InvariantCulture);

                    reader.GetTagValue<uint>(ExifTags.PixelXDimension, out var pixelX);
                    textBox5.Text = pixelX.ToString() + @" пикселей";

                    reader.GetTagValue<uint>(ExifTags.PixelYDimension, out var pixelY);
                    textBox6.Text = pixelY.ToString() + @" пикселей";
                }
            }
            catch (Exception ex)
            {
                // Something didn't work!
                MessageBox.Show(this, ex.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}