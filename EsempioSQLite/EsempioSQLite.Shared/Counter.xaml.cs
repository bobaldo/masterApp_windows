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
using SQLite;
using Windows.Devices.Geolocation;
using Windows.UI.Core;

namespace EsempioSQLite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Counter : Page
    {
        SQLiteAsyncConnection conn;
        Geolocator gps;

        public Counter()
        {
            this.InitializeComponent();
            conn = new SQLite.SQLiteAsyncConnection("location.db");
        }

        async void gps_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            var row = new Model.UserLocation
            {
                TimeStamp = DateTime.Now,
                Latidute = args.Position.Coordinate.Point.Position.Longitude,
                Longitude = args.Position.Coordinate.Point.Position.Longitude
            };
            var ins = conn.InsertAsync(row);

            //serve per aggiornare la UI
            var upUI = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                lblOut.Text = String.Format("({0}, {1})", row.Latidute, row.Longitude);
            });

            await ins;
            await upUI;
        }

        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            await conn.CreateTableAsync<Model.UserLocation>();
            gps = new Geolocator();
            gps.DesiredAccuracyInMeters = 500;
            gps.MovementThreshold = 1;
            gps.PositionChanged += gps_PositionChanged;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            gps.PositionChanged -= gps_PositionChanged;
            gps = null;
        }
    }
}