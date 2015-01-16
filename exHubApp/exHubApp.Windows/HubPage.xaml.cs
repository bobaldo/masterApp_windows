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
using Windows.UI.Xaml.Navigation;
using exHubApp.Data;
using exHubApp.Common;
using Windows.UI.Xaml.Media.Animation;

// The Universal Hub Application project template is documented at http://go.microsoft.com/fwlink/?LinkID=391955

namespace exHubApp
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class HubPage : Page
    {
        public static DependencyProperty CurrentValueProperty = 
            DependencyProperty.Register("CurrentValue", typeof(string), typeof(HubPage), new PropertyMetadata("0"));

        private int value;
        private int acc;
        private Operation lastOp;
        public string CurrentValue
        {
            get { return GetValue(CurrentValueProperty) as string; }
            set
            {
                porco.Begin();
                SetValue(CurrentValueProperty, value);
            }
        }
        
        private enum Operation { None, Piu }

        public HubPage()
        {
            this.InitializeComponent();
            lastOp = Operation.None;
            value = 0;
            acc = 0;
        }

        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            value = (((sender as Button).Content as String)[0] - '0') + value * 10;
            CurrentValue = value.ToString();
        }

        private void Op_Click(object sender, RoutedEventArgs e)
        {
            var label = ((sender as Button).Content as String);

            switch (lastOp)
            {
                case Operation.None:
                    acc = value;
                    value = 0;
                    break;
                case Operation.Piu:
                    acc += value;
                    value = 0;
                    break;
            }

            switch (label)
            {
                case "+":
                    lastOp = Operation.Piu;
                    break;
                case "=":
                    lastOp = Operation.None;
                    CurrentValue = acc.ToString();
                    break;
            }
        }
    }
}