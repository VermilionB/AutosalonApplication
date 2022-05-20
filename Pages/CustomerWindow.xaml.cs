using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Resources;
using Autosalon.Infrastructure;
using Autosalon.ViewModels;

namespace Autosalon.Pages;

public partial class CustomerWindow : Window
{
    CustomerViewModel? model;

    public CustomerWindow()
    {
        InitializeComponent();
        Navigator navigator = new Navigator();
        navigator.CurrentViewModel = new LoginViewModel();
        DataContext = new CustomerViewModel(navigator);

        SourceInitialized += (s, e) =>
        {
            var handle = new WindowInteropHelper(this).Handle;
            HwndSource.FromHwnd(handle)?.AddHook(WindowProc);
        };
       
    }

    private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam,
        IntPtr lParam, ref bool handled)
    {
        switch (msg)
        {
            case 0x0024:
                WmGetMinMaxInfo(hwnd, lParam);
                handled = true;
                break;
        }

        return (IntPtr) 0;
    }

    private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
    {
        var mmi =
            (MinMaxInfo) Marshal.PtrToStructure(lParam, typeof(MinMaxInfo));
        const int monitorDefaultToNearest = 0x00000002;
        var monitor = MonitorFromWindow(hwnd, monitorDefaultToNearest);
        if (monitor != IntPtr.Zero)
        {
            var monitorInfo = new MonitorInfo();
            GetMonitorInfo(monitor, monitorInfo);
            var rcWorkArea = monitorInfo.rcWork;
            var rcMonitorArea = monitorInfo.rcMonitor;
            mmi.ptMaxPosition.x =
                Math.Abs(rcWorkArea.left - rcMonitorArea.left);
            mmi.ptMaxPosition.y =
                Math.Abs(rcWorkArea.top - rcMonitorArea.top);
            mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
            mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            if (Application.Current.MainWindow != null)
            {
                mmi.ptMinSize.x =
                    (int) Application.Current.MainWindow.MinWidth;
                mmi.ptMinSize.y =
                    (int) Application.Current.MainWindow.MinHeight;
            }
        }

        Marshal.StructureToPtr(mmi, lParam, true);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        /// <summary>
        /// x coordinate of point.
        /// </summary>
        public int x;

        /// <summary>
        /// y coordinate of point.
        /// </summary>
        public int y;

        /// <summary>
        /// Construct a point of coordinates (x,y).
        /// </summary>
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MinMaxInfo
    {
        private readonly Point ptReserved;
        public Point ptMaxSize;
        public Point ptMaxPosition;
        public Point ptMinSize;
        public Point ptMinPosition;
        private readonly Point ptMinTrackSize;
        private readonly Point ptMaxTrackSize;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class MonitorInfo
    {
        public int cbSize = Marshal.SizeOf(typeof(MonitorInfo));
        public Rect rcMonitor = new Rect();
        public Rect rcWork = new Rect();
        public int dwFlags = 0;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public readonly struct Rect
    {
        public readonly int left;
        public readonly int top;
        public readonly int right;
        public readonly int bottom;
        private static readonly Rect Empty;

        public int Width => Math.Abs(right - left);

        public int Height => bottom - top;

        public Rect(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public Rect(Rect rcSrc)
        {
            left = rcSrc.left;
            top = rcSrc.top;
            right = rcSrc.right;
            bottom = rcSrc.bottom;
        }

        public bool IsEmpty => left >= right || top >= bottom;

        public override string ToString()
        {
            if (this == Empty)
            {
                return "Rect {Empty}";
            }

            return "Rect { left : " + left + " / top : " + top +
                   " / right : " + right + " / bottom : " + bottom + " }";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is System.Windows.Rect))
            {
                return false;
            }

            // ReSharper disable once PossibleInvalidCastException
            return this == (Rect) obj;
        }

        /// <summary>
        /// Return the HashCode for this struct (not guaranteed to be
        /// unique)
        /// </summary>
        public override int GetHashCode() => left.GetHashCode() +
                                             top.GetHashCode() +
                                             right.GetHashCode() +
                                             bottom.GetHashCode();

        /// <summary>
        /// Determine if 2 Rect are equal (deep compare)
        /// </summary>
        public static bool operator ==(Rect rect1, Rect rect2)
        {
            return rect1.left == rect2.left && rect1.top == rect2.top &&
                   rect1.right == rect2.right &&
                   rect1.bottom == rect2.bottom;
        }

        /// <summary>
        /// Determine if 2 Rect are different(deep compare)
        /// </summary>
        public static bool operator !=(Rect rect1, Rect rect2)
        {
            return !(rect1 == rect2);
        }
    }

    [DllImport("user32")]
    private static extern bool GetMonitorInfo(IntPtr hMonitor,
        MonitorInfo lpmi);

    [DllImport("User32")]
    private static extern IntPtr MonitorFromWindow(IntPtr handle,
        int flags);


    private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void MaximizeButton_OnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Maximized
            ? WindowState.Normal
            : WindowState.Maximized;
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    public void ToHomePage_Click(object sender, RoutedEventArgs e)
    {
        if (model != null) ;
    }
}