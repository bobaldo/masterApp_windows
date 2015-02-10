using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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

namespace UniversalClock
{
    public sealed partial class ClockControl : UserControl
    {
        public static DependencyProperty AnalogClockEnabledProperty = DependencyProperty.Register("AnalogClockEnabled", typeof(bool), typeof(ClockControl), new PropertyMetadata(true));
        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(ClockControl), new PropertyMetadata("Clock"));
        public static DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(DateTime), typeof(ClockControl), new PropertyMetadata(DateTime.Now));

        public bool AnalogClockEnabled
        {
            get { return (bool)GetValue(AnalogClockEnabledProperty); }
            set { SetValue(AnalogClockEnabledProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public DateTime Time
        {
            get { return (DateTime)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public ClockControl()
        {
            this.InitializeComponent();
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer t = new DispatcherTimer();
            t.Tick += t_Tick;
            t.Interval = new TimeSpan(0, 0, 1);
            t.Start();
        }

        void t_Tick(object sender, object e)
        {
            Debug.WriteLine("{0}", DateTime.Now);
            Time = Time.Add(new TimeSpan(0, 0, 1));
            rtSeconds.Angle = Time.Second * 6;
            rtMinutes.Angle = Time.Minute * 6;
            rtHours.Angle = Time.Hour * 6 * 5;
            Title = Time.ToString("hh:mm tt");
        }
    }
}