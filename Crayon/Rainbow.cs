using System;

namespace Crayon
{
    public class Rainbow
    {
        private readonly double _freq;
        private int _idx;

        public Rainbow(double freq)
        {
            _freq = freq;
        }

        public IOutput Next() => ToRainbow(_freq, _idx++);

        private static IOutput ToRainbow(double freq, int idx)
        {
            var r = Convert.ToByte(Math.Round(Math.Sin(freq * idx) * 127 + 128));
            var g = Convert.ToByte(Math.Round(Math.Sin(freq * idx + 2) * 127 + 128));
            var b = Convert.ToByte(Math.Round(Math.Sin(freq * idx + 4) * 127 + 128));
            
            return Output.Rgb(r, g, b);
        }
    }
}