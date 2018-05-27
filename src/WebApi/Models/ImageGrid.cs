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
            int colNumber;
            if (!int.TryParse(columnName, out colNumber))
            {
                throw new ArgumentException("numeric string value expected", nameof(columnName));
            }

            if (colNumber < 0 || colNumber > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(columnName), "expected numeric string between 1 and 12");
            }

            

            if (rowName != "A" && rowName != "B" )
            {
                throw new NotImplementedException();
            }

            var t = new Triangle() { RowName = rowName, ColumnName = columnName };

            bool isEvenColNumber = (colNumber % 2 == 0);

            int xOffsetRight = ((isEvenColNumber) ? colNumber : colNumber+1) / 2;
            int xOffsetLeft = xOffsetRight - 1;

            int xLeft = (10 * xOffsetLeft);
            int xRight = (10 * xOffsetRight);

            int rowIndex = (int)rowName[0] - (int)'A'; //zero based
            int rowNumber = rowIndex + 1;

            int yTopOffset = (10 * rowIndex);
            int yBottomOffset = (10 * rowNumber);

            int yTop = 0 - yTopOffset;
            int yBottom = 0 - yBottomOffset;

            t.Vertices.Add(new Vertex { X = xLeft, Y = yTop });
            t.Vertices.Add(new Vertex { X = xRight, Y = yBottom });
            t.Vertices.Add(new Vertex { X = xLeft, Y = yBottom });

            return t;
        }
    }
}