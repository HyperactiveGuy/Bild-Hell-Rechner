using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bild_Hell_Rechner
{



    internal static class ImageBrightener
    {

        public static void BrightenUp(string imagePath, int brightnessMultiplicator, FileInfo file)
        {
            Console.WriteLine("Starting with brightening-process...");

            Bitmap image = new Bitmap(imagePath);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color originalColor = image.GetPixel(x, y);

                    int r = originalColor.R;
                    int g = originalColor.G;
                    int b = originalColor.B;

                    //   int average = (r + g + b) / 3; //Durchschnitswert der RGB-Farben fuer Graustufe
                    // if (average < compareBrightness)
                    {
                        r *= brightnessMultiplicator;
                        g *= brightnessMultiplicator;
                        b *= brightnessMultiplicator;
                    }
                    r = r < 255 ? r : 255;
                    b = b < 255 ? b : 255;
                    g = g < 255 ? g : 255;

                    Color newColor = Color.FromArgb(r, g, b);

                    image.SetPixel(x, y, newColor); //ueberschreibt Pixel
                }
                Console.WriteLine($"Finished Row {y} of {image.Height}");
            }
            Console.WriteLine("Finished");
            image.Save(@"D:\Compare\" + file.Name);
            Console.WriteLine("Image saved successfully (I hope...)");
        }

    }
}
