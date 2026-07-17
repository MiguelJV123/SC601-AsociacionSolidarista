using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto.asociacionsolidarista.Models.Entities
{
    public class Comercio
    {
        public int IdComercio { get; set; }

        public string Identificacion { get; set; }

        public int TipoIdentificacion { get; set; }

        public string Nombre { get; set; }

        public string Telefono { get; set; }

        public string CorreoElectronico { get; set; }

        public string Direccion { get; set; }

        public DateTime FechaDeRegistro { get; set; }

        public DateTime? FechaDeModificacion { get; set; }

        public int Estado { get; set; }
    }
}