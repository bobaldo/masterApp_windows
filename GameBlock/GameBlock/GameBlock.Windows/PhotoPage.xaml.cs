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
        private bool makeFoto = false;

        public PhotoPage()
        {
            this.InitializeComponent();
            loadCameraAndFile();
            ManageImage.resetCountCapture();
            imgFormat.Height = Constant.DimensionHeightImageSaved;
            imgFormat.Width = Constant.DimensionWidthImageSaved;
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
                if (!makeFoto)
                    ManageImage.roolbackCountCapture();


                this.Frame.Navigate(typeof(MainPage));
            }
            catch { }
            finally
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        async private void CapturePhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!makeFoto)
                {
                    foreach (var item in await ApplicationData.Current.LocalFolder.GetFilesAsync())
                    {
                        if (item.Name.Contains(".png"))
                            await item.DeleteAsync();
                    }
                }
                makeFoto = true;

                ManageImage.addCountCapture();
                file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                    String.Format(Constant.NameImageWork, ManageImage.CountCapture),
                    CreationCollisionOption.ReplaceExisting);
                await captureManager.CapturePhotoToStorageFileAsync(imgFormat, file);

                imagePreivew.Source = new BitmapImage(new Uri(file.Path));
            }
            catch { }
        }
    }
}