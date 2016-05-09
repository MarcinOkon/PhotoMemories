using GooglePhotosUploader.Code;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using System;
using GooglePhotosUploader.Code.DataModel;

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
