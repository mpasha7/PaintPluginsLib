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
    public class GrayscaleTransform : IPlugin
    {
        public string Name { get => "Оттенки серого"; }
        public string Author { get => "Pavel Melyukhin"; }

        public Bitmap Transform(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            Bitmap newBitmap = new Bitmap(width, height);
            int grayShade;
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    grayShade = (int)(0.299*bitmap.GetPixel(i, j).R + 0.587*bitmap.GetPixel(i, j).G + 0.114*bitmap.GetPixel(i, j).B);
                    newBitmap.SetPixel(i, j, Color.FromArgb(grayShade, grayShade, grayShade));
                }
            return newBitmap;
        }
    }
}
