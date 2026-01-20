using dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using negocio;
namespace Discos_mvc.Controllers
{
    public class DiscosController : Controller
    {
        // GET: DiscosController
        public ActionResult Index(string filtro)
        {
            DiscoNegocio negocioDisco = new DiscoNegocio();
            var discos = negocioDisco.Listar();

            if (!string.IsNullOrEmpty(filtro))
            {
                discos = discos.FindAll(d => d.Nombre.Contains(filtro));
            }

            return View(discos);
        }

        // GET: DiscosController/Details/5
        public ActionResult Details(int id)
        {
            PlataformaNegocio negocioPlataforma = new PlataformaNegocio();
            DiscoNegocio discoNegocio = new DiscoNegocio();

            var disco = discoNegocio.Listar().Find(d => d.IdDisco == id);

            ViewBag.Plataformas = new SelectList(negocioPlataforma.Listar(), "Id", "Descripcion");


            GenerosNegocio negocioGenero = new GenerosNegocio();
            ViewBag.Generos = new SelectList(negocioGenero.Listar(), "Id", "Descripcion");
            return View(disco);
        }

        // GET: DiscosController/Create
        // Listado de desplegables
        public ActionResult Create()
        {
            //recordar confirgurar las sobrecargas
            PlataformaNegocio negocioPlataforma = new PlataformaNegocio();
            ViewBag.Plataforma = new SelectList(negocioPlataforma.Listar(), "Id", "Descripcion");

            GenerosNegocio negocioGenero = new GenerosNegocio();
            ViewBag.Genero = new SelectList(negocioGenero.Listar(), "Id", "Descripcion");
            return View();
        }

        // POST: DiscosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //esto se va a modificar para que pueda recibir realmente lo que queremos este form collectionb es una colecion de elemnetos. Porque sabemos en este caso lo que es.
        public ActionResult Create(Disco disco)
        {
            try
            {
                //Para terminar el alta terminar el alta se tiene que utilizar la clase de negocio.
                //Agregacion de productos
                DiscoNegocio negocioDisco = new DiscoNegocio();
                //Hacer que esto sea dinamico
                //disco.Genero = new Genero { Id = 1 };
                //disco.Plataforma = new Plataforma { Id = 2 };
                negocioDisco.agregar(disco);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DiscosController/Edit/5
        public ActionResult Edit(int id)
        {
            PlataformaNegocio negocioPlataforma = new PlataformaNegocio();
            DiscoNegocio discoNegocio = new DiscoNegocio();

            var disco = discoNegocio.Listar().Find(d => d.IdDisco == id);

            ViewBag.Plataformas = new SelectList(negocioPlataforma.Listar(), "Id", "Descripcion");


            GenerosNegocio negocioGenero = new GenerosNegocio();
            ViewBag.Generos = new SelectList(negocioGenero.Listar(), "Id", "Descripcion");
            return View(disco);
        }

        // POST: DiscosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Disco disco)
        {
            try
            {
                DiscoNegocio discoNegocio = new DiscoNegocio();
                discoNegocio.modificar(disco);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DiscosController/Delete/5
        public ActionResult Delete(int id)
        {
            DiscoNegocio discoNegocio = new DiscoNegocio();

            var disco = discoNegocio.Listar().Find(d => d.IdDisco == id);
            return View(disco);
        }

        // POST: DiscosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Disco disco)
        {
            try
            {
                DiscoNegocio discoNegocio = new DiscoNegocio();
                discoNegocio.eliminar(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
