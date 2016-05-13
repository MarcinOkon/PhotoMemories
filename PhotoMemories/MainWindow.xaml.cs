using GooglePhotosUploader.Code;
using System.Windows;

namespace GooglePhotosUploader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //TODO:
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            PicasaPhotoManager.DownloadMedia();
        }
    }
}
