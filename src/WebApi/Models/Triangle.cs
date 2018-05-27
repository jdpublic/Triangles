using System.Collections.Generic;

namespace WebApi.Models
{
    public class Triangle
    {
        public Triangle()
        {
            Vertices = new List<Vertex>(3);
        }

        public string RowName { get; set; }
        public string ColumnName { get; set; }

        public string Name => RowName + ColumnName;

        public List<Vertex> Vertices { get; set; }
    }
}
