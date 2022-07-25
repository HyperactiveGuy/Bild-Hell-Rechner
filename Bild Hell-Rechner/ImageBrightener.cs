using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Bild_Hell_Rechner
{
    internal static class ImageBrightener
    {

        public static Bitmap BrightenUp(string imagePath, int brightnessMultiplicator, Popup popup, MainWindow w)
        {
            Bitmap image = new Bitmap(imagePath);

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
                if (y % 100 == 0)
                {
                    RefreshProgressBar(popup,y, image.Height);
                }
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                popup.Close();
            });
            return image;
        }

        private static void RefreshProgressBar(Popup popup, int y, int height)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                popup.progressbar.Value = (double)y * 100.0d / (double)height;
            });
        }
    }
}
