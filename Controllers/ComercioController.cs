using System.Web.Mvc;
using proyecto.asociacionsolidarista.Infrastructure.DbContexts;
using proyecto.asociacionsolidarista.Infrastructure.Repositories;

namespace proyecto.asociacionsolidarista.Controllers
{
    public class ComercioController : Controller
    {
        private readonly IComercioRepository _repository;

        public ComercioController()
        {
            var context = new AsociacionSolidaristaDbContext();

            _repository = new ComercioRepository(context);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var comercios = _repository.GetAll();

            return View(comercios);
        }
    }
}