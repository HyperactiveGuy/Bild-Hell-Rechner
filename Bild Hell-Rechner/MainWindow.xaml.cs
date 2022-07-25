using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;

namespace Bild_Hell_Rechner
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileInfo chosenFile;
        MemoryStream ms;
        Popup popup;
        Bitmap bmpResult;
        Task tskBrightenUp;
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
                popup = new Popup();
                CreateBrighterImage();
            }
            else if (pressedButton.Name == "btnSaveImage")
            {
                SaveImage();
            }
        }
      
        private void CreateBrighterImage()
        {
            int brightnessLevel = (int)sldBrightness.Value;

            Task tskBrightenUp = Task.Run(() =>
            {
                bmpResult = ImageBrightener.BrightenUp(chosenFile.FullName, brightnessLevel, popup, this);
                Application.Current.Dispatcher.Invoke(() => ProcessFinishedImage());
            });
        }
        private void ProcessFinishedImage()
        {
            ms = new MemoryStream();
            bmpResult.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            BitmapImage bmpi = new BitmapImage();
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
                }
                else
                {
                    MessageBox.Show("Illegal file extension! \nOnly .png, .jpg or .jpeg", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveImage()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Png Image|*.png|JPeg Image|*.jpg"; //Legt Fest, welche Datentyoen zum Speicher erlaubt sind
            saveFileDialog.ShowDialog(); //Oeffnet Explorer
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

    }
}
