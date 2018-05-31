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
                HandleArgumentErrors($"row {row}, col {col}", argEx);
            }

            return triangle;
        }

        // GET: api/ImageGrid/GetRowAndColumn?v1X=0&v1Y=0
        [HttpGet]
        public string GetRowAndColumn(int v1X, int v1Y, int v2X, int v2Y, int v3X, int v3Y)
        {
            string rowAndColumn = string.Empty;

            try
            {
                var v1 = new Vertex() { X = v1X, Y = v1Y };
                var v2 = new Vertex() { X = v2X, Y = v2Y };
                var v3 = new Vertex() { X = v3X, Y = v3Y };

                var vertices = new List<Vertex>(3) { v1, v2, v3 };
                var imageGrid = new ImageGrid();
                rowAndColumn = imageGrid.GetRowAndColumn(vertices);
            }
            catch (ArgumentException argEx) //also covers ArgumentOutOfRangeException as descendant
            {
                HandleArgumentErrors("vertices", argEx);
            }

            return rowAndColumn;
        }

        /// <summary>
        /// simple error handler for now, could refactor to use ExceptionAttribute filter etc
        /// </summary>
        private static void HandleArgumentErrors(string extraInfo, ArgumentException argEx)
        {
            //return status 400 - BadRequest

            var msg = argEx?.Message?.Split("\r\n".ToCharArray()).FirstOrDefault();

            var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(argEx.Message),
                ReasonPhrase = $"{argEx.GetType().Name} ({extraInfo} - {msg})",
            };
            throw new HttpResponseException(resp);
        }

    }
}
