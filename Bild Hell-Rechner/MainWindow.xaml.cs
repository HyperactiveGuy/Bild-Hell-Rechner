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
                CreateBrighterImage();
            }
            else if (pressedButton.Name == "btnSaveImage")
            {
                SaveImage();
            }
        }
      
        private void CreateBrighterImage()
        {
            if (ms != null)
            {
                btnBrightenUp.IsEnabled = false;
                popup = new Popup();
                int brightnessLevel = (int)sldBrightness.Value;

                Task tskBrightenUp = Task.Run(() =>
                {
                    bmpResult = ImageBrightener.BrightenUp(chosenFile.FullName, brightnessLevel, popup, this);
                    Application.Current.Dispatcher.Invoke(() => ProcessFinishedImage());
                });
            }
            else
            {
                MessageBox.Show("No Image loaded!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ProcessFinishedImage()
        {            
            bmpResult.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            BitmapImage bmpi = new BitmapImage();
            bmpi.BeginInit();
            bmpi.StreamSource = ms;
            bmpi.EndInit();
            img.Source = bmpi;
            btnBrightenUp.IsEnabled = true;
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
                    ms = new MemoryStream();
                }
                else
                {
                    MessageBox.Show("Illegal file extension! \nOnly .png, .jpg or .jpeg", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveImage()
        {
            if (ms != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Png Image|*.png|JPeg Image|*.jpg"; //Legt Fest, welche Datentyoen zum Speicher erlaubt sind
                if (saveFileDialog.ShowDialog() == true)
                {
                    Bitmap bitmap = new Bitmap(ms);
                    bitmap.Save(saveFileDialog.FileName);
                }
            }
            else
            {
                MessageBox.Show("No Image loaded!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

    }
}
