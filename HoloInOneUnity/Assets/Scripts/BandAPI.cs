using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
#if REAL_APP
using System.Threading.Tasks;
using Microsoft.Band;
using Microsoft.Band.Sensors;
using Windows.UI.Xaml;
#endif

public class BandAPI
{
    public enum VibrateBand: int {Win = 1, HitBall = 2};

    private static double accel = 0;
#if REAL_APP
    private static IBandClient bandClient;
#endif
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

    private BandAPI()
    {

    }
#if REAL_APP
    public async void StartBandAPI()
#else
    public void StartBandAPI()
#endif
    {
#if REAL_APP
        UnityEngine.Debug.Log("Sarting App");
        try
        {
            
            // Get the list of Microsoft Bands paired to the device.
            IBandInfo[] pairedBands = await BandClientManager.Instance.GetBandsAsync();
            
            if (pairedBands.Length < 1)
            {
                string needBand = "Need to pair Band";
                System.Diagnostics.Debug.WriteLine(needBand);
                UnityEngine.Debug.Log(needBand);
                return;
            }
            UnityEngine.Debug.Log("1");
            // Connect to Microsoft Band.
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                using (bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]))
                {
                    UnityEngine.Debug.Log("2");
                    UnityEngine.Debug.Log("---Connecting to band: " + pairedBands[0].ToString());
                    // Subscribe to Accelerometer data.
                    bandClient.SensorManager.Accelerometer.ReadingChanged += (s, args) =>
                    {
                        accel = Accelerometer_ReadingChanged(s, args);
                    };
                    System.Diagnostics.Debug.WriteLine("Retrieving accelerometer data");
                    UnityEngine.Debug.Log("Retrieving accelerometer data");
                    await bandClient.SensorManager.Accelerometer.StartReadingsAsync();
                    // Keep retrieving Accelerometer data for an hour
                    await Task.Delay(TimeSpan.FromHours(1));
                    await bandClient.SensorManager.Accelerometer.StopReadingsAsync();
                }
            });

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
            UnityEngine.Debug.Log(ex.ToString());
        }
#endif
    }
#if REAL_APP
    private  async void StopBandAPI(object sender, RoutedEventArgs e)
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
#endif

    public double GetAcceleration() {
        return accel;
    }

        
    public void CauseVibration(VibrateBand vibrationType)
    {
#if REAL_APP
        switch (vibrationType)
        {
            case VibrateBand.Win:
                bandClient.NotificationManager.VibrateAsync(Microsoft.Band.Notifications.VibrationType.ThreeToneHigh);
                break;
            case VibrateBand.HitBall:
                bandClient.NotificationManager.VibrateAsync(Microsoft.Band.Notifications.VibrationType.OneToneHigh);
                break;
        }
#endif
    }
        
}