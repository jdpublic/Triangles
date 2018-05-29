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
            Triangle triangle = null;

            try
            {
                var imageGrid = new ImageGrid();
                triangle = imageGrid.GetTriangleByRowAndColumn(row, col);
            }
            catch (ArgumentException argEx) //also covers ArgumentOutOfRangeException as descendant
            {
                HandleArgumentErrors(row, col, argEx);
            }

            return triangle;
        }

        /// <summary>
        /// simple error handler for now, could refactor to use ExceptionAttribute filter etc
        /// </summary>
        private static void HandleArgumentErrors(string row, string col, ArgumentException argEx)
        {
            //return status 400 - BadRequest
            var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(argEx.Message),
                ReasonPhrase = $"{argEx.GetType().Name} (row '{row}', col '{col}')",
            };
            throw new HttpResponseException(resp);
        }

        // POST: api/ImageGrid
        public void Post([FromBody]string value)
        {
        }

    }
}
