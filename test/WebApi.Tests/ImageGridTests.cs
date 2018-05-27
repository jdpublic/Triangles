using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi;

namespace WebApi.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var imageGrid = new ImageGrid(60, 60);

            var expectedWidthAndHeight = 60;

            imageGrid.Width.Should().Be(expectedWidthAndHeight);
            imageGrid.Height.Should().Be(expectedWidthAndHeight);

        }
    }
}
