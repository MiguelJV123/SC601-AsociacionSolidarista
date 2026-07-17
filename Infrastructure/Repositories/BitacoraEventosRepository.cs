using proyecto.asociacionsolidarista.Infrastructure.DbContexts;
using proyecto.asociacionsolidarista.Models.Entities;

namespace proyecto.asociacionsolidarista.Infrastructure.Repositories
{
    public class BitacoraEventosRepository : Repository<BitacoraEventos>, IBitacoraEventosRepository
    {
        public BitacoraEventosRepository(AsociacionSolidaristaDbContext context)
            : base(context)
        {
        }
    }
}