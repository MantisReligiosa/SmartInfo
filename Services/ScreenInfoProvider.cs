using DomainObjects;
using ServiceInterfaces;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Services
{
    public class ScreenInfoProvider : IScreenInfoProvider
    {
        public ScreenInfo GetScreenInfo()
        {
            return DisplayTools.GetVirtualDisplaySize();
        }
    }

    internal static class DisplayTools
    {
        public enum SystemMetric
        {
            VirtualScreenWidth = 78, // CXVIRTUALSCREEN 0x0000004E 
            VirtualScreenHeight = 79, // CYVIRTUALSCREEN 0x0000004F 
        }

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(SystemMetric metric);

        public static ScreenInfo GetVirtualDisplaySize()
        {
            var width = GetSystemMetrics(SystemMetric.VirtualScreenWidth);
            var height = GetSystemMetrics(SystemMetric.VirtualScreenHeight);

            return new ScreenInfo
            {
                Width = width,
                Height = height
            };
        }
    }
}
