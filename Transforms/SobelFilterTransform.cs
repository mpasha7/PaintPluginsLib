using PluginInterface;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transforms
{
    [Version(1, 0)]
    internal class SobelFilterTransform : IPlugin
    {
        public string Name { get => "Фильтр Собеля"; }
        public string Author { get => "Pavel Melyukhin"; }

        public Bitmap Transform(Bitmap bitmap)
        {
            double[,] sx = new double[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
            var width = bitmap.Width;
            var height = bitmap.Height;
            Bitmap newBitmap = new Bitmap(width, height);
            for (int x = 1; x < width - 1; x++)
                for (int y = 1; y < height - 1; y++)
                {
                    newBitmap.SetPixel(x, y, Color.FromArgb(
                        FiltredPixel(bitmap, x, y, c => c.R),
                        FiltredPixel(bitmap, x, y, c => c.G),
                        FiltredPixel(bitmap, x, y, c => c.B)));
                }
            return newBitmap;
        }

        private int FiltredPixel(Bitmap bmp, int x, int y, Func<Color, double> GetSpectrum)
        {
            var gx = 
                - GetSpectrum(bmp.GetPixel(x - 1, y - 1)) - 2 * GetSpectrum(bmp.GetPixel(x, y - 1)) - GetSpectrum(bmp.GetPixel(x + 1, y - 1)) 
                + GetSpectrum(bmp.GetPixel(x - 1, y + 1)) + 2 * GetSpectrum(bmp.GetPixel(x, y + 1)) + GetSpectrum(bmp.GetPixel(x + 1, y + 1));
            var gy =
                -GetSpectrum(bmp.GetPixel(x - 1, y - 1)) - 2 * GetSpectrum(bmp.GetPixel(x - 1, y)) - GetSpectrum(bmp.GetPixel(x - 1, y + 1))
                + GetSpectrum(bmp.GetPixel(x + 1, y - 1)) + 2 * GetSpectrum(bmp.GetPixel(x + 1, y)) + GetSpectrum(bmp.GetPixel(x + 1, y + 1));

            int result = (int)Math.Sqrt(gx * gx + gy * gy);
            result = Math.Min(255, result);
            result = Math.Max(0, result);
            return result;
        }

    }
}
