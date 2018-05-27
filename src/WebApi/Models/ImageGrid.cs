using System;

namespace WebApi.Models
{
    public class ImageGrid
    {
        public ImageGrid(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public int Width { get; internal set; }
        public int Height { get; internal set; }
        public Triangle GetTriangleByRowAndColumnNames(string rowName, string columnName)
        {
            if (rowName != "A" || columnName != "1")
            {
                throw new NotImplementedException();
            }

            var t =  new Triangle() { RowName = rowName, ColumnName = columnName };

            t.Vertices.Add(new Vertex { X = 0, Y = 0 });
            t.Vertices.Add(new Vertex { X = 10, Y = -10 });
            t.Vertices.Add(new Vertex { X = 0, Y = -10 });

            return t;
        }
    }
}