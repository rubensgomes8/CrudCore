using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crudmysql.Models;

namespace crudmysql.Controllers
{
    public class FuncionalidadeController : Controller
    {
        private readonly AppDbContext _context;

        public FuncionalidadeController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listar usuarios
        /// </summary>
        /// <returns>Lista de funcionalidades</returns>
        public ActionResult Index()
        {
            return View(_context.Funcionalidades.ToList());
        }

        /// <summary>
        /// Detalhes da funcionalidade
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionalidade = _context.Funcionalidades
                .SingleOrDefault(m => m.IdFuncionalidade == id);
            if (funcionalidade == null)
            {
                return NotFound();
            }

            return View(funcionalidade);
        }

        /// <summary>
        /// Criar funcionalidade
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Create()
        {
            ViewBag.Perfis = _context.Perfis.ToList();
            return View();
        }

        /// <summary>
        /// Criar funcionalidade
        /// </summary>
        /// <param name="funcionalidade">funcionalidade</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult Create([Bind("IdFuncionalidade,Nome,Descricao,IdPerfil")] Funcionalidade funcionalidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionalidade);
                _context.SaveChanges();
                TempData["Message"] = "Funcionalidade cadastrada com sucesso";
                return RedirectToAction(nameof(Index));
            }
            return View(funcionalidade);
        }

        /// <summary>
        /// Editar funcionalidade
        /// </summary>
        /// <param name="id">Id da funcionalidade</param>
        /// <returns>View</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionalidade = _context.Funcionalidades.SingleOrDefault(m => m.IdFuncionalidade == id);
            if (funcionalidade == null)
            {
                return NotFound();
            }
            ViewBag.Perfis = _context.Perfis.ToList();
            return View(funcionalidade);
        }

        /// <summary>
        /// Editar funcionalidade
        /// </summary>
        /// <param name="id">id da funcionalidade</param>
        /// <param name="funcionalidade">Funcionalidade</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult Edit(int id, [Bind("IdFuncionalidade,Nome,Descricao,IdPerfil")] Funcionalidade funcionalidade)
        {
            if (id != funcionalidade.IdFuncionalidade)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionalidade);
                    _context.SaveChanges();
                    TempData["Message"] = "Funcionalidade alterada com sucesso";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionalidadeExists(funcionalidade.IdFuncionalidade))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(funcionalidade);
        }

        /// <summary>
        /// Deletar funcionalidade
        /// </summary>
        /// <param name="id">Id da funcionalidade</param>
        /// <returns>View</returns>
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionalidade = _context.Funcionalidades
                .SingleOrDefault(m => m.IdFuncionalidade == id);
            if (funcionalidade == null)
            {
                return NotFound();
            }

            return View(funcionalidade);
        }

        /// <summary>
        /// Deletar funcionalidade
        /// </summary>
        /// <param name="id">Id da funcionalidade</param>
        /// <returns>View</returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var funcionalidade = _context.Funcionalidades.SingleOrDefault(m => m.IdFuncionalidade == id);
            _context.Funcionalidades.Remove(funcionalidade);
            _context.SaveChanges();
            TempData["Message"] = "Funcionalidade excluida com sucesso";
            return RedirectToAction(nameof(Index));
        }


        /// <summary>
        /// Verificar se funcionalidade já existe
        /// </summary>
        /// <param name="id">Id da funcionalidade</param>
        /// <returns>View</returns>
        private bool FuncionalidadeExists(int id)
        {
            return _context.Funcionalidades.Any(e => e.IdFuncionalidade == id);
        }
    }
}
