using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace XMouse
{
    public class MouseHandler
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [DllImport("user32.Dll")]
        internal static extern long SetCursorPos(int x, int y);

        [DllImport("user32.Dll")]
        internal static extern bool ClientToScreen(IntPtr hWnd, ref Win32Point point);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public int X;
            public int Y;
        };

        public static Point GetMousePosition()
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        public static void SetMousePosition(int x, int y)
        {
            var p = new Win32Point
            {
                X = Convert.ToInt16(x),
                Y = Convert.ToInt16(y)
            };
            SetCursorPos(p.X, p.Y);
        }
    }
}
