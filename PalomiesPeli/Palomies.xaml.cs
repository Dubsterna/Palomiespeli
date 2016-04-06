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
    public sealed partial class Palomies : UserControl
    {
        // animate butterfly
        private DispatcherTimer timer;
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        //animate
        private int currentFrame = 0;
        private int direction = 1;
        private int frameHeight = 70;
        // speed
        private readonly double Step = 5;
        public Palomies()
        {
            this.InitializeComponent();
            
        }
        private void Animate()
        {
            // current frame 0->4, 4->0
            if (direction == 1) currentFrame++;
            else currentFrame--;
            if (currentFrame == 0 || currentFrame == 1)
                direction *= -1; // 1 tai -1
            // offset
            SpriteSheetOffset.Y = currentFrame * -frameHeight;
        }
        //animate butterfly
        /*private void Animate()
        {
            timer_tick will be called in every 125 ms
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 125);
            timer.Start();
        }*/

        /*private void Timer_Tick(object sender, object e)
        {
            // current frame 0->4, 4->0
            if (direction == 1) currentFrame++;
            else currentFrame--;
            if (currentFrame == 0 || currentFrame == 1)
                direction *= -1; // 1 tai -1
            // offset
            SpriteSheetOffset.Y = currentFrame * -frameHeight;

        }*/
        // show butterfly in canvas (right location)
        public void SetLocation()
        {
            SetValue(Canvas.LeftProperty, LocationX);
            SetValue(Canvas.TopProperty, LocationY);
        }
        public void Move(int direction)
        {
             
            LocationX = LocationX + Step * direction;
            SetValue(Canvas.LeftProperty, LocationX);
            Animate();
            
            if (LocationX > 500)
            {
                LocationX = 500;
                SetValue(Canvas.LeftProperty, LocationX);
                Animate();
            }
            if (LocationX < 0)
                {
                    LocationX = 0;
                    SetValue(Canvas.LeftProperty, LocationX);
                    Animate();
                }
        }
    }
}
