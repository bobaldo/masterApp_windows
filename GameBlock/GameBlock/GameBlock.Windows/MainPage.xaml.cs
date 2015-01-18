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

        private void setControlView(Storyboard story, TimeSpan ts, string addNumber)
        {
            if(!String.IsNullOrEmpty(addNumber))
            {
                if (numbers <= 0)
                    Numbers = Numbers.Remove(0, 1);
                Numbers += addNumber;
                numbers -= 1;
            }
            story.RepeatBehavior = new RepeatBehavior(ts);
            story.Begin();
        }

        #region Event Handler
        private void r1_Click(object sender, RoutedEventArgs e)
        {
             setControlView(s1, new TimeSpan(0, 0, 2), ((sender as Button).Content as TextBlock).Text);
        }

        private void r2_Click(object sender, RoutedEventArgs e)
        {
            setControlView(s2, new TimeSpan(0, 0, 4),((sender as Button).Content as TextBlock).Text);
        }

        private void r3_Click(object sender, RoutedEventArgs e)
        {
            setControlView(s3, new TimeSpan(0, 0, 3),((sender as Button).Content as TextBlock).Text);
        }

        private void r4_Click(object sender, RoutedEventArgs e)
        {
            setControlView(s4, new TimeSpan(0, 0, 5), ((sender as Button).Content as TextBlock).Text);
        }

        private void r5_Click(object sender, RoutedEventArgs e)
        {
            setControlView(s5, new TimeSpan(0, 0, 4), ((sender as Button).Content as TextBlock).Text);
        }

        private void r6_Click(object sender, RoutedEventArgs e)
        {
            setControlView(s6, new TimeSpan(0, 0, 1), ((sender as Button).Content as TextBlock).Text);
        }

        private void r7_Click(object sender, RoutedEventArgs e)
        {
            setControlView(s7, new TimeSpan(0, 0, 1), ((sender as Button).Content as TextBlock).Text);
        }

        private void r8_Click(object sender, RoutedEventArgs e)
        {
            setControlView(s8, new TimeSpan(0, 0, 2), ((sender as Button).Content as TextBlock).Text);
        }

        private void r9_Click(object sender, RoutedEventArgs e)
        {
            setControlView(s9, new TimeSpan(0, 0, 2), ((sender as Button).Content as TextBlock).Text);
        }

        private void r0_Click(object sender, RoutedEventArgs e)
        {
           setControlView(s01, new TimeSpan(0, 0, 1), ((sender as Button).Content as TextBlock).Text);
        }

        private void s01_Completed(object sender, object e)
        {
            setControlView(s02, new TimeSpan(0, 0, 1),null);
        }

        private void s02_Completed(object sender, object e)
        {
            setControlView(s03, new TimeSpan(0, 0, 1), null);
        }

        private void s03_Completed(object sender, object e)
        {
            setControlView(s04, new TimeSpan(0, 0, 1), null);
        }

        private void erase_Click(object sender, RoutedEventArgs e)
        {
            Numbers = "";
            numbers = 10;
        }
        #endregion
    }
}
