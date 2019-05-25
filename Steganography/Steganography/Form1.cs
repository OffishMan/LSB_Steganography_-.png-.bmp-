using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteganographyLib;

namespace Steganography
{
    public partial class MainMenu : Form
    {
        private string picName;
        private string textName;

        public MainMenu()
        {
            InitializeComponent();
            picName = "";
            textName = "";
        }

        private void button1_Click(object sender, EventArgs e)      //Выбор изображения для кодирования
        {
            OpenFileDialog openPic = new OpenFileDialog();
            openPic.Filter = "Файлы изображений (*.png)|*.png|Файлы изображений (*.bmp)|*.bmp";
            //|Все файлы (*.*)|*.*
            if (openPic.ShowDialog() == DialogResult.OK)
            {
                picName = openPic.FileName;
                checkBox1.Checked = true;
                checkBox4.Checked = false;
                checkBox3.Checked = false;
                checkBox5.Checked = false;
            }
            else
            {
                picName = "";
                checkBox1.Checked = false;
                checkBox4.Checked = false;
                checkBox3.Checked = false;
                checkBox5.Checked = false;
                return;
            }

        }

        private void button2_Click(object sender, EventArgs e)      //Выбор текстового файла для кодирования
        {
            OpenFileDialog openText = new OpenFileDialog();
            openText.Filter = "Текстовые файлы (*.txt)|*.txt";

            if (openText.ShowDialog() == DialogResult.OK)
            {
                textName = openText.FileName;
                checkBox2.Checked = true;
                checkBox3.Checked = false;
            }
            else
            {
                textName = "";
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)      //Выбор изображения для декодирования
        {
            OpenFileDialog openPic = new OpenFileDialog();
            openPic.Filter = "Файлы изображений (*.png)|*.png|Файлы изображений (*.bmp)|*.bmp";
            //|Все файлы (*.*)|*.*
            if (openPic.ShowDialog() == DialogResult.OK)
            {
                picName = openPic.FileName;
                checkBox4.Checked = true;
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                checkBox5.Checked = false;
            }
            else
            {
                picName = "";
                checkBox1.Checked = false;
                checkBox4.Checked = false;
                checkBox3.Checked = false;
                checkBox5.Checked = false;
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)          //Кодирование текста в изображение
        {
            if (checkBox1.Checked == true && checkBox2.Checked == true)
            {
                FileStream picStream;
                FileStream textStream;

                try
                {
                    picStream = new FileStream(picName, FileMode.Open);
                }
                catch(IOException)
                {
                    MessageBox.Show("Ошибка открытия файла, перезапустите приложение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    textStream = new FileStream(textName, FileMode.Open);
                }
                catch (IOException)
                {
                    MessageBox.Show("Ошибка открытия файла, перезапустите приложение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = SteganographyLSB.Encode(picStream, textStream);

                picStream.Close();
                textStream.Close();

                Bitmap img;

                if (result.Key == null)
                {
                    MessageBox.Show(result.Value, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    img = new Bitmap(result.Key);
                }

                string picSaveName;

                SaveFileDialog picSaveDialog = new SaveFileDialog();
                picSaveDialog.Filter = "Файлы изображений (*.png)|*.png|Файлы изображений (*.bmp)|*.bmp";

                if (picSaveDialog.ShowDialog() == DialogResult.OK)
                {
                    picSaveName = picSaveDialog.FileName;
                }
                else
                {
                    //picSaveName = "";
                    return;
                }

                FileStream saveFile;
                try
                {
                    saveFile = new FileStream(picSaveName, FileMode.Create);
                    checkBox3.Checked = true;
                }
                catch(IOException)
                {
                    MessageBox.Show("Ошибка открытия файла на запись, перезапустите приложение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (picSaveName[picSaveName.Length - 1] == 'g')     //В зависимости от имени сохранение должно происходит в нужном формате
                {
                    img.Save(saveFile, System.Drawing.Imaging.ImageFormat.Png);
                }
                else
                {
                    img.Save(saveFile, System.Drawing.Imaging.ImageFormat.Bmp);
                }
                saveFile.Close();
            }
            else
            {
                MessageBox.Show("Не выбран текст или картинка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)      //Декодирование текста из изображения
        {
            if (checkBox4.Checked == true)
            {
                FileStream picStream;
                try
                {
                    picStream = new FileStream(picName, FileMode.Open);
                }
                catch (IOException)
                {
                    MessageBox.Show("Ошибка открытия файла, перезапустите приложение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var message = SteganographyLSB.Decode(picStream);

                if (message.Key)
                {
                    string textSaveName;
                    SaveFileDialog textSaveDialog = new SaveFileDialog();
                    textSaveDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

                    if (textSaveDialog.ShowDialog() == DialogResult.OK)
                    {
                        textSaveName = textSaveDialog.FileName;
                    }
                    else
                    {
                        return;
                    }

                    FileStream saveFile;
                    try
                    {
                        saveFile = new FileStream(textSaveName, FileMode.Create);
                    }
                    catch(IOException)
                    {
                        MessageBox.Show("Ошибка открытия файла на запись, перезапустите приложение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    StreamWriter writeText = new StreamWriter(saveFile, Encoding.Default);
                    writeText.Write(message.Value);

                    writeText.Close();
                    saveFile.Close();

                    checkBox5.Checked = true;
                }
                else
                {
                    MessageBox.Show(message.Value, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                picStream.Close();
            }
            else
            {
                MessageBox.Show("Не выбрана картинка для декодирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
