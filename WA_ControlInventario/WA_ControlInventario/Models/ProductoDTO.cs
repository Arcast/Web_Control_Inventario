using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WA_ControlInventario.Models
{
    public class ProductoDTO
    {
        public Guid ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string CreadoPor { get; set; }
    }
}