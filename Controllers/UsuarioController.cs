using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crudmysql.Models;
using System;

namespace crudmysql.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listar usuarios
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        public ActionResult Index()
        {
            return View(_context.Usuarios.ToList());
        }

        /// <summary>
        /// Detalhes do usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _context.Usuarios
                .SingleOrDefault(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        /// <summary>
        /// Criar usuario
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Create()
        {
            ViewBag.Perfis = _context.Perfis.ToList();
            return View();
        }

        /// <summary>
        /// Criar usuario
        /// </summary>
        /// <param name="usuario">usuario</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult Create([Bind("Id,Nome,Idade,IdPerfil")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(usuario);
                    _context.SaveChanges();
                    TempData["Message"] = "Usuario cadastrado com sucesso";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                TempData["MessageError"] = "Ocorreu um erro ao salvar usuario";
            }

            return View(usuario);
        }

        /// <summary>
        /// Editar usuario
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <returns>View</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _context.Usuarios.SingleOrDefault(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            ViewBag.Perfis = _context.Perfis.ToList();
            return View(usuario);
        }

        /// <summary>
        /// Editar usuario
        /// </summary>
        /// <param name="id">id do usuario</param>
        /// <param name="usuario">Usuario</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult Edit(int id, [Bind("Id,Nome,Idade,IdPerfil")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    _context.SaveChanges();
                    TempData["Message"] = "Usuario alterado com sucesso";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            TempData["MessageError"] = "Ocorreu um erro ao alterar usuario";
            return View(usuario);
        }

        /// <summary>
        /// Deletar usuario
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <returns>View</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _context.Usuarios
                .SingleOrDefault(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        /// <summary>
        /// Deletar usuario
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <returns>View</returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var usuario = _context.Usuarios.SingleOrDefault(m => m.Id == id);
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
                TempData["Message"] = "Usuario excluido com sucesso";
            }

            catch (Exception)
            {
                TempData["MessageError"] = "Ocorreu um erro ao excluir usuario";
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verificar se usuario já existe
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <returns>View</returns>
        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
