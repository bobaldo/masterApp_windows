using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace ExUniversalApp
{
    public sealed partial class MainPage : Page
    {
        public static int DELTA = 10;
        public static int INITVALUETOPBARRIERA = 250;
        public static int INITVALUEHEIGTHBARRIERA = 100;
        public static int HEIGTHCANVAS = 500;
        public static bool started = false;
        public static DependencyProperty CurrentTopBarriera = DependencyProperty.Register("TopBarriera", typeof(int), typeof(MainPage), new PropertyMetadata(INITVALUETOPBARRIERA));
        public static DependencyProperty CurrentHeigthBarriera = DependencyProperty.Register("HeigthBarriera", typeof(int), typeof(MainPage), new PropertyMetadata(INITVALUEHEIGTHBARRIERA));

        public int HeigthBarriera
        {
            get { return (int)GetValue(CurrentHeigthBarriera); }
            set { SetValue(CurrentHeigthBarriera, value); }
        }
        
        public int TopBarriera
        {
            get { return (int)GetValue(CurrentTopBarriera); }
            set { SetValue(CurrentTopBarriera, value); }
        }

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnInizia_Click(object sender, RoutedEventArgs e)
        {
            started = true;
            btnInizia.IsEnabled = false;
            ss1.Begin();
        }

        private void ss1_Completed(object sender, object e)
        {
            ss2.Begin();
        }

        private void ss2_Completed(object sender, object e)
        {
            ss3.Begin();
        }

        private void ss3_Completed(object sender, object e)
        {
            ss4.Begin();
        }

        private void ss4_Completed(object sender, object e)
        {
            ss1.Begin();
        }

        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (started)
            {
                //HEIGTHCANVAS - HeigthBarriera
                switch (e.Key)
                {
                    case Windows.System.VirtualKey.Down:
                        if ((TopBarriera + HeigthBarriera + DELTA) <= HEIGTHCANVAS)
                            TopBarriera = TopBarriera + DELTA;
                        break;
                    case Windows.System.VirtualKey.Up:
                        if ((TopBarriera - DELTA) >= 0)
                            TopBarriera = TopBarriera - DELTA;
                        break;
                }
            }
        }
    }

    public class PositiveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var v = int.Parse(value.ToString());
            return v >= 0 ? "positive" : "negative";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}