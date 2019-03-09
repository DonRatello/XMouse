using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace XMouse
{
    public class MouseHandler
    {
        protected const int MOUSEEVENTF_LEFTDOWN = 0x02;
        protected const int MOUSEEVENTF_LEFTUP = 0x04;
        protected const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        protected const int MOUSEEVENTF_RIGHTUP = 0x10;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [DllImport("user32.dll")]
        internal static extern long SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        internal static extern bool ClientToScreen(IntPtr hWnd, ref Win32Point point);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public int X;
            public int Y;
        };

        public static Point GetMousePosition()
        {
            try
            {
                var w32Mouse = new Win32Point();
                GetCursorPos(ref w32Mouse);
                return new Point(w32Mouse.X, w32Mouse.Y);
            }
            catch (Exception) { return new Point(0, 0); }
        }

        public static void SetMousePosition(int x, int y)
        {
            try
            {
                var p = new Win32Point
                {
                    X = Convert.ToInt16(x),
                    Y = Convert.ToInt16(y)
                };
                SetCursorPos(p.X, p.Y);
            }
            catch (Exception) { }
        }

        public static void LeftMouseClick()
        {
            try
            {
                var w32Mouse = new Win32Point();
                GetCursorPos(ref w32Mouse);

                mouse_event(MOUSEEVENTF_LEFTDOWN, w32Mouse.X, w32Mouse.Y, 0, 0);
                System.Threading.Thread.Sleep(100);
                mouse_event(MOUSEEVENTF_LEFTUP, w32Mouse.X, w32Mouse.Y, 0, 0);
            }
            catch (Exception){ }
        }

        public static void RightMouseClick()
        {
            try
            {
                var w32Mouse = new Win32Point();
                GetCursorPos(ref w32Mouse);

                mouse_event(MOUSEEVENTF_RIGHTDOWN, w32Mouse.X, w32Mouse.Y, 0, 0);
                System.Threading.Thread.Sleep(100);
                mouse_event(MOUSEEVENTF_RIGHTUP, w32Mouse.X, w32Mouse.Y, 0, 0);
            }
            catch (Exception) { }
        }
    }
}
