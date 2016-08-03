using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band;
using Microsoft.Band.Sensors;
using Windows.UI.Xaml;



namespace Accelerometer
{
    public class BandAPI
    {
        public enum VibrateBand: int {Win = 1, HitBall = 2};

        private static double accel = 0;

        private static IBandClient bandClient;
        private static BandAPI instance;

        public static BandAPI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BandAPI();
                }
                return instance;
            }
        }

        public async void StartBandAPI()
        {
            
            try
            {
                // Get the list of Microsoft Bands paired to the device.
                IBandInfo[] pairedBands = await BandClientManager.Instance.GetBandsAsync();
                if (pairedBands.Length < 1)
                {
                    string needBand = "Need to pair Band";
                    System.Diagnostics.Debug.WriteLine(needBand);
                    return;
                }

                // Connect to Microsoft Band.
                using (bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]))
                {

                    // Subscribe to Accelerometer data.
                    bandClient.SensorManager.Accelerometer.ReadingChanged += (s, args) =>
                    {
                        accel = Accelerometer_ReadingChanged(s, args);
                    };
                    System.Diagnostics.Debug.WriteLine("Retrieving accelerometer data");
                    await bandClient.SensorManager.Accelerometer.StartReadingsAsync();
                    // Keep retrieving Accelerometer data for an hour
                    await Task.Delay(TimeSpan.FromHours(1));
                    await bandClient.SensorManager.Accelerometer.StopReadingsAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        public async void StopBandAPI(object sender, RoutedEventArgs e)
        {
            try
            {
                await bandClient.SensorManager.Accelerometer.StopReadingsAsync();
                System.Diagnostics.Debug.WriteLine("Stop retrieving accelerometer data");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private double Accelerometer_ReadingChanged(object sender, BandSensorReadingEventArgs<IBandAccelerometerReading> e)
        {
            IBandAccelerometerReading accel = e.SensorReading;
            //Read in accelerometer data from sensor
            string text = string.Format("Accelerometer X = {0:G4}, Y = {1:G4}, Z = {2:G4}", accel.AccelerationX, accel.AccelerationY, accel.AccelerationZ);
            System.Diagnostics.Debug.WriteLine(text);
            return Math.Abs(Math.Max(accel.AccelerationX, Math.Max(accel.AccelerationY, accel.AccelerationZ)));
        }

        public static double getAcceleration() {
            return accel;
        }

        
        public static void vibrateBand(VibrateBand vibrationType) {
            switch(vibrationType)
            {
                case VibrateBand.Win:
                    bandClient.NotificationManager.VibrateAsync(Microsoft.Band.Notifications.VibrationType.ThreeToneHigh);
                    break;
                case VibrateBand.HitBall:
                    bandClient.NotificationManager.VibrateAsync(Microsoft.Band.Notifications.VibrationType.OneToneHigh);
                    break;
            }
        }



    }
}
