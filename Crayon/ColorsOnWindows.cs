using System;
using System.Runtime.InteropServices;

namespace Crayon
{
    /// https://www.jerriepelser.com/blog/using-ansi-color-codes-in-net-console-apps/
    internal static class ColorsOnWindows
    {
        public static bool Enable()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return true;
            }

            var iStdOut = GetStdHandle(StdOutputHandle);
            return GetConsoleMode(iStdOut, out var outConsoleMode) &&
                SetConsoleMode(iStdOut, outConsoleMode | EnableVirtualTerminalProcessing);
        }

        private const int StdOutputHandle = -11;
        private const uint EnableVirtualTerminalProcessing = 0x0004;

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();
    }
}