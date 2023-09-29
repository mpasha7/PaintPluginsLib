using System;
using PluginInterface;
using System.Drawing;


namespace Transforms
{
    [Version(1, 0)]
    public class ReverseTransform : IPlugin
    {
        public string Name { get => "Переворот на 180\u00B0"; }
        public string Author { get => "Pavel Melyukhin"; }

        public Bitmap Transform(Bitmap bitmap)
        {
            for (int i = 0; i < bitmap.Width; i++)
                for (int j = 0; j < bitmap.Height / 2; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    bitmap.SetPixel(i, j, bitmap.GetPixel(i, bitmap.Height - j - 1));
                    bitmap.SetPixel(i, bitmap.Height - j - 1, color);
                }
            return bitmap;
        }
    }
}
