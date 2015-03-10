using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace GameBlock
{
    public sealed partial class MainPage : Page
    {
        private int numbers = 10;

        public MainPage()
        {
            this.InitializeComponent();
        }

        public static DependencyProperty CurrentValueProperty =
            DependencyProperty.Register("Numbers", typeof(string), typeof(MainPage), new PropertyMetadata(""));

        public string Numbers
        {
            get { return GetValue(CurrentValueProperty) as string; }
            set { SetValue(CurrentValueProperty, value); }
        }

        private void setControlView(string content)
        {
            if (!String.IsNullOrEmpty(content))
            {
                if (numbers <= 0)
                    Numbers = Numbers.Remove(0, 1);
                Numbers += content;
                numbers -= 1;

                if (FindName("s" + content) is Storyboard)
                    (FindName("s" + content) as Storyboard).Begin();

                setImageVisibility(Int32.Parse(content));
                playMp3(content);
            }
        }

        private void playMp3(string content)
        {
            //BackgroundAudioPlayer

            //WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

            //wplayer.URL = "My MP3 file.mp3";
            //wplayer.Controls.Play();
        }

        private void setImageVisibility(int numberImgVisible)
        {
            for (int i = 0; i < 10; i++)
            {
                if (FindName("i" + i) is Image)
                {
                    if (i <= numberImgVisible)
                    {
                        (FindName("i" + i) as Image).Visibility = Visibility.Visible;

                        Storyboard sb = (FindName("animateImage" + i) as Storyboard);
                        sb.Stop();
                        sb.Begin();
                    }
                    else
                    {
                        (FindName("i" + i) as Image).Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        #region Event Handler
        private void erase_Click(object sender, RoutedEventArgs e)
        {
            Numbers = "";
            numbers = 10;
            setImageVisibility(0);
        }

        private void btnNumber_Click(object sender, RoutedEventArgs e)
        {
            setControlView(((sender as Button).Content as TextBlock).Text);
        }

        private void gridContainer_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            //doppio evento da capire il perchè
            var btnValuPressed = "";
            switch (e.Key)
            {
                default:
                case VirtualKey.NumberPad0:
                case VirtualKey.Number0:
                    btnValuPressed = "0";
                    break;
                case VirtualKey.NumberPad1:
                case VirtualKey.Number1:
                    btnValuPressed = "1";
                    break;
                case VirtualKey.NumberPad2:
                case VirtualKey.Number2:
                    btnValuPressed = "2";
                    break;
                case VirtualKey.NumberPad3:
                case VirtualKey.Number3:
                    btnValuPressed = "3";
                    break;
                case VirtualKey.NumberPad4:
                case VirtualKey.Number4:
                    btnValuPressed = "4";
                    break;
                case VirtualKey.NumberPad5:
                case VirtualKey.Number5:
                    btnValuPressed = "5";
                    break;
                case VirtualKey.NumberPad6:
                case VirtualKey.Number6:
                    btnValuPressed = "6";
                    break;
                case VirtualKey.NumberPad7:
                case VirtualKey.Number7:
                    btnValuPressed = "7";
                    break;
                case VirtualKey.NumberPad8:
                case VirtualKey.Number8:
                    btnValuPressed = "8";
                    break;
                case VirtualKey.NumberPad9:
                case VirtualKey.Number9:
                    btnValuPressed = "9";
                    break;
            }
            setControlView(btnValuPressed);
        }

        //TODO: sistemare
        CaptureElement capturePreview;
        MediaCapture captureMgr;
        ImageEncodingProperties imageProperties = ImageEncodingProperties.CreateJpeg();
        WriteableBitmap wBitmap;

        private async void apri_camera_Click(object sender, RoutedEventArgs e)
        {
            salva_camera.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //this.capturePreview = inputCamera;
            //this.captureMgr = new MediaCapture();

            //MediaCaptureInitializationSettings settings = new Windows.Media.Capture.MediaCaptureInitializationSettings();
            //settings.StreamingCaptureMode = StreamingCaptureMode.Video;
            //await this.captureMgr.InitializeAsync(settings);
            //this.capturePreview.Source = captureMgr;
            //await this.captureMgr.StartPreviewAsync();

            CameraCaptureUI cameraUI = new CameraCaptureUI();

            cameraUI.PhotoSettings.AllowCropping = false;
            cameraUI.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.MediumXga;

            Windows.Storage.StorageFile capturedMedia =
                await cameraUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (capturedMedia != null)
            {
                using (var streamCamera = await capturedMedia.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    BitmapImage bitmapCamera = new BitmapImage();
                    bitmapCamera.SetSource(streamCamera);
                    // To display the image in a XAML image object, do this:
                    // myImage.Source = bitmapCamera;

                    // Convert the camera bitap to a WriteableBitmap object, 
                    // which is often a more useful format.

                    int width = bitmapCamera.PixelWidth;
                    int height = bitmapCamera.PixelHeight;

                    wBitmap = new WriteableBitmap(width, height);

                    using (var stream = await capturedMedia.OpenAsync(Windows.Storage.FileAccessMode.Read))
                    {
                        wBitmap.SetSource(stream);
                    }
                }
            }
        }

        private async void salva_camera_Click(object sender, RoutedEventArgs e)
        {
            //TODO: salvare immagine
        //captureMgr.get

            // Create the File Picker control
            Windows.Storage.Pickers.FileSavePicker picker = new Windows.Storage.Pickers.FileSavePicker();
            picker.FileTypeChoices.Add("JPG File", new List<string>() { ".jpg" });
            Windows.Storage.StorageFile file = await picker.PickSaveFileAsync();

            if (file != null)
            {
                // If the file path and name is entered properly, and user has not tapped 'cancel'..

                using (Windows.Storage.Streams.IRandomAccessStream stream = await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
                {
                    // Encode the image into JPG format,reading for saving
                    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
                    Stream pixelStream = wBitmap.PixelBuffer.AsStream();
                    byte[] pixels = new byte[pixelStream.Length];
                    await pixelStream.ReadAsync(pixels, 0, pixels.Length);
                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)wBitmap.PixelWidth, (uint)wBitmap.PixelHeight, 96.0, 96.0, pixels);
                    await encoder.FlushAsync();
                }
            }

        }
        #endregion
    }
}