using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using crudmysql.Models;
using Newtonsoft.Json;

namespace crudmysql.Controllers
{
    public class PerfilController : Controller
    {
        private readonly AppDbContext _context;

        public PerfilController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listar perfis
        /// </summary>
        /// <returns>Lista de perfis</returns>
        public ActionResult Index()
        {
            return View(_context.Perfis.ToList());
        }

        /// <summary>
        /// Detalhes do perfil
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfil = _context.Perfis
                .SingleOrDefault(m => m.IdPerfil == id);
            if (perfil == null)
            {
                return NotFound();
            }

            return View(perfil);
        }

        /// <summary>
        /// Criar perfil
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Create()
        {
            ViewBag.Funcionalidade = _context.Funcionalidades.ToList();
            return View();
        }

        /// <summary>
        /// Criar perfil
        /// </summary>
        /// <param name="perfil">perfil</param>
        /// /// <param name="funcionalidades">funcionalidades</param>
        /// /// /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string perfil, string funcionalidades)
        {
            var objetoPerfil = JsonConvert.DeserializeObject<Perfil>(perfil);
            var listaFuncionalidades = JsonConvert.DeserializeObject<List<Funcionalidade>>(funcionalidades);

            if (ModelState.IsValid)
            {
                _context.Add(objetoPerfil);
                if (listaFuncionalidades.Count() > 0)
                {
                    foreach (Funcionalidade item in listaFuncionalidades)
                    {
                        var funcionalidade = _context.Funcionalidades.SingleOrDefault(m => m.IdFuncionalidade == item.IdFuncionalidade);
                        funcionalidade.IdPerfil = objetoPerfil.IdPerfil;
                        _context.Update(funcionalidade);
                    }
                }
            }

            _context.SaveChanges();
            TempData["Message"] = "Perfil cadastrado com sucesso";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Editar perfil
        /// </summary>
        /// <param name="id">Id do perfil</param>
        /// <returns>View</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfil = _context.Perfis.SingleOrDefault(m => m.IdPerfil == id);
            if (perfil == null)
            {
                return NotFound();
            }
            return View(perfil);
        }

        /// <summary>
        /// Editar perfil
        /// </summary>
        /// <param name="id">id do perfil</param>
        /// <param name="perfil">Perfil</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult Edit(int id, [Bind("IdPerfil,Nome,Descricao")] Perfil perfil)
        {
            if (id != perfil.IdPerfil)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfil);
                    _context.SaveChanges();
                    TempData["Message"] = "Perfil alterado com sucesso";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfilExists(perfil.IdPerfil))
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
            return View(perfil);
        }

        /// <summary>
        /// Deletar perfil
        /// </summary>
        /// <param name="id">Id do perfil</param>
        /// <returns>View</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfil = _context.Perfis
                .SingleOrDefault(m => m.IdPerfil == id);
            if (perfil == null)
            {
                return NotFound();
            }

            return View(perfil);
        }

        /// <summary>
        /// Deletar perfil
        /// </summary>
        /// <param name="id">Id do perfil</param>
        /// <returns>View</returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var perfil = _context.Perfis.SingleOrDefault(m => m.IdPerfil == id);
            _context.Perfis.Remove(perfil);
            _context.SaveChanges();
            TempData["Message"] = "Perfil excluido com sucesso";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verificar se perfil já existe
        /// </summary>
        /// <param name="id">Id do perfil</param>
        /// <returns>View</returns>
        private bool PerfilExists(int id)
        {
            return _context.Perfis.Any(e => e.IdPerfil == id);
        }
    }
}
