using PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Transforms
{
    [Version(1, 0)]
    public class SignAddTransform : IPlugin
    {
        public string Name { get => "Вставить подпись"; }
        public string Author { get => "Pavel Melyukhin"; }

        public Bitmap Transform(Bitmap bitmap)
        {
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawString($"{Author}\n{DateTime.Now.ToShortDateString()}", new Font(FontFamily.GenericSansSerif, bitmap.Height * 0.05F), 
                new SolidBrush(Color.Red), bitmap.Width * 0.65F, bitmap.Height * 0.85F);
            graphics.Dispose();
            return bitmap;
        }
    }
}
