using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Tests
{
    
    /// <summary>
    /// ImageGrid Method Tests relating to GetRowAndColumn Method
    /// </summary>
    public partial class ImageGridTests
    {

        [TestMethod]
        [DataRow("A1", 0, 0, 10, -10, 0, -10)]
        [DataRow("A2", 0, 0, 10, -10, 10, 0)]
        //[DataRow("A11", 50, 0, 60, -10, 50, -10)]
        //[DataRow("A12", 50, 0, 60, -10, 60, 0)]
        //[DataRow("F1", 0, -50, 10, -60, 0, -60)]
        //[DataRow("F2", 0, -50, 10, -60, 10, -50)]
        //[DataRow("F11", 50, -50, 60, -60, 50, -60)]
        //[DataRow("F12", 50, -50, 60, -60, 60, -50)]
        public void GetRowAndColumn(string expectedRowAndColumn, int x1, int y1, int x2, int y2, int x3, int y3)
        {

            var expectedVertices = new List<Vertex>(3) { new Vertex { X = x1, Y = y1 }
                                                        , new Vertex {X = x2, Y = y2 }
                                                        , new Vertex { X = x3, Y = y3 } };

            

            var rowAndColumn = TestImageGrid.GetRowAndColumn(expectedVertices);

            rowAndColumn.Should().Be(expectedRowAndColumn, "the row letter and column number should be as expected");
        }

    }
}
