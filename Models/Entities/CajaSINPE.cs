using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto.asociacionsolidarista.Models.Entities
{
    public class CajaSINPE
    {
        public int IdCaja { get; set; }

        public int IdComercio { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string TelefonoSINPE { get; set; }

        public DateTime FechaDeRegistro { get; set; }

        public DateTime? FechaDeModificacion { get; set; }

        public int Estado { get; set; }

        // Relaciones

        public virtual Comercio Comercio { get; set; }

        public virtual ICollection<PagoSINPE> PagosSINPE { get; set; }

        public CajaSINPE()
        {
            PagosSINPE = new HashSet<PagoSINPE>();
        }
    }
}