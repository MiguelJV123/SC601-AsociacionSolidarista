using proyecto.asociacionsolidarista.Infrastructure.DbContexts;
using proyecto.asociacionsolidarista.Models.Entities;

namespace proyecto.asociacionsolidarista.Infrastructure.Repositories
{
    public class ComercioRepository : Repository<Comercio>, IComercioRepository
    {
        public ComercioRepository(AsociacionSolidaristaDbContext context)
            : base(context)
        {
        }
    }
}