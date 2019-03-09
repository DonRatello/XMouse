using System;
using System.Windows;
using System.Windows.Threading;

namespace XMouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XController controller;
        ProcessObserver observer;
        DispatcherTimer timer;
        DispatcherTimer processObserverTimer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            controller = new XController();
            observer = new ProcessObserver();

            imgGreen.Visibility = Visibility.Hidden;
            imgRed.Visibility = Visibility.Hidden;

            // Handling window position
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Top + this.Height;

            // Timer for UPDATE actions
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            timer.Start();

            // Timer for Process observer
            processObserverTimer = new DispatcherTimer();
            processObserverTimer.Tick += new EventHandler(processObserver_Tick);
            processObserverTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            processObserverTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            controller.Update();

            if (controller.IsEnabled)
            {
                SetImgGreen();
            }
            else
            {
                SetImgRed();
            }
        }

        private void processObserver_Tick(object sender, EventArgs e)
        {
            if (observer.ControlShouldBeStopped())
            {
                timer.Stop();
                SetImgRed();
            }
            else
            {
                timer.Start();
                SetImgGreen();
            }
        }

        private void SetImgGreen()
        {
            imgGreen.Visibility = Visibility.Visible;
            imgRed.Visibility = Visibility.Hidden;
        }

        private void SetImgRed()
        {
            imgGreen.Visibility = Visibility.Hidden;
            imgRed.Visibility = Visibility.Visible;
        }
    }
}
