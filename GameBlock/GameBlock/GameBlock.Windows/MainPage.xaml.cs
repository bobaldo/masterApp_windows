using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
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
            }
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
        #endregion

    }
}