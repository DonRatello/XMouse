using SharpDX.XInput;
using System;
using System.Windows;

namespace XMouse
{
    public class XController
    {
        public Point thumbLeft;
        public Point thumbRight;

        protected Controller controller;
        protected Gamepad gamepad;
        protected const int deadband = 2500;
        protected Point previousThumbLeft, previousThumbRight;

        protected double diffX, diffY;

        public bool IsEnabled { get; set; }
        public bool IsConnected => controller != null ? controller.IsConnected : false;

        public XController()
        {
            controller = new Controller(UserIndex.One);
            thumbLeft = new Point(0, 0);
            thumbRight = new Point(0, 0);
        }

        public void Update()
        {
            if (!IsConnected) return;

            gamepad = controller.GetState().Gamepad;

            // Back and Start buttons are controlling mouse movements
            if (gamepad.Buttons.HasFlag(GamepadButtonFlags.Back) && gamepad.Buttons.HasFlag(GamepadButtonFlags.Start))
            {
                IsEnabled = IsEnabled ? false : true;
                System.Threading.Thread.Sleep(500);
            }

            if (!IsEnabled) return;

            thumbLeft.X = (Math.Abs((float)gamepad.LeftThumbX) < deadband) ? 0 : (float)gamepad.LeftThumbX / short.MinValue * -100;
            thumbLeft.Y = (Math.Abs((float)gamepad.LeftThumbY) < deadband) ? 0 : (float)gamepad.LeftThumbY / short.MaxValue * 100;
            //thumbRight.Y = (Math.Abs((float)gamepad.RightThumbX) < deadband) ? 0 : (float)gamepad.RightThumbX / short.MaxValue * 100;
            //thumbRight.X = (Math.Abs((float)gamepad.RightThumbY) < deadband) ? 0 : (float)gamepad.RightThumbY / short.MaxValue * 100;

            if (previousThumbLeft == null)
            {
                previousThumbLeft = thumbLeft;
                return;
            }

            diffX = NormalizeValue(thumbLeft.X - previousThumbLeft.X);
            diffY = NormalizeValue(thumbLeft.Y - previousThumbLeft.Y);

            MoveMouse();
        }

        protected void MoveMouse()
        {
            Point currentPos = MouseHandler.GetMousePosition();
            MouseHandler.SetMousePosition(
                Convert.ToInt32(currentPos.X) + Convert.ToInt32(Math.Round(diffX)), 
                Convert.ToInt32(currentPos.Y) - Convert.ToInt32(Math.Round(diffY))
                );
        }

        protected double NormalizeValue(double value)
        {
            const double max = 20;
            const double min = 0;

            return (value - min) / (max - min);
        }
    }
}
