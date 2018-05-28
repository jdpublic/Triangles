using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ImageGridController : ApiController
    {

        // GET: api/ImageGrid/GetTriangle?row=a&col=1
        // GET: api/ImageGrid/?row=a&col=1
        [HttpGet]
        public Triangle GetTriangle(string row, string col)
        {
            var imageGrid = new ImageGrid();

            var t = imageGrid.GetTriangleByRowAndColumn(row, col);

            return t;
        }

        // POST: api/ImageGrid
        public void Post([FromBody]string value)
        {
        }

    }
}
