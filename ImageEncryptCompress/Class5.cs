using System;
using System.Text;
using Nito.Collections;

namespace ImageEncryptCompress
{
    internal class Class5
    {
        public static RGBPixel[,] EncryptImage(RGBPixel[,] pixels, string initiall, int tap)
        {
            int height = ImageOperations.GetHeight(pixels);
            int width = ImageOperations.GetWidth(pixels);
            StringBuilder initial = new StringBuilder(initiall);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    string[] Keys = GenerateKeys(ref initial, tap);
                    byte RKey = Convert.ToByte(Keys[0], 2);
                    byte GKey = Convert.ToByte(Keys[1], 2);
                    byte BKey = Convert.ToByte(Keys[2], 2);


                    pixels[i, j].red = (byte)(pixels[i, j].red ^ RKey);
                    pixels[i, j].green = (byte)(pixels[i, j].green ^ GKey);
                    pixels[i, j].blue = (byte)(pixels[i, j].blue ^ BKey);
                }
            }
            return pixels;
        }

        public static string[] GenerateKeys(ref StringBuilder initial, int tap)
        {

            StringBuilder rbuilder = new StringBuilder();
            StringBuilder gbuilder = new StringBuilder();
            StringBuilder bbuilder = new StringBuilder();

            char replaced;
            tap = initial.Length - tap - 1;
            for (int i = 0; i < 24; i++)
            {
                if (i < 8)
                {
                    if (initial[0] == initial[tap]) replaced = '0';
                    else replaced = '1';
                    initial.Remove(0,1);
                    initial.Append(replaced);
                    rbuilder.Append(replaced);
                }
                else if (i < 16)
                {

                    if (initial[0] == initial[tap]) replaced = '0';
                    else replaced = '1';
                    initial.Remove(0, 1);
                    initial.Append(replaced);
                    gbuilder.Append(replaced);
                }
                else
                {
                    if (initial[0] == initial[tap]) replaced = '0';
                    else replaced = '1';
                    initial.Remove(0, 1);
                    initial.Append(replaced);
                    bbuilder.Append(replaced);
                }

            }
            string BlueKey = bbuilder.ToString();
            string GreenKey = gbuilder.ToString();
            string RedKey = rbuilder.ToString();

            string[] Keys = { RedKey, GreenKey, BlueKey };
            return Keys;
        }
    }
}
