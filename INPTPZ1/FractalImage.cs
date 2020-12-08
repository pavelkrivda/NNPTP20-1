using System.Drawing;

namespace INPTPZ1
{
    namespace Mathematics
    {
        public class FractalImage
        {
            private Bitmap fractalImage;
            private const string DEFAULT_IMAGE_PATH = "../../../out.png";

            private static readonly Color[] colors =
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
                Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            public int Width { get; }
            public int Height { get; }


            public FractalImage(int width, int height)
            {
                Width = width;
                Height = height;

                fractalImage = new Bitmap(width, height);
            }

            public void SetPixelsColors(int x, int y, int rootIdentifier)
            {
                Color pixelColor = colors[rootIdentifier % colors.Length];
                fractalImage.SetPixel(x, y, pixelColor);
            }

            public void SaveImage(string path)
            {
                fractalImage.Save(path ?? DEFAULT_IMAGE_PATH);
            }
        }
    }
}
