using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GameBlock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PhotoPage : Page
    {
        private Windows.Media.Capture.MediaCapture captureManager;

        public PhotoPage()
        {
            this.InitializeComponent();
            //TODO: fare in modo che quando venga caricata la pagina si avvii la camera
        }

        async private void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                captureManager = new MediaCapture();
                await captureManager.InitializeAsync();
                capturePreview.Source = captureManager;
                await captureManager.StartPreviewAsync();
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        async private void StopCapturePreview_Click(object sender, RoutedEventArgs e)
        {
            await captureManager.StopPreviewAsync();
            this.Frame.Navigate(typeof(MainPage));
        }

        async private void CapturePhoto_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFile file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(
                Constant.NameImageSaved,
                Windows.Storage.CreationCollisionOption.ReplaceExisting);

            ImageEncodingProperties imgFormat = ImageEncodingProperties.CreatePng();
            imgFormat.Height = Constant.DimensionHeightImageSaved;
            imgFormat.Width = Constant.DimensionWidthImageSaved;
            await captureManager.CapturePhotoToStorageFileAsync(imgFormat, file);

            BitmapImage bmpImage = new BitmapImage(new Uri(file.Path));
            imagePreivew.Source = bmpImage;
        }
    }
}