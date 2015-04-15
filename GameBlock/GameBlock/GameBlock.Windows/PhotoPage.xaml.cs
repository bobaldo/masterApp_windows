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
        private int countCapture = -1;

        public PhotoPage()
        {
            this.InitializeComponent();
            imgFormat.Height = Constant.DimensionHeightImageSaved;
            imgFormat.Width = Constant.DimensionWidthImageSaved;
            loadCameraAndFile();
        }

        async private void loadCameraAndFile()
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
            try
            {
                await captureManager.StopPreviewAsync();
                int i = 0;
                for (i = 0; i < countCapture; i++)
                {
                    file = await ApplicationData.Current.LocalFolder.GetFileAsync(String.Format(Constant.NameImageWork, i));
                    await file.DeleteAsync();
                }
                file = await ApplicationData.Current.LocalFolder.GetFileAsync(String.Format(Constant.NameImageWork, i));
                await file.RenameAsync(Constant.NameImageFinal);

                this.Frame.Navigate(typeof(MainPage));
            }
            catch { }
        }

        async private void CapturePhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                countCapture = countCapture + 1;
                file = await ApplicationData.Current.LocalFolder.CreateFileAsync(String.Format(Constant.NameImageWork, countCapture),
                    CreationCollisionOption.ReplaceExisting);
                await captureManager.CapturePhotoToStorageFileAsync(imgFormat, file);

                BitmapImage bmpImage = new BitmapImage(new Uri(file.Path));
                imagePreivew.Source = bmpImage;
            }
            catch { }
        }
    }
}