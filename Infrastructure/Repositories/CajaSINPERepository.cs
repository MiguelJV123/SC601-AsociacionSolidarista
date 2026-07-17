using proyecto.asociacionsolidarista.Infrastructure.DbContexts;
using proyecto.asociacionsolidarista.Models.Entities;

namespace proyecto.asociacionsolidarista.Infrastructure.Repositories
{
    public class CajaSINPERepository : Repository<CajaSINPE>, ICajaSINPERepository
    {
        public CajaSINPERepository(AsociacionSolidaristaDbContext context)
            : base(context)
        {
        }
    }
}