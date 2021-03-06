﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
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

        public static DependencyProperty CurrentValueProperty = DependencyProperty.Register("Numbers", typeof(string), typeof(MainPage), new PropertyMetadata(""));

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

                setImage(Int32.Parse(content));
                playMp3(content);
            }
        }

        private void playMp3(string content)
        {
            if (FindName("sound" + content) is MediaElement)
                (FindName("sound" + content) as MediaElement).Play();
        }

        async private  void setImage(int numberImgVisible)
        {
            for (int i = 0; i < 10; i++)
            {
                if (FindName("i" + i) is Image)
                {
                    if (i <= numberImgVisible)
                    {
                        try
                        {
                            StorageFile fileGood = await ApplicationData.Current.LocalFolder.GetFileAsync(String.Format(Constant.NameImageWork, ManageImage.CountCapture));
                            (FindName("i" + i) as Image).Source = new BitmapImage(new Uri(fileGood.Path));
                            (FindName("i" + i) as Image).CacheMode = new BitmapCache();
                        }
                        catch { }

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
            setImage(0);
        }

        private void btnNumber_Click(object sender, RoutedEventArgs e)
        {
            setControlView(((sender as Button).Content as TextBlock).Text);
        }

        private void gridContainer_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            //TODO: doppio evento da capire il perchè
            var btnValuPressed = "";
            switch (e.Key)
            {
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

        private void Button_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Button)
                setControlView(((sender as Button).Content as TextBlock).Text);
        }

        private void photo_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PhotoPage));
        }
        #endregion
    
    }
}