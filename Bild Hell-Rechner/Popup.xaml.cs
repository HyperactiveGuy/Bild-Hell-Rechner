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
using System.Windows.Shapes;

namespace Bild_Hell_Rechner
{
    /// <summary>
    /// Interaktionslogik für Popup.xaml
    /// </summary>
    public partial class Popup : Window
    {
        public Popup()
        {
            InitializeComponent();
            
            Show();
          //  Task t = Task.Run(() => StretchProgressBar());
        }

        public static void StretchProgressBar(Popup popup, int currentY, int imageHeight)
        {
            Console.WriteLine("Method");
            Task t = Task.Run(() =>
            {
                Console.WriteLine("Task");

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Console.WriteLine("Dispatcher");
                    popup.progressbar.Value = (double)currentY * 100.0d / (double)imageHeight;
                });
            });


        }
    }
}
