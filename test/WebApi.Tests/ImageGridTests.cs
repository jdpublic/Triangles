using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models;

namespace WebApi.Tests
{
    [TestClass]
    public class ImageGridTests
    {
        [TestMethod]
        public void Create()
        {
            var imageGrid = new ImageGrid(60, 60);

            var expectedWidthAndHeight = 60;

            imageGrid.Width.Should().Be(expectedWidthAndHeight);
            imageGrid.Height.Should().Be(expectedWidthAndHeight);

        }

        [TestMethod]
        public void GetTriangleByRowAndColumn_A1()
        {
            var imageGrid = new ImageGrid(60, 60);

            var triangle = imageGrid.GetTriangleByRowAndColumnNames("A", "1");


            var expectedName = "A1";
            var expectedNumberOfCoordinates = 3;
            
            triangle.Name.Should().Be(expectedName);
            triangle.Vertices.Should().NotBeEmpty()
                .And.HaveCount(expectedNumberOfCoordinates);

        }

        [TestMethod]
        public void GetTriangleByRowAndColumn_A3()
        {
            var imageGrid = new ImageGrid(60, 60);

            var triangle = imageGrid.GetTriangleByRowAndColumnNames("A", "3");


            var expectedName = "A3";
            var expectedNumberOfCoordinates = 3;

            triangle.Name.Should().Be(expectedName);
            triangle.Vertices.Should().NotBeEmpty()
                .And.HaveCount(expectedNumberOfCoordinates);

            triangle.Vertices[0].Should().BeEquivalentTo(new Vertex { X = 10, Y = 0 });
            triangle.Vertices[1].Should().BeEquivalentTo(new Vertex { X = 20, Y = -10 });
            triangle.Vertices[2].Should().BeEquivalentTo(new Vertex { X = 10, Y = -10 });

        }
    }
}
