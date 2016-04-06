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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PalomiesPeli
{
    public sealed partial class fallingObject : UserControl
    {
        // animate butterfly
        private DispatcherTimer timer;

        public double LocationX { get; set; }
        //public double LocationY { get; set; }
        public double LocationY { get; set; }
        public fallingObject()
        {
            this.InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();
            //SetValue(Canvas.LeftProperty, LocationX);
        }



        private void Timer_Tick(object sender, object e)
        {

            LocationY += 5;
            SetValue(Canvas.LeftProperty, LocationX);
            SetValue(Canvas.TopProperty, LocationY);
            if (LocationY >= 600)
            {
                timer.Stop();
                

            }





        }
    }
}
    

