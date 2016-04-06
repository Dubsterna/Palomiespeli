using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PalomiesPeli
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // butterfly
        private Palomies palomies;
        // game loop timer
        private DispatcherTimer timer;
        
        // lista putoavista obuista
        private List<fallingObject> fallingObjects = new List<fallingObject>();
        // which keys are pressed
        private bool RightPressed;
        private bool LeftPressed;

        public fallingObject FallingObject;
        private DispatcherTimer fallingtimer;
        private int Points;
        private int Life=3;
        public MainPage()
        {
            this.InitializeComponent();


            ApplicationView.PreferredLaunchWindowingMode
            = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchViewSize = new Size(1024, 720);

            // key listeners
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            fallingtimer = new DispatcherTimer();
            fallingtimer.Tick += fallingTimer_Tick;
            fallingtimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            fallingtimer.Start();
            //CreateFallingObject();
            CreatePalomies();
            StartGame();
        }

        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    LeftPressed = false;
                    break;
                case VirtualKey.Right:
                    RightPressed = false;
                    break;
                default:
                    break;

            }
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    LeftPressed = true;
                    break;
                case VirtualKey.Right:
                    RightPressed = true;
                    break;
                default:
                    break;

            }
        }

        private void CreatePalomies()
        {
            palomies = new Palomies
            {
                LocationX = MyCanvas.Width / 2 - 75,
                LocationY = 530
            };
            // add to canvas
            MyCanvas.Children.Add(palomies);
            // show in right location
            palomies.SetLocation();
        }
        private void CreateFallingObject()
        {
            Random rdnX = new Random();
            Random rdnY = new Random();
            int StartingPlaceX = rdnX.Next(1, 5) * 100;
            int StartingPlaceY = rdnY.Next(0, 2) * 100;


            FallingObject = new fallingObject
            {
                LocationX = StartingPlaceX,
                LocationY = StartingPlaceY
                //LocationX = MyCanvas.Width / 2 - 75,
                //LocationY = MyCanvas.Height / 2 - 66
            };
            // add to canvas
            MyCanvas.Children.Add(FallingObject);
            // show in right location
            fallingObjects.Add(FallingObject);
        }

        private void fallingTimer_Tick(object sender, object e)
        {
            CreateFallingObject();
        }
        private void StartGame()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            timer.Start(); // stop!

        }
        private void Timer_Tick(object sender, object e)
        {
            // move palomies
            if (LeftPressed) palomies.Move(-1);
            if (RightPressed) palomies.Move(1);
            // update palomies location
            palomies.SetLocation();

            CheckCollision();

        }

        // check collision with flowers and butterfly
        private void CheckCollision()
        {

            foreach (fallingObject FallingObject in fallingObjects) { 
                // get rects
            Rect r1 = new Rect((palomies.LocationX + 30), (palomies.LocationY + 30), 40, 5); // palomies
            Rect r2 = new Rect(FallingObject.LocationX, FallingObject.LocationY, FallingObject.ActualWidth, FallingObject.ActualHeight); // loota
                                                                                                                               // does intersects happen
            r1.Intersect(r2);
            // if not empty
            if (!r1.IsEmpty)
            {
                // remove flower
                MyCanvas.Children.Remove(FallingObject);
                //CreateFallingObject();
                fallingObjects.Remove(FallingObject);
                Points++;
                    pointsTextBlock.Text = "Pisteet: " + Points;
                // end looping...
                break;
                }
           
            

            }
            foreach (fallingObject Missed in fallingObjects)
            {
                Rect r2 = new Rect(FallingObject.LocationX, FallingObject.LocationY, FallingObject.ActualWidth, FallingObject.ActualHeight);
                Rect r3 = new Rect(0, 600, 600, 1);
                r3.Intersect(r2);
                if(!r3.IsEmpty)
                {
                    Life--;
                    pointsTextBlock.Text = "toimiii";
                    break;
                }
            }
            if (Life == 0)
            {
                fallingtimer.Stop();
            }
        }
    }
}
