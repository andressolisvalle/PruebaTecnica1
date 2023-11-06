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

        public ActionResult Index()
        {
            var autores = _context.Autores.ToList();
            return View(autores);
        }

        public ActionResult Agregar()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Autor autor)
        {

            

            if (ModelState.IsValid)
            {
                var existingAuthor = _context.Autores.FirstOrDefault(a => a.Nombre == autor.Nombre);

                if (existingAuthor != null)
                {
                    TempData["MessageError"] = "Ya existe un autor con el mismo nombre.";

                    return View(autor);
                }
                _context.Autores.Add(autor);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "El autor se ha guardado correctamente.";


            }

            return View(autor);

        }

        

        public ActionResult Eliminar(int id)
        {
            var autor = _context.Autores.Find(id);

            _context.Autores.Remove(autor);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
