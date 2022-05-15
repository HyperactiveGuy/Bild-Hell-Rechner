using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        //TestComment

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button pressedButton = (Button)sender;

            if (pressedButton.Name == "btnLoadFile")
            {
                //Oeffnet Explorer
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    FileInfo chosenFile = new FileInfo(openFileDialog.FileName);

                    Console.WriteLine(IsFileLegalImage(chosenFile));
                    if (IsFileLegalImage(chosenFile))
                    {
                        imgSource.Source = new BitmapImage(new Uri(chosenFile.FullName)); //Fuegt Image in GUI ein

                        TestDataTypeStuffLolHaha(new BitmapImage(new Uri(chosenFile.FullName)), chosenFile.FullName); //My test Method
                    }
                }
            }
        }

        /// <summary>
        /// Checks, if a File is an Image with a legal extension
        /// </summary>
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
        private void TestDataTypeStuffLolHaha(BitmapImage img, string imgPath)
        {

            Bitmap bmp = new Bitmap(imgPath);

            MemoryStream ms = new MemoryStream();

            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            imgResult.Source = image;


        }
    }
}
