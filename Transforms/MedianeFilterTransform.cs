using PluginInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transforms
{
    [Version(1, 0)]
    public class MedianeFilterTransform : IPlugin
    {
        public string Name { get => "Медианный фильтр"; }
        public string Author { get => "Pavel Melyukhin"; }

        public Bitmap Transform(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            Bitmap newBitmap = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    newBitmap.SetPixel(i, j, GetMedianePixel(bitmap, i, j));
                }
            return newBitmap;
        }

        private Color GetMedianePixel(Bitmap bitmap, int i, int j)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            List<Color> pixelList = new List<Color>();
            for (int m = 0; m < 3; m++)
                for (int n = 0; n < 3; n++)
                {
                    if (i - 1 + m >= 0 && i - 1 + m < width && j - 1 + n >= 0 && j - 1 + n < height)
                        pixelList.Add(bitmap.GetPixel(i - 1 + m, j - 1 + n));
                }
            return Color.FromArgb(
                GetMediane(pixelList, c => c.R),
                GetMediane(pixelList, c => c.G),
                GetMediane(pixelList, c => c.B));
        }

        private int GetMediane(List<Color> pixelList, Func<Color, int> GetSpectrum)
        {
            int mediane;
            pixelList.Sort((a, b) => GetSpectrum(a) - GetSpectrum(b));
            if (pixelList.Count % 2 == 0)
                mediane = (GetSpectrum(pixelList[pixelList.Count / 2 - 1]) + GetSpectrum(pixelList[pixelList.Count / 2])) / 2;
            else
                mediane = GetSpectrum(pixelList[(pixelList.Count - 1) / 2]);
            return mediane;
        }
    }
}
