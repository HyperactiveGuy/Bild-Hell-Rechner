using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;
using System.Drawing;

namespace Bild_Hell_Rechner
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileInfo chosenFile;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button pressedButton = (Button)sender;
            if (pressedButton.Name == "btnLoadImage")
            {
                ChoseSourceImage();
            }
            else if (pressedButton.Name == "btnBrightenUp")
            {
                ImageBrightener.BrightenUp(chosenFile.FullName, (int)sldBrightness.Value, chosenFile);
            }
        }



        /// <summary>
        /// Oens File Explorer and sets the Source Image
        /// after User-Input
        /// </summary>
        private void ChoseSourceImage()
        {
            //Oeffnet Explorer
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                chosenFile = new FileInfo(openFileDialog.FileName);

                if (IsFileLegalImage(chosenFile))
                {
                    img.Source = new BitmapImage(new Uri(chosenFile.FullName)); //Fuegt Image in GUI ein

                    // TestDataTypeStuffLolHaha(new BitmapImage(new Uri(chosenFile.FullName)), chosenFile.FullName); //My test Method
                }
            }
        }


        /// <summary>
        /// Checks, if a File is an Image with a legal extension
        /// </summary>
        ///<param name="file"></param>
        private bool IsFileLegalImage(FileInfo file)
        {
            string[] extensions = new string[] { ".png", ".jpg", ".jpeg" };

            for (int i = 0; i < extensions.Length; i++)
            {
                if (file.Extension == extensions[i])
                {
                    return true;
                }
            }
            return false;
        }


        //TODO Understand Code from Stack Overflow
        /// <summary>
        /// Do not call this in finished Project!
        /// Just for testing stuff lulZ
        /// </summary>
        /// <param name="imgPath"></param>
        private void TestDataTypeStuffLolHaha(string imgPath)
        {
            return;
            Bitmap bmp = new Bitmap(imgPath);

            MemoryStream ms = new MemoryStream();

            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            BitmapImage image = new BitmapImage();



            //  image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;

            //image.EndInit();



            Byte[] streamFile = ms.ToArray();



            MemoryStream streamy = new MemoryStream(streamFile);
            image.StreamSource = streamy;

            ms.Dispose(); //Macht Probleme TODO: Fix
            img.Source = image;
        }



    }
}
