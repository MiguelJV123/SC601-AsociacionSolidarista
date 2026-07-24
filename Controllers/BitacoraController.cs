using System;
using System.Linq;
using System.Web.Mvc;
using proyecto.asociacionsolidarista.Infrastructure.DbContexts;
using proyecto.asociacionsolidarista.Infrastructure.Repositories;
using proyecto.asociacionsolidarista.Models.Entities;

namespace proyecto.asociacionsolidarista.Controllers
{
    public class BitacoraController : Controller
    {
        private readonly IBitacoraEventosRepository _bitacoraRepository;

        public BitacoraController()
        {
            var context = new AsociacionSolidaristaDbContext();
            _bitacoraRepository = new BitacoraEventosRepository(context);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var eventos = _bitacoraRepository.GetAll().OrderByDescending(b => b.FechaDeEvento).ToList();
            return View(eventos);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var evento = _bitacoraRepository.GetById(id);
            if (evento == null) return HttpNotFound();
            
            return View(evento);
        }
    }
}
