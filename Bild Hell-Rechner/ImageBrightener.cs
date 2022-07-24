using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bild_Hell_Rechner
{
    internal static class ImageBrightener
    {

        public static Bitmap BrightenUp(string imagePath, int brightnessMultiplicator)
        {

            Console.WriteLine("Starting with brightening-process...");

            Bitmap image = new Bitmap(imagePath);

            Console.WriteLine("Task started...");

            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color originalColor = image.GetPixel(x, y);

                    int r = originalColor.R * brightnessMultiplicator;
                    int g = originalColor.G * brightnessMultiplicator;
                    int b = originalColor.B * brightnessMultiplicator;

                    r = r < 255 ? r : 255;
                    b = b < 255 ? b : 255;
                    g = g < 255 ? g : 255;

                    Color newColor = Color.FromArgb(r, g, b);

                    image.SetPixel(x, y, newColor); //ueberschreibt Pixel
                }
            }
            sw.Stop();
            Console.WriteLine("Finished Brightening Process!");
            Console.WriteLine($"Runtime in Milliseconds: {sw.ElapsedMilliseconds}");
            return image;
        }
    }
}
