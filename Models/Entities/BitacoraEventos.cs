using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto.asociacionsolidarista.Models.Entities
{
    public class BitacoraEventos
    {
        public int IdEvento { get; set; }

        public int? IdSinpe { get; set; }

        public string TablaDeEvento { get; set; }

        public string TipoDeEvento { get; set; }

        public DateTime FechaDeEvento { get; set; }

        public string DescripcionDeEvento { get; set; }

        public string DatosAnteriores { get; set; }

        public string DatosPosteriores { get; set; }

        // Relaciones

        public virtual PagoSINPE PagoSINPE { get; set; }
    }
}