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
        [DataRow("A11", 50, 0, 60, -10, 50, -10)]
        [DataRow("A12", 50, 0, 60, -10, 60, 0)]
        [DataRow("F1", 0, -50, 10, -60, 0, -60)]
        [DataRow("F2", 0, -50, 10, -60, 10, -50)]
        [DataRow("F11", 50, -50, 60, -60, 50, -60)]
        [DataRow("F12", 50, -50, 60, -60, 60, -50)]
        public void GetRowAndColumn_GridBoundaries(string expectedRowAndColumn, int x1, int y1, int x2, int y2, int x3, int y3)
        {

            var expectedVertices = new List<Vertex>(3) { new Vertex { X = x1, Y = y1 }
                                                        , new Vertex {X = x2, Y = y2 }
                                                        , new Vertex { X = x3, Y = y3 } };

            

            var rowAndColumn = TestImageGrid.GetRowAndColumn(expectedVertices);

            rowAndColumn.Should().Be(expectedRowAndColumn, "the row letter and column number should be as expected");
        }



        [TestMethod]
        public void GetRowAndColumn_Vertices_Null()
        {

            List<Vertex> vertices = null;

            Action act = () =>  TestImageGrid.GetRowAndColumn(vertices);

            act.Should().Throw<ArgumentNullException>("no vertices list is supplied");
        }

        [TestMethod]
        public void GetRowAndColumn_Vertices_TooFew()
        {

            List<Vertex> vertices = new List<Vertex>();
           
            //check count is < 3 before act
            vertices.Count.Should().BeLessThan(3, "test action required input of less than three vertices to be valid ");
            
            //act
            Action act = () => TestImageGrid.GetRowAndColumn(vertices);

            act.Should().Throw<ArgumentOutOfRangeException>($"expected list of 3 vertices got {vertices.Count}");
        }

        [TestMethod]
        public void GetRowAndColumn_Vertices_TooMany()
        {

            List<Vertex> vertices = new List<Vertex>() { new Vertex(), new Vertex(), new Vertex(), new Vertex() };

            //check count is > 3 before act
            vertices.Count.Should().BeGreaterThan(3, "test action required input of more than three vertices to be valid ");

            //act
            Action act = () => TestImageGrid.GetRowAndColumn(vertices);

            act.Should().Throw<ArgumentOutOfRangeException>($"expected list of 3 vertices got {vertices.Count}");
        }

        [TestMethod]
        [DataRow(-1)] //outwith grid lower
        [DataRow(15)] //mid cell
        [DataRow(65)] //outwith grid upper
        public void GetRowAndColumn_Vertices_InvalidVertexX(int invalidX)
        {
            List<Vertex> vertices = new List<Vertex>() { new Vertex() { X = invalidX }, new Vertex(), new Vertex() };


            Action act = () => TestImageGrid.GetRowAndColumn(vertices);

            act.Should().Throw<ArgumentOutOfRangeException>($"invalid vertex X coordinate ({invalidX})");
        }

        [TestMethod]
        [DataRow(-1)] //outwith grid lower
        [DataRow(15)] //mid cell
        [DataRow(65)] //outwith grid upper
        public void GetRowAndColumn_Vertices_InvalidVertexY(int invalidY)
        {
            List<Vertex> vertices = new List<Vertex>() { new Vertex(), new Vertex() { Y= invalidY },  new Vertex() };


            Action act = () => TestImageGrid.GetRowAndColumn(vertices);

            act.Should().Throw<ArgumentOutOfRangeException>($"invalid vertex Y coordinate ({invalidY})");
        }
    }
}
