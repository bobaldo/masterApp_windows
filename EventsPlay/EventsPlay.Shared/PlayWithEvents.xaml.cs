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

namespace EventsPlay
{
    public sealed partial class PlayWithEvents : UserControl
    {
        public PlayWithEvents()
        {
            this.InitializeComponent();
            bordo.PointerMoved += (c, e) =>
            {
                bordo.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Yellow);
            };

            bordo.ManipulationStarted += (c, e) => { 
            
            };

            bordo.ManipulationDelta += (c, e) =>
            {
            
            };

            bordo.ManipulationCompleted += (c, e) =>
            {

            };

            bordo.PointerExited += (c, e) =>
            {
                    bordo.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
            };
        }
    }
}