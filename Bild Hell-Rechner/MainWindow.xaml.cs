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
        MemoryStream ms;
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
                CreateBrighterImage();

            }
            else if (pressedButton.Name == "btnSaveImage")
            {
                SaveImage();
            }
        }

        private void CreateBrighterImage()
        {
            Bitmap bmpResult = ImageBrightener.BrightenUp(chosenFile.FullName, (int)sldBrightness.Value, chosenFile);
            BitmapImage bmpi = new BitmapImage();

            ms = new MemoryStream();
            bmpResult.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            ms.Seek(0, SeekOrigin.Begin);

            bmpi.BeginInit();
            bmpi.StreamSource = ms;
            bmpi.EndInit();

            img.Source = bmpi;
        }


        /// <summary>
        /// Opens File Explorer and sets the Source Image
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

                    //TestDataTypeStuffLolHaha(chosenFile.FullName); //My test Method
                }
            }
        }

        private void SaveImage()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPeg Image|*.jpg|Png Image|*.png";
            saveFileDialog.ShowDialog();
            Console.Write($"Target Path: {saveFileDialog.FileName}");
            Bitmap bitmap = new Bitmap(ms);
            bitmap.Save(saveFileDialog.FileName);


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
            //  return;
            Bitmap bmp = new Bitmap(imgPath);

            MemoryStream ms = new MemoryStream();

            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            BitmapImage image = new BitmapImage();


            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            MemoryStream streamy = new MemoryStream(ms.ToArray());
            image.StreamSource = streamy;

            image.EndInit();

            image.StreamSource = streamy;

            // ms.Dispose(); //Macht Probleme TODO: Fix
            img.Source = image;
        }



    }
}
