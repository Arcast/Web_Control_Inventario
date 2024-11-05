using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WA_ControlInventario.Models
{
    public class BodegaDTO
    {
        public Guid BodegaId { get; set; } = Guid.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;
    }
}