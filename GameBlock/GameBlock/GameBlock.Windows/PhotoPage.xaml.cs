using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
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
        private StorageFile file;
        private ImageEncodingProperties imgFormat = ImageEncodingProperties.CreatePng();
        private MediaCapture captureManager;
        public PhotoPage()
        {
            this.InitializeComponent();
            imgFormat.Height = Constant.DimensionHeightImageSaved;
            imgFormat.Width = Constant.DimensionWidthImageSaved;
            loadCamera();
        }

        async private void loadCamera()
        {
            try
            {
                captureManager = new MediaCapture();
                await captureManager.InitializeAsync();
                capturePreview.Source = captureManager;
                captureManager.StartPreviewAsync().Completed += new AsyncActionCompletedHandler(completeCameraLoad);
            }
            catch { }
        }

        private void completeCameraLoad(IAsyncAction asyncInfo, AsyncStatus asyncStatus)
        {
            var upUI = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                CapturePhoto.IsEnabled = true;
            });
        }

        async private void StopCapturePreview_Click(object sender, RoutedEventArgs e)
        {
            await captureManager.StopPreviewAsync();
            this.Frame.Navigate(typeof(MainPage));
        }

        async private void CapturePhoto_Click(object sender, RoutedEventArgs e)
        {
            //TODO: non funziona il secondo salvataggio - problema di cache ????
            file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(Constant.NameImageSaved,
                CreationCollisionOption.ReplaceExisting);

            await captureManager.CapturePhotoToStorageFileAsync(imgFormat, file);
            BitmapImage bmpImage = new BitmapImage(new Uri(file.Path));
            imagePreivew.Source = null;
            imagePreivew.Source = bmpImage;
        }
    }
}