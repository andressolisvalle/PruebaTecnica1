using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bibliboteca.Controllers
{
    public class AutoresController : Controller
    {

        private readonly BibliotecaContext _context;

        public AutoresController(BibliotecaContext context)
        {
            _context = context;
        }

        // Acción para mostrar la lista de autores
        public ActionResult Index()
        {
            var autores = _context.Autores.ToList();
            return View(autores);
        }

        // Acción para mostrar el formulario de agregar un autor
        public ActionResult Agregar()
        {
            
            return View();
        }

        // Acción para procesar la solicitud de agregar un autor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Autor autor)
        {
            if (ModelState.IsValid)
            {
                _context.Autores.Add(autor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autor);
        }
    }
}
