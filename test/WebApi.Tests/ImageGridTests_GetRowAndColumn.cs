using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Tests
{
    /// <summary>
    /// ImageGrid Method Tests - mainly relating to GetTriangleByRowAndColumn Method
    /// </summary>
    [TestClass]
    public partial class ImageGridTests
    {
        ImageGrid TestImageGrid;

        [TestInitialize]
        public void TestInit()
        {
            TestImageGrid = new ImageGrid();
        }

        [TestMethod]
        public void CreateDefault()
        {
            var expectedWidthAndHeight = 60;
            var expectedCellSize = 10;
            TestImageGrid.Width.Should().Be(expectedWidthAndHeight);
            TestImageGrid.Height.Should().Be(expectedWidthAndHeight);
            TestImageGrid.CellSize.Should().Be(expectedCellSize);
        }

        [TestMethod]
        public void GetTriangleByRowAndColumn_A1()
        {

            var triangle = TestImageGrid.GetTriangleByRowAndColumn("A", "1");

            var expectedName = "A1";
            var expectedNumberOfCoordinates = 3;

            triangle.Name.Should().Be(expectedName);
            triangle.Vertices.Should().NotBeEmpty()
                .And.HaveCount(expectedNumberOfCoordinates);

        }

        [TestMethod]
        public void GetTriangleByRowAndColumn_A3()
        {
            var triangle = TestImageGrid.GetTriangleByRowAndColumn("A", "3");

            var expectedName = "A3";
            var expectedNumberOfCoordinates = 3;

            triangle.Name.Should().Be(expectedName);
            triangle.Vertices.Should().NotBeEmpty()
                .And.HaveCount(expectedNumberOfCoordinates);

            triangle.Vertices[0].Should().BeEquivalentTo(new Vertex { X = 10, Y = 0 });
            triangle.Vertices[1].Should().BeEquivalentTo(new Vertex { X = 20, Y = -10 });
            triangle.Vertices[2].Should().BeEquivalentTo(new Vertex { X = 10, Y = -10 });
        }

        [TestMethod]
        public void GetTriangleByRowAndColumn_B3()
        {
            var triangle = TestImageGrid.GetTriangleByRowAndColumn("B", "3");

            var expectedName = "B3";
            var expectedNumberOfCoordinates = 3;

            triangle.Name.Should().Be(expectedName);
            triangle.Vertices.Should().NotBeEmpty()
                .And.HaveCount(expectedNumberOfCoordinates);

            triangle.Vertices[0].Should().BeEquivalentTo(new Vertex { X = 10, Y = -10 });
            triangle.Vertices[1].Should().BeEquivalentTo(new Vertex { X = 20, Y = -20 });
            triangle.Vertices[2].Should().BeEquivalentTo(new Vertex { X = 10, Y = -20 });

        }

        [TestMethod]
        public void GetTriangleByRowAndColumn_A2()
        {
            var triangle = TestImageGrid.GetTriangleByRowAndColumn("A", "2");

            var expectedName = "A2";

            var expectedVertices = new List<Vertex>(3) { new Vertex { X = 0, Y = 0 }
                                                        , new Vertex {X = 10, Y = -10 }
                                                        , new Vertex { X = 10, Y = 0 } };

            AssertTriangle(triangle, expectedName, expectedVertices);

        }

        [TestMethod]
        [DataRow("A1", 0, 0, 10, -10, 0, -10)]
        [DataRow("A2", 0, 0, 10, -10, 10, 0)]
        [DataRow("A11", 50, 0, 60, -10, 50, -10)]
        [DataRow("A12", 50, 0, 60, -10, 60, 0)]
        [DataRow("F1", 0, -50, 10, -60, 0, -60)]
        [DataRow("F2", 0, -50, 10, -60, 10, -50)]
        [DataRow("F11", 50, -50, 60, -60, 50, -60)]
        [DataRow("F12", 50, -50, 60, -60, 60, -50)]
        public void GetTriangleByRowAndColumn_CheckGridBoundaries(string expectedName, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            var rowName = expectedName.Substring(0, 1);
            var colName = expectedName.Substring(1);
            var triangle = TestImageGrid.GetTriangleByRowAndColumn(rowName, colName);


            var expectedVertices = new List<Vertex>(3) { new Vertex { X = x1, Y = y1 }
                                                        , new Vertex {X = x2, Y = y2 }
                                                        , new Vertex { X = x3, Y = y3 } };

            AssertTriangle(triangle, expectedName, expectedVertices);
        }


        [TestMethod]
        [DataRow("", "row name should be given")]
        [DataRow(null, "row name should not be null")]
        [DataRow("a", "row name should Uppercase")]
        public void GetTriangleByRowAndColumn_RowNames_Invalid(string rowName, string reason)
        {
            var columnName = "1";

            Action act = () => TestImageGrid.GetTriangleByRowAndColumn(rowName, columnName);

            act.Should().Throw<ArgumentException>(reason);
        }


        [TestMethod]
        [DataRow("@", "row name should be > 'A'")]
        [DataRow("G", "row name should be <= 'F'")]
        public void GetTriangleByRowAndColumn_RowNames_OutOfRange(string rowName, string reason)
        {
            var columnName = "1";

            Action act = () => TestImageGrid.GetTriangleByRowAndColumn(rowName, columnName);

            act.Should().Throw<ArgumentOutOfRangeException>(reason);
        }
        
        [TestMethod]
        [DataRow("", "column name should be given")]
        [DataRow(null, "column name should not be null")]
        [DataRow("A", "column name should be given")]
        [DataRow("AA", "column name should be a numeric")]
        public void GetTriangleByRowAndColumn_ColumnNames_Invalid(string columnName, string reason)
        {
            var rowName = "A";

            Action act = () => TestImageGrid.GetTriangleByRowAndColumn(rowName, columnName);

            act.Should().Throw<ArgumentException>(reason);
        }

        [TestMethod]
        [DataRow("0", "column name should be >= 1")]
        [DataRow("13", "column name should be <= 12")]
        public void GetTriangleByRowAndColumn_ColumnNames_OutOfRange(string columnName, string reason)
        {
            var rowName = "A";

            Action act = () => TestImageGrid.GetTriangleByRowAndColumn(rowName, columnName);

            act.Should().Throw<ArgumentOutOfRangeException>(reason);

        }

        private static void AssertTriangle(Triangle triangle, string expectedName, List<Vertex> expectedVertices)
        {
            var expectedNumberOfVertices = 3;
            triangle.Name.Should().Be(expectedName);
            triangle.Vertices.Should().NotBeEmpty()
                .And.HaveCount(expectedNumberOfVertices);

            triangle.Vertices[0].Should().BeEquivalentTo(expectedVertices[0]);
            triangle.Vertices[1].Should().BeEquivalentTo(expectedVertices[1]);
            triangle.Vertices[2].Should().BeEquivalentTo(expectedVertices[2]);
        }
    }
}
