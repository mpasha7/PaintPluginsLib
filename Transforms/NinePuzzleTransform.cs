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
    public class NinePuzzleTransform : IPlugin
    {
        public string Name { get => "Перемешать 9 кусков"; }
        public string Author { get => "Pavel Melyukhin"; }

        public Bitmap Transform(Bitmap bitmap)
        {
            List<Bitmap> puzzles = new List<Bitmap>();
            int puzzleWidth = bitmap.Width / 3;
            int puzzleHeight = bitmap.Height / 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    puzzles.Add(GetPuzzle(bitmap, i * puzzleWidth, j * puzzleHeight, puzzleWidth, puzzleHeight));
                }
            }
            //Bitmap newBitmap = new Bitmap(3 * puzzles[0].Width, 3 * puzzles[0].Height);
            Random rnd = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int puzzleNumber = rnd.Next(puzzles.Count);
                    SetPuzzle(puzzles[puzzleNumber], bitmap, i, j);
                    puzzles.RemoveAt(puzzleNumber);
                }
            }
            return bitmap;
        }

        public Bitmap GetPuzzle(Bitmap bitmap, int X, int Y, int width, int height)
        {
            Bitmap newPuzzle = new Bitmap(width, height);
            for (int i = 0; i < newPuzzle.Width; i++)
            {
                for (int j = 0; j < newPuzzle.Height; j++)
                {
                    newPuzzle.SetPixel(i, j, bitmap.GetPixel(X + i, Y + j));
                }
            }
            return newPuzzle;
        }

        public void SetPuzzle(Bitmap puzzle, Bitmap bitmap, int column, int row)
        {
            for (int i = 0; i < puzzle.Width; i++)
            {
                for (int j = 0; j < puzzle.Height; j++)
                {
                    bitmap.SetPixel(column * puzzle.Width + i, row * puzzle.Height + j, puzzle.GetPixel(i, j));
                }
            }
        }
    }
}
