using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto.asociacionsolidarista.Models.Entities
{
    public class PagoSINPE
    {
        public int IdSinpe { get; set; }

        public int IdCaja { get; set; }

        public string TelefonoOrigen { get; set; }

        public string NombreOrigen { get; set; }

        public string TelefonoDestinatario { get; set; }

        public string NombreDestinatario { get; set; }

        public decimal Monto { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaDeRegistro { get; set; }

        public int Estado { get; set; }

        // Relaciones

        public virtual CajaSINPE CajaSINPE { get; set; }

        public virtual ICollection<BitacoraEventos> BitacoraEventos { get; set; }

        public PagoSINPE()
        {
            BitacoraEventos = new HashSet<BitacoraEventos>();
        }
    }
}