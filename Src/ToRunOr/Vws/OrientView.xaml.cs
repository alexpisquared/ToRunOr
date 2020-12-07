using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ToRunOr.Vws
{
    public sealed partial class OrientView : Page
    {
        public OrientView()
        {
            this.InitializeComponent();

            _simpleorientation = SimpleOrientationSensor.GetDefault(); if (_simpleorientation != null) _simpleorientation.OrientationChanged += new TypedEventHandler<SimpleOrientationSensor, SimpleOrientationSensorOrientationChangedEventArgs>(OrientationChanged);

            _sensor = OrientationSensor.GetDefault();
            if (_sensor != null)
            {
                _sensor.ReportInterval = _sensor.MinimumReportInterval > 16 ? _sensor.MinimumReportInterval : 16;
                _sensor.ReadingChanged += new TypedEventHandler<OrientationSensor, OrientationSensorReadingChangedEventArgs>(ReadingChanged);
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) { if (Window.Current.Content is Frame rootFrame && rootFrame.CanGoBack) SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible; }


        SimpleOrientationSensor _simpleorientation; // https://msdn.microsoft.com/en-us/windows/uwp/devices-sensors/use-the-orientation-sensor
        OrientationSensor _sensor;

        async void OrientationChanged(object sender, SimpleOrientationSensorOrientationChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                tbOri.Text = $"{e.Orientation}";
            });
        }
        async void ReadingChanged(object sender, OrientationSensorReadingChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                OrientationSensorReading reading = e.Reading;
                var f = "{0,8:N1}";

                // Quaternion values
                txtQuaternionX.Text = String.Format(f, reading.Quaternion.X);
                txtQuaternionY.Text = String.Format(f, reading.Quaternion.Y);
                txtQuaternionZ.Text = String.Format(f, reading.Quaternion.Z);
                txtQuaternionW.Text = String.Format(f, reading.Quaternion.W);

                // Rotation Matrix values
                txtM11.Text = String.Format(f, reading.RotationMatrix.M11);
                txtM12.Text = String.Format(f, reading.RotationMatrix.M12);
                txtM13.Text = String.Format(f, reading.RotationMatrix.M13);
                txtM21.Text = String.Format(f, reading.RotationMatrix.M21);
                txtM22.Text = String.Format(f, reading.RotationMatrix.M22);
                txtM23.Text = String.Format(f, reading.RotationMatrix.M23);
                txtM31.Text = String.Format(f, reading.RotationMatrix.M31);
                txtM32.Text = String.Format(f, reading.RotationMatrix.M32);
                txtM33.Text = String.Format(f, reading.RotationMatrix.M33);
            });
        }
    }
}
