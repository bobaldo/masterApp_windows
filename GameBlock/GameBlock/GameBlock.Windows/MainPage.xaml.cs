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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GameBlock
{
    public sealed partial class MainPage : Page
    {
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

        private void animate(Storyboard story, TimeSpan ts)
        {
            story.RepeatBehavior = new RepeatBehavior(ts);
            story.Begin();
        }

        #region Event Handler
        private void r1_Click(object sender, RoutedEventArgs e)
        {
            Numbers += ((sender as Button).Content as TextBlock).Text;
            animate(s1, new TimeSpan(0, 0, 2));
        }

        private void r2_Click(object sender, RoutedEventArgs e)
        {
            Numbers += ((sender as Button).Content as TextBlock).Text;
            animate(s2, new TimeSpan(0, 0, 4));
        }

        private void r3_Click(object sender, RoutedEventArgs e)
        {
            Numbers += ((sender as Button).Content as TextBlock).Text;
            animate(s3, new TimeSpan(0, 0, 3));
        }

        private void r4_Click(object sender, RoutedEventArgs e)
        {
            Numbers += ((sender as Button).Content as TextBlock).Text;
            animate(s4, new TimeSpan(0, 0, 5));
        }

        private void r5_Click(object sender, RoutedEventArgs e)
        {
            Numbers += ((sender as Button).Content as TextBlock).Text;
            animate(s5, new TimeSpan(0, 0, 4));
        }

        private void r6_Click(object sender, RoutedEventArgs e)
        {
            Numbers += ((sender as Button).Content as TextBlock).Text;
            animate(s6, new TimeSpan(0, 0, 1));
        }

        private void r7_Click(object sender, RoutedEventArgs e)
        {
            Numbers += ((sender as Button).Content as TextBlock).Text;
            animate(s7, new TimeSpan(0, 0, 1));
        }

        private void r8_Click(object sender, RoutedEventArgs e)
        {
            Numbers += ((sender as Button).Content as TextBlock).Text;
            animate(s8, new TimeSpan(0, 0, 2));
        }

        private void r9_Click(object sender, RoutedEventArgs e)
        {
            Numbers += ((sender as Button).Content as TextBlock).Text;
            animate(s9, new TimeSpan(0, 0, 2));
        }

        private void r0_Click(object sender, RoutedEventArgs e)
        {
            Numbers += ((sender as Button).Content as TextBlock).Text;
            animate(s01, new TimeSpan(0, 0, 1));
        }

        private void s01_Completed(object sender, object e)
        {
            animate(s02, new TimeSpan(0, 0, 1));
        }

        private void s02_Completed(object sender, object e)
        {
            animate(s03, new TimeSpan(0, 0, 1));
        }

        private void s03_Completed(object sender, object e)
        {
            animate(s04, new TimeSpan(0, 0, 1));
        }
        #endregion



    }
}
