using System;
using System.Web.Mvc;
using proyecto.asociacionsolidarista.Infrastructure.DbContexts;
using proyecto.asociacionsolidarista.Infrastructure.Repositories;
using proyecto.asociacionsolidarista.Models.Entities;

namespace proyecto.asociacionsolidarista.Controllers
{
    public class CajaSINPEController : Controller
    {
        private readonly ICajaSINPERepository _cajaRepository;
        private readonly IComercioRepository _comercioRepository;

        public CajaSINPEController()
        {
            var context = new AsociacionSolidaristaDbContext();
            _cajaRepository = new CajaSINPERepository(context);
            _comercioRepository = new ComercioRepository(context);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var cajas = _cajaRepository.GetAll();
            return View(cajas);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CajaSINPE
            {
                FechaDeRegistro = DateTime.Now,
                Estado = 1
            };

            ViewBag.IdComercio = new SelectList(_comercioRepository.GetAll(), "IdComercio", "Nombre");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CajaSINPE caja)
        {
            if (ModelState.IsValid)
            {
                var comercio = _comercioRepository.GetById(caja.IdComercio);
                if (comercio == null)
                {
                    ModelState.AddModelError("IdComercio", "El comercio seleccionado no existe.");
                }
                else
                {
                    caja.FechaDeRegistro = DateTime.Now;
                    caja.FechaDeModificacion = null;
                    _cajaRepository.Add(caja);

                    TempData["Success"] = "Caja registrada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.IdComercio = new SelectList(_comercioRepository.GetAll(), "IdComercio", "Nombre", caja.IdComercio);
            return View(caja);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var caja = _cajaRepository.GetById(id);
            if (caja == null) return HttpNotFound();

            return View(caja);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var caja = _cajaRepository.GetById(id);
            if (caja == null) return HttpNotFound();

            ViewBag.IdComercio = new SelectList(_comercioRepository.GetAll(), "IdComercio", "Nombre", caja.IdComercio);
            return View(caja);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CajaSINPE caja)
        {
            if (ModelState.IsValid)
            {
                var comercio = _comercioRepository.GetById(caja.IdComercio);
                if (comercio == null)
                {
                    ModelState.AddModelError("IdComercio", "El comercio seleccionado no existe.");
                }
                else
                {
                    var existingCaja = _cajaRepository.GetById(caja.IdCaja);
                    if (existingCaja != null)
                    {
                        existingCaja.IdComercio = caja.IdComercio;
                        existingCaja.Nombre = caja.Nombre;
                        existingCaja.Descripcion = caja.Descripcion;
                        existingCaja.TelefonoSINPE = caja.TelefonoSINPE;
                        existingCaja.Estado = caja.Estado;
                        existingCaja.FechaDeModificacion = DateTime.Now;

                        _cajaRepository.Update(existingCaja);

                        TempData["Success"] = "Caja actualizada correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    return HttpNotFound();
                }
            }

            ViewBag.IdComercio = new SelectList(_comercioRepository.GetAll(), "IdComercio", "Nombre", caja.IdComercio);
            return View(caja);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var caja = _cajaRepository.GetById(id);
            if (caja == null) return HttpNotFound();

            return View(caja);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var caja = _cajaRepository.GetById(id);
            if (caja == null)
            {
                TempData["Error"] = "La caja no existe.";
                return RedirectToAction(nameof(Index));
            }

            _cajaRepository.Remove(caja);

            TempData["Success"] = "Caja eliminada correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
