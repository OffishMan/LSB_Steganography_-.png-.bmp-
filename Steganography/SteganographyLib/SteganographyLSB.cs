using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteganographyLib
{
    public static class SteganographyLSB
    {
        public static int MessageLength = 9;/*{ get; set; }*/

        public static BitArray ByteToBit(byte src)
        {
            BitArray bitArray = new BitArray(8);
            bool temp;
            for (int i = 0; i < 8; i++)
            {
                if ((src >> i & 1) == 1)        //Выталкиваем младшие биты
                {
                    temp = true;
                }
                else
                {
                    temp = false;
                }
                bitArray[i] = temp;
            }

            return bitArray;
        }

        public static byte BitToByte(BitArray src)
        {
            byte num = 0;
            for (int i = 0; i < src.Count; i++)
                if (src[i] == true)
                    num += (byte)Math.Pow(2, i);    //Сразу переводим в десятичную систему счисления
            return num;
        }

        public static bool IsContainsMessage(Bitmap src)
        {
            byte[] rez = new byte[1];

            Color color = src.GetPixel(0, 0);       //Читаем самый первый пиксель

            BitArray symbol = new BitArray(8);    //Здесь будет информация из первого символа

            BitArray colorArray = ByteToBit(color.R);       //Читаем байт красного цвета и заменяем 2 младших разряда            
            symbol[0] = colorArray[0];
            symbol[1] = colorArray[1];

            colorArray = ByteToBit(color.G);            //Читаем байт зелёного цвета и заменяем 3 младших разряда
            symbol[2] = colorArray[0];
            symbol[3] = colorArray[1];
            symbol[4] = colorArray[2];

            colorArray = ByteToBit(color.B);            //Читаем байт синего цвета и заменяем 3 младших разряда
            symbol[5] = colorArray[0];
            symbol[6] = colorArray[1];
            symbol[7] = colorArray[2];

            rez[0] = BitToByte(symbol);           //Собираем биты в байт

            string m = Encoding.GetEncoding(1251).GetString(rez);   //Декодируем

            if (m == "~")
            {
                return true;
            }
            else
                return false;
        }
        //
        //Отсюда и далее конструкции схожие с методом IsContainsMessage(Bitmap src) не будут комментироваться
        //
        public static void WriteLengthMessage(int count, Bitmap src)
        {
            byte[] CountSymbols = Encoding.GetEncoding(1251).GetBytes(count.ToString());

            for (int i = 0; i < ((CountSymbols.Length < MessageLength) ? CountSymbols.Length : MessageLength); i++)
            {
                BitArray bitCount = ByteToBit(CountSymbols[i]);     //Массив битов, представляющих собой цифру из числа символов
                                                                    //Разрядность числа, доступного для записи определяется автосвойством MessageLength
                Color color = src.GetPixel(i + 1, 0);

                BitArray bitsCurColor = ByteToBit(color.R);
                bitsCurColor[0] = bitCount[0];
                bitsCurColor[1] = bitCount[1];
                byte newR = BitToByte(bitsCurColor);

                bitsCurColor = ByteToBit(color.G);
                bitsCurColor[0] = bitCount[2];
                bitsCurColor[1] = bitCount[3];
                bitsCurColor[2] = bitCount[4];
                byte newG = BitToByte(bitsCurColor);

                bitsCurColor = ByteToBit(color.B);
                bitsCurColor[0] = bitCount[5];
                bitsCurColor[1] = bitCount[6];
                bitsCurColor[2] = bitCount[7];
                byte newB = BitToByte(bitsCurColor);

                Color newColor = Color.FromArgb(newR, newG, newB);
                src.SetPixel(i + 1, 0, newColor);
            }
        }

        public static int ReadCountText(Bitmap src)
        {
            byte[] rez = new byte[MessageLength];

            for (int i = 0; i < MessageLength; i++)
            {
                Color color = src.GetPixel(i + 1, 0);

                BitArray bitCount = ByteToBit(8);

                BitArray colorArray = ByteToBit(color.R);
                bitCount[0] = colorArray[0];
                bitCount[1] = colorArray[1];

                colorArray = ByteToBit(color.G);
                bitCount[2] = colorArray[0];
                bitCount[3] = colorArray[1];
                bitCount[4] = colorArray[2];

                colorArray = ByteToBit(color.B);
                bitCount[5] = colorArray[0];
                bitCount[6] = colorArray[1];
                bitCount[7] = colorArray[2];

                rez[i] = BitToByte(bitCount);
            }

            string count = Encoding.GetEncoding(1251).GetString(rez);

            count = ReturnCorrectString(count);
            return Convert.ToInt32(count, 10);
        }

        public static string ReturnCorrectString(string num)
        {
            int count = 0;
            foreach (var item in num)
            {
                if (char.IsDigit(item))
                {
                    ++count;
                }
                else
                    break;
            }
            return num.Substring(0, count);
        }

        public static void EncodeFirstSymbol(Bitmap img)
        {
            byte[] symbol = Encoding.GetEncoding(1251).GetBytes("~");
            BitArray firstSymbol = ByteToBit(symbol[0]);

            Color currentColor = img.GetPixel(0, 0);

            BitArray tempArray = ByteToBit(currentColor.R);
            tempArray[0] = firstSymbol[0];
            tempArray[1] = firstSymbol[1];
            byte newR = BitToByte(tempArray);

            tempArray = ByteToBit(currentColor.G);
            tempArray[0] = firstSymbol[2];
            tempArray[1] = firstSymbol[3];
            tempArray[2] = firstSymbol[4];
            byte newG = BitToByte(tempArray);

            tempArray = ByteToBit(currentColor.B);
            tempArray[0] = firstSymbol[5];
            tempArray[1] = firstSymbol[6];
            tempArray[2] = firstSymbol[7];
            byte newB = BitToByte(tempArray);

            Color newColor = Color.FromArgb(newR, newG, newB);
            img.SetPixel(0, 0, newColor);
        }

        public static KeyValuePair<Bitmap, string> Encode(FileStream picStream, FileStream textStream)
        {
            //KeyValuePair<Bitmap, string> message = new KeyValuePair<Bitmap, string>(null, "Произошла непредвиденная ошибка");

            Bitmap img = new Bitmap(picStream);

            //Проверка на наличие сообщения в картинке
            if (IsContainsMessage(img))
            {
                return new KeyValuePair<Bitmap, string>(null, "Файл уже зашифрован");
            }

            BinaryReader text = new BinaryReader(textStream, Encoding.ASCII);

            List<byte> list = new List<byte>();

            while (text.PeekChar() != -1)       //Считываем весь текст побайтово в список
            {
                list.Add(text.ReadByte());
            }
            int textLength = list.Count;         //Количество байт текста, совпадает с длиной текста


            //Проверка на то, поместится ли текст. Для удобства организации циклов, первые MessageLength + 1 столбцы пустуют, кроме первой строки
            if (textLength > ((img.Width * img.Height)) - (MessageLength + 1) * img.Height)  //Запись идёт сверху вниз
            {
                return new KeyValuePair<Bitmap, string>(null, "Выбранная картинка мала для размещения выбранного текста");
            }

            //Шифрование первого символа, индикатора наличия сообщения
            EncodeFirstSymbol(img);

            WriteLengthMessage(textLength, img);

            //int index = 0;
            //bool st = false;
            //for (int j = 0; j < img.Height; j++)
            //{
            //    for (int i = 1; i < img.Width; i++)
            //    {
            //        Color pixelColor = img.GetPixel(i, j);
            //        if (index == bList.Count)
            //        {
            //            st = true;
            //            break;
            //        }
            //        BitArray colorArray = ByteToBit(pixelColor.R);
            //        BitArray messageArray = ByteToBit(bList[index]);
            //        colorArray[0] = messageArray[0];    //меняем
            //        colorArray[1] = messageArray[1];    // в нашем цвете биты
            //        byte newR = BitToByte(colorArray);

            //        colorArray = ByteToBit(pixelColor.G);
            //        colorArray[0] = messageArray[2];
            //        colorArray[1] = messageArray[3];
            //        colorArray[2] = messageArray[4];
            //        byte newG = BitToByte(colorArray);

            //        colorArray = ByteToBit(pixelColor.B);
            //        colorArray[0] = messageArray[5];
            //        colorArray[1] = messageArray[6];
            //        colorArray[2] = messageArray[7];
            //        byte newB = BitToByte(colorArray);

            //        Color newColor = Color.FromArgb(newR, newG, newB);
            //        img.SetPixel(i, j, newColor);
            //        index++;
            //    }
            //    if (st)
            //    {
            //        break;
            //    }
            //}

            //for (int i = MessageLength + 1; i < textLength + MessageLength + 1; i++)
            //{
            //    Color pixelColor = img.GetPixel(i % img.Width, i / img.Width);

            //    BitArray messageArray = ByteToBit(list[i - MessageLength - 1]);

            //    BitArray colorArray = ByteToBit(pixelColor.R);
            //    colorArray[0] = messageArray[0];
            //    colorArray[1] = messageArray[1];
            //    byte newR = BitToByte(colorArray);

            //    colorArray = ByteToBit(pixelColor.G);
            //    colorArray[0] = messageArray[2];
            //    colorArray[1] = messageArray[3];
            //    colorArray[2] = messageArray[4];
            //    byte newG = BitToByte(colorArray);

            //    colorArray = ByteToBit(pixelColor.B);
            //    colorArray[0] = messageArray[5];
            //    colorArray[1] = messageArray[6];
            //    colorArray[2] = messageArray[7];
            //    byte newB = BitToByte(colorArray);

            //    Color newColor = Color.FromArgb(newR, newG, newB);
            //    img.SetPixel(i % img.Width, i / img.Width, newColor);
            //}

            Parallel.For(MessageLength + 1, textLength + MessageLength + 1,
            (i) =>
            {
                Color pixelColor;
                lock (img) pixelColor = img.GetPixel(i % img.Width, i / img.Width);

                BitArray messageArray = ByteToBit(list[i - MessageLength - 1]);

                BitArray colorArray = ByteToBit(pixelColor.R);
                colorArray[0] = messageArray[0];
                colorArray[1] = messageArray[1];
                byte newR = BitToByte(colorArray);

                colorArray = ByteToBit(pixelColor.G);
                colorArray[0] = messageArray[2];
                colorArray[1] = messageArray[3];
                colorArray[2] = messageArray[4];
                byte newG = BitToByte(colorArray);

                colorArray = ByteToBit(pixelColor.B);
                colorArray[0] = messageArray[5];
                colorArray[1] = messageArray[6];
                colorArray[2] = messageArray[7];
                byte newB = BitToByte(colorArray);

                Color newColor = Color.FromArgb(newR, newG, newB);
                lock (img) img.SetPixel(i % img.Width, i / img.Width, newColor);
            });

            return new KeyValuePair<Bitmap, string>(img, "");
        }

        public static KeyValuePair<bool, string> Decode(FileStream picFile)
        {
            Bitmap img = new Bitmap(picFile);

            if (!IsContainsMessage(img))
            {
                return new KeyValuePair<bool, string>(false, "В файле не обнаружено признаков шифрования");
            }

            int symbolsCount = ReadCountText(img);          //считали количество зашифрованных символов

            byte[] message = new byte[symbolsCount];

            //int index = 0;
            //bool st = false;
            //for (int j = 0; j < img.Height; j++)
            //{
            //    for (int i = 1; i < img.Width; i++)
            //    {
            //        Color pixelColor = img.GetPixel(i, j);
            //        if (index == message.Length)
            //        {
            //            st = true;
            //            break;
            //        }
            //        BitArray colorArray = ByteToBit(pixelColor.R);
            //        BitArray messageArray = ByteToBit(pixelColor.R);
            //        messageArray[0] = colorArray[0];
            //        messageArray[1] = colorArray[1];

            //        colorArray = ByteToBit(pixelColor.G);
            //        messageArray[2] = colorArray[0];
            //        messageArray[3] = colorArray[1];
            //        messageArray[4] = colorArray[2];

            //        colorArray = ByteToBit(pixelColor.B);
            //        messageArray[5] = colorArray[0];
            //        messageArray[6] = colorArray[1];
            //        messageArray[7] = colorArray[2];
            //        message[index] = BitToByte(messageArray);
            //        index++;
            //    }
            //    if (st)
            //    {
            //        break;
            //    }
            //}

            //for (int i = MessageLength + 1; i < symbolsCount + MessageLength + 1; i++)
            //{
            //    Color pixelColor = img.GetPixel(i % img.Width, i / img.Width);

            //    BitArray messageArray = ByteToBit(8);

            //    BitArray colorArray = ByteToBit(pixelColor.R);
            //    messageArray[0] = colorArray[0];
            //    messageArray[1] = colorArray[1];

            //    colorArray = ByteToBit(pixelColor.G);
            //    messageArray[2] = colorArray[0];
            //    messageArray[3] = colorArray[1];
            //    messageArray[4] = colorArray[2];

            //    colorArray = ByteToBit(pixelColor.B);
            //    messageArray[5] = colorArray[0];
            //    messageArray[6] = colorArray[1];
            //    messageArray[7] = colorArray[2];

            //    message[i - MessageLength - 1] = BitToByte(messageArray);
            //}

            Parallel.For(MessageLength + 1, symbolsCount + MessageLength + 1,
                (i) =>
                {
                    Color pixelColor;
                    lock (img) pixelColor = img.GetPixel(i % img.Width, i / img.Width);

                    BitArray messageArray = ByteToBit(8);

                    BitArray colorArray = ByteToBit(pixelColor.R);
                    messageArray[0] = colorArray[0];
                    messageArray[1] = colorArray[1];

                    colorArray = ByteToBit(pixelColor.G);
                    messageArray[2] = colorArray[0];
                    messageArray[3] = colorArray[1];
                    messageArray[4] = colorArray[2];

                    colorArray = ByteToBit(pixelColor.B);
                    messageArray[5] = colorArray[0];
                    messageArray[6] = colorArray[1];
                    messageArray[7] = colorArray[2];

                    message[i - MessageLength - 1] = BitToByte(messageArray);
                });

            string strMessage = Encoding.GetEncoding(1251).GetString(message);

            return new KeyValuePair<bool, string>(true, strMessage);
        }
    }
}
