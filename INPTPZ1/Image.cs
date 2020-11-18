using System.Drawing;

namespace INPTPZ1
{
    namespace Mathematics
    {
        public class Image
        {
            private Bitmap image;
            private const string DEFAULT_IMAGE_PATH = "../../../out.png";

            private static readonly Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
                Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            public int Width { get; }
            public int Height { get; }


            public Image(int width, int height)
            {
                Width = width;
                Height = height;

                image = new Bitmap(width, height);
            }

            public void SetPixelsColors(int x, int y, int rootIdentifier)
            {
                Color pixelColor = colors[rootIdentifier % colors.Length];
                image.SetPixel(x, y, pixelColor);
            }

            public void SaveImage(string path)
            {
                image.Save(path ?? DEFAULT_IMAGE_PATH);
            }
        }
    }
}
