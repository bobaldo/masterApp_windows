using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Devices.Sensors;

namespace EsempioSQLite
{


    [Table("Users")]
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }

    public class Test
    {
        private async Task<bool> ChechDBAsync(string dbName)
        {
            var exist = true;
            try
            {
                var sf = await ApplicationData.Current.LocalFolder.GetFileAsync(dbName);
            }
            catch
            {
                exist = false;
            }
            return exist;
        }

        public async void TestDB()
        {
            #region Sensors
            /*
            var acc = Windows.Devices.Sensors.Accelerometer.GetDefault();
            acc.ReadingChanged += (o, e) =>
            {
                var x = e.Reading.AccelerationX;
                var y = e.Reading.AccelerationY;
                var z = e.Reading.AccelerationZ;
                var t = e.Reading.Timestamp;
            };

            var inc = Windows.Devices.Sensors.Inclinometer.GetDefault();
            inc.ReadingChanged += (o, e) => {
                var pd = e.Reading.PitchDegrees; //asse x
                var rd = e.Reading.RollDegrees; //aase y
                var yd = e.Reading.YawDegrees; //asse z
                var t = e.Reading.Timestamp;
                var ya = e.Reading.YawAccuracy;
            };

            var gio = Windows.Devices.Sensors.Gyrometer.GetDefault();
            gio.ReadingChanged += (o, e) =>
            {
                var x = e.Reading.AngularVelocityX;
                var y = e.Reading.AngularVelocityY;
                var z = e.Reading.AngularVelocityZ;
                var t = e.Reading.Timestamp;
            };

            var com = Windows.Devices.Sensors.Compass.GetDefault();
            com.ReadingChanged += (o, e) =>
            {
                var ha = e.Reading.HeadingAccuracy; 
                var hmn = e.Reading.HeadingMagneticNorth; 
                var htn = e.Reading.HeadingTrueNorth; 
                var t = e.Reading.Timestamp;
            };

            var geo =new Windows.Devices.Geolocation.Geolocator();
            geo.DesiredAccuracy = Windows.Devices.Geolocation.PositionAccuracy.High;
            geo.DesiredAccuracyInMeters = 100;
            geo.PositionChanged += (o, e) =>
            {
                var ca = e.Position.CivicAddress; 
                var co = e.Position.Coordinate; 
            };

            var b = Windows.Devices.WiFiDirect.WiFiDirectDevice.GetDeviceSelector();
            var wifi =Windows.Devices.WiFiDirect.WiFiDirectDevice.FromIdAsync(b);
             * */
            #endregion

            var db = new SQLiteAsyncConnection("test db");
            await db.CreateTableAsync<Users>();

            var u = new Users { Name = "Davide Patrizi", City = "Frosinone" };
            await db.InsertAsync(u);

        }
    }
}   