using System.IO;

namespace ImageEncryptCompress
{
    internal class Comp
    {
        public static void WriteColorComponentsToFile(RGBPixel[,] decompressedImage, string redFile, string greenFile, string blueFile,string s)
        {
            int height = ImageOperations.GetHeight(decompressedImage);
            int width = ImageOperations.GetWidth(decompressedImage);

            using (StreamWriter Writer = new StreamWriter(s))
            {
                
                Writer.WriteLine(height);
                Writer.WriteLine(width);
            }
            // Write red component to file
            using (StreamWriter redWriter = new StreamWriter(redFile))
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        redWriter.WriteLine(decompressedImage[i, j].red);
                    }
                }
            }

            // Write green component to file
            using (StreamWriter greenWriter = new StreamWriter(greenFile))
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        greenWriter.WriteLine(decompressedImage[i, j].green);
                    }
                }
            }

            // Write blue component to file
            using (StreamWriter blueWriter = new StreamWriter(blueFile))
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        blueWriter.WriteLine(decompressedImage[i, j].blue);
                    }
                }
            }
        }
    }
}
