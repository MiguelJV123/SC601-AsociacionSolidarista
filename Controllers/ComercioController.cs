using System;
using System.Web.Mvc;
using proyecto.asociacionsolidarista.Infrastructure.DbContexts;
using proyecto.asociacionsolidarista.Infrastructure.Repositories;
using proyecto.asociacionsolidarista.Models.Entities;

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

        [HttpGet]
        public ActionResult Create()
        {
            var model = new Comercio
            {
                FechaDeRegistro = DateTime.Now,
                Estado = 1
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comercio comercio)
        {
            if (ModelState.IsValid)
            {
                comercio.FechaDeRegistro = DateTime.Now;
                comercio.FechaDeModificacion = null;

                _repository.Add(comercio);

                TempData["Success"] = "Comercio registrado correctamente.";

                return RedirectToAction(nameof(Index));
            }

            return View(comercio);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var comercio = _repository.GetById(id);

            if (comercio == null)
            {
                return HttpNotFound();
            }

            return View(comercio);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var comercio = _repository.GetById(id);

            if (comercio == null)
            {
                return HttpNotFound();
            }

            return View(comercio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comercio comercio)
        {
            if (ModelState.IsValid)
            {
                comercio.FechaDeModificacion = DateTime.Now;

                _repository.Update(comercio);

                TempData["Success"] = "Comercio actualizado correctamente.";

                return RedirectToAction(nameof(Index));
            }

            return View(comercio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var comercio = _repository.GetById(id);

            if (comercio == null)
            {
                TempData["Error"] = "El comercio no existe.";
                return RedirectToAction(nameof(Index));
            }

            _repository.Remove(comercio);

            TempData["Success"] = "Comercio eliminado correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}