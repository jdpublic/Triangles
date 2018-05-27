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
            int rowIndex, colIndex;

            ValidateAndGetRowAndColumnIndexes(rowName, columnName, out rowIndex, out colIndex);
            
            var t = new Triangle() { RowName = rowName, ColumnName = columnName };

            int xLeft, xRight;
            CalcXCoordinates(colIndex, out xLeft, out xRight);

            int yTop, yBottom;
            CalcYCoordinates(rowIndex, out yTop, out yBottom);

            t.Vertices.Add(new Vertex { X = xLeft, Y = yTop });
            t.Vertices.Add(new Vertex { X = xRight, Y = yBottom });
            t.Vertices.Add(new Vertex { X = xLeft, Y = yBottom });

            return t;
        }

        private static void CalcYCoordinates(int rowIndex, out int yTop, out int yBottom)
        {
            int rowNumber = rowIndex + 1;

            int yTopOffset = (10 * rowIndex);
            int yBottomOffset = (10 * rowNumber);

            yTop = 0 - yTopOffset;
            yBottom = 0 - yBottomOffset;
        }

        private static void CalcXCoordinates(int colIndex, out int xLeft, out int xRight)
        {
            int colNumber = colIndex + 1;
            bool isEvenColNumber = (colNumber % 2 == 0);

            int xOffsetRight = ((isEvenColNumber) ? colNumber : colNumber + 1) / 2;
            int xOffsetLeft = xOffsetRight - 1;

            xLeft = (10 * xOffsetLeft);
            xRight = (10 * xOffsetRight);
        }

        private static void ValidateAndGetRowAndColumnIndexes(string rowName, string columnName, out int rowIndex, out int colIndex)
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

            colIndex = colNumber - 1;

            if (rowName != "A" && rowName != "B")
            {
                throw new NotImplementedException();
            }

            rowIndex = (int)rowName[0] - (int)'A'; //zero based
        }
    }
}