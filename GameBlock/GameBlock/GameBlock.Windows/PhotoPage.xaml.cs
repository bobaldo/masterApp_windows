using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace GameBlock
{
    public sealed partial class PhotoPage : Page
    {
        private Windows.Storage.StorageFile file;
        private ImageEncodingProperties imgFormat;
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

                
                //await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);

                 imgFormat = ImageEncodingProperties.CreatePng();
                imgFormat.Height = Constant.DimensionHeightImageSaved;
                imgFormat.Width = Constant.DimensionWidthImageSaved;
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
            try
            {
                await file.DeleteAsync(Windows.Storage.StorageDeleteOption.Default);
            }
            catch { }
            file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(Constant.NameImageSaved,
            Windows.Storage.CreationCollisionOption.ReplaceExisting);

            await captureManager.CapturePhotoToStorageFileAsync(imgFormat, file);
            BitmapImage bmpImage = new BitmapImage(new Uri(file.Path));
            imagePreivew.Source = null;
            imagePreivew.Source = bmpImage;
        }
    }
}