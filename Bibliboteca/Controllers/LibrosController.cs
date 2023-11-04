using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers
{
    public class LibrosController : Controller
    {
        private readonly BibliotecaContext _context;

        public LibrosController(BibliotecaContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var libros = _context.Libros.Include(l => l.Autor);
            return View(libros.ToList());
        }

        public ActionResult Agregar()
        {
            var autores = _context.Autores.ToList();
            var model = new Libro();
            ViewBag.AutorID = new SelectList(autores, "Id", "Nombre");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Libros.Add(libro);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AutorID = new SelectList(_context.Autores, "Id", "Nombre", libro.Id);
            return View(libro);
        }

    }
}
