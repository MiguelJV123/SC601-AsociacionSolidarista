using proyecto.asociacionsolidarista.Infrastructure.DbContexts;
using proyecto.asociacionsolidarista.Models.Entities;

namespace proyecto.asociacionsolidarista.Infrastructure.Repositories
{
    public class PagoSINPERepository : Repository<PagoSINPE>, IPagoSINPERepository
    {
        public PagoSINPERepository(AsociacionSolidaristaDbContext context)
            : base(context)
        {
        }
    }
}