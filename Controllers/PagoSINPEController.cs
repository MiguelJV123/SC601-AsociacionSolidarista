using System;
using System.Linq;
using System.Web.Mvc;
using proyecto.asociacionsolidarista.Infrastructure.DbContexts;
using proyecto.asociacionsolidarista.Infrastructure.Repositories;
using proyecto.asociacionsolidarista.Models.Entities;

namespace proyecto.asociacionsolidarista.Controllers
{
    public class PagoSINPEController : Controller
    {
        private readonly IPagoSINPERepository _pagoRepository;
        private readonly ICajaSINPERepository _cajaRepository;

        public PagoSINPEController()
        {
            var context = new AsociacionSolidaristaDbContext();
            _pagoRepository = new PagoSINPERepository(context);
            _cajaRepository = new CajaSINPERepository(context);
        }

        private SelectList GetCajasSelectList(object selectedValue = null)
        {
            var cajas = _cajaRepository.GetAll().Select(c => new
            {
                IdCaja = c.IdCaja,
                NombreCompleto = c.Nombre + " - " + (c.Comercio != null ? c.Comercio.Nombre : "Sin comercio")
            });
            return new SelectList(cajas, "IdCaja", "NombreCompleto", selectedValue);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var pagos = _pagoRepository.GetAll();
            return View(pagos);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new PagoSINPE
            {
                FechaDeRegistro = DateTime.Now,
                Estado = 1
            };

            ViewBag.IdCaja = GetCajasSelectList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PagoSINPE pago)
        {
            if (ModelState.IsValid)
            {
                var caja = _cajaRepository.GetById(pago.IdCaja);
                if (caja == null)
                {
                    ModelState.AddModelError("IdCaja", "La Caja SINPE seleccionada no existe.");
                }
                else
                {
                    pago.FechaDeRegistro = DateTime.Now;
                    _pagoRepository.Add(pago);

                    TempData["Success"] = "Pago SINPE registrado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.IdCaja = GetCajasSelectList(pago.IdCaja);
            return View(pago);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var pago = _pagoRepository.GetById(id);
            if (pago == null) return HttpNotFound();

            return View(pago);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pago = _pagoRepository.GetById(id);
            if (pago == null) return HttpNotFound();

            ViewBag.IdCaja = GetCajasSelectList(pago.IdCaja);
            return View(pago);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PagoSINPE pago)
        {
            if (ModelState.IsValid)
            {
                var caja = _cajaRepository.GetById(pago.IdCaja);
                if (caja == null)
                {
                    ModelState.AddModelError("IdCaja", "La Caja SINPE seleccionada no existe.");
                }
                else
                {
                    var existingPago = _pagoRepository.GetById(pago.IdSinpe);
                    if (existingPago != null)
                    {
                        existingPago.IdCaja = pago.IdCaja;
                        existingPago.TelefonoOrigen = pago.TelefonoOrigen;
                        existingPago.NombreOrigen = pago.NombreOrigen;
                        existingPago.TelefonoDestinatario = pago.TelefonoDestinatario;
                        existingPago.NombreDestinatario = pago.NombreDestinatario;
                        existingPago.Monto = pago.Monto;
                        existingPago.Descripcion = pago.Descripcion;
                        existingPago.Estado = pago.Estado;
                        
                        _pagoRepository.Update(existingPago);

                        TempData["Success"] = "Pago SINPE actualizado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    return HttpNotFound();
                }
            }

            ViewBag.IdCaja = GetCajasSelectList(pago.IdCaja);
            return View(pago);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var pago = _pagoRepository.GetById(id);
            if (pago == null) return HttpNotFound();

            return View(pago);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var pago = _pagoRepository.GetById(id);
            if (pago == null)
            {
                TempData["Error"] = "El pago no existe.";
                return RedirectToAction(nameof(Index));
            }

            _pagoRepository.Remove(pago);

            TempData["Success"] = "Pago SINPE eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
