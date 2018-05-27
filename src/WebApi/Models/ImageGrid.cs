using System;

namespace WebApi
{
    public class ImageGrid
    {
        public ImageGrid(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public object Width { get; internal set; }
        public object Height { get; internal set; }
    }
}