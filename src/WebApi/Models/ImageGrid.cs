using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Models
{
    public class ImageGrid
    {

        public ImageGrid() : this(60,60,10)
        {
            //default 
        }
        public ImageGrid(int width, int height, int cellSize)
        {
            this.Width = width;
            this.Height = height;
            this.CellSize = cellSize;
        }

        public readonly int Width;
        public readonly int Height;
        public readonly int CellSize;

       /// <summary>
       /// A Triangle object with vertex coordinates for the corners relative to this ImageGrid's
       /// top left corner
       /// </summary>
       /// <param name="rowName">The Row reference as a string</param>
       /// <param name="columnName">The Column reference as a string</param>
       /// <returns>Triangle Object</returns>
        public Triangle GetTriangleByRowAndColumn(string rowName, string columnName)
        {
            int rowIndex, colIndex;

            ValidateAndGetRowAndColumnIndexes(rowName, columnName, out rowIndex, out colIndex);
            
            var t = new Triangle() { RowName = rowName, ColumnName = columnName };

            int xLeft, xRight;
            CalcCellXCoordinates(colIndex, CellSize, out xLeft, out xRight);

            int yTop, yBottom;
            CalcCellYCoordinates(rowIndex, CellSize, out yTop, out yBottom);

            //add vertices 

            //Common - top left and bottom right
            t.Vertices.Add(new Vertex { X = xLeft, Y = yTop });
            t.Vertices.Add(new Vertex { X = xRight, Y = yBottom });

            bool colNumberIsEven = (colIndex+1) % 2 == 0;
            if (colNumberIsEven)
            {
                //top right
                t.Vertices.Add(new Vertex { X = xRight, Y = yTop });
            }
            else
            {
                //bottom left
                t.Vertices.Add(new Vertex { X = xLeft, Y = yBottom });
            }
            return t;
        }

        /// <summary>
        /// This method returns the Image Row Letter and Column Number as a single string
        /// calculated from the supplied triangle vertex coordinates
        /// </summary>
        /// <param name="vertices">The vertices of a specific triangle</param>
        /// <returns></returns>
        public string GetRowAndColumn(List<Vertex> vertices)
        {
            var v1 = vertices[0];
            var v2 = vertices[1];
            var v3 = vertices[2];

            var xValues = from v in vertices select v.X;
            var xMax = xValues.Max();

            var col = xMax / CellSize;

            return "A" + col;

            //throw new NotImplementedException();
        }

        private static void CalcCellYCoordinates(int rowIndex, int cellSize, out int yTop, out int yBottom)
        {
            int rowNumber = rowIndex + 1;

            int yTopOffset = (cellSize * rowIndex);
            int yBottomOffset = (cellSize * rowNumber);

            yTop = 0 - yTopOffset;
            yBottom = 0 - yBottomOffset;
        }

        private static void CalcCellXCoordinates(int colIndex, int cellSize, out int xLeft, out int xRight)
        {
            int colNumber = colIndex + 1;
            bool isEvenColNumber = (colNumber % 2 == 0);

            int xOffsetRight = ((isEvenColNumber) ? colNumber : colNumber + 1) / 2;
            int xOffsetLeft = xOffsetRight - 1;

            xLeft = (cellSize * xOffsetLeft);
            xRight = (cellSize * xOffsetRight);
        }

        private static void ValidateAndGetRowAndColumnIndexes(string rowName, string columnName, out int rowIndex, out int colIndex)
        {
            int colNumber;
            if (!int.TryParse(columnName, out colNumber))
            {
                throw new ArgumentException("numeric string value expected", nameof(columnName));
            }

            colIndex = colNumber - 1;

            if (colIndex < 0 || colIndex > 11)
            {
                throw new ArgumentOutOfRangeException(nameof(columnName), "expected numeric string between 1 and 12");
            }

            
            if (string.IsNullOrEmpty(rowName))
            {
                throw new ArgumentException("it should not be null or empty", nameof(rowName));
            }

            rowIndex = (int)rowName[0] - (int)'A'; //zero based

            if (rowIndex < 0 || rowIndex > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(rowName), "expected letter A through F");
            }
        }
    }
}