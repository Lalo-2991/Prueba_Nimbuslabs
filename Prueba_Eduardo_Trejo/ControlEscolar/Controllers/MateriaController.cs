using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControlEscolar.Models.Context;
using ControlEscolar.Models.Entidades;
using ControlEscolar.Models;

namespace ControlEscolar.Controllers
{
    public class MateriaController : Controller
    {
        private readonly NMateria _NMateria;

        public MateriaController(NMateria nMateria)
        {
            _NMateria = nMateria;
        }

        // GET: Materia
        public async Task<IActionResult> Index()
        {
            List<Materia> lstMateria = await _NMateria.Consultar();
            return View(lstMateria);
        }

        // GET: Materia/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Materia oMateria = await _NMateria.Consultar(id);
            return View(oMateria);
        }

        // GET: Materia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Materia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Creditos")] Materia oMateria)
        {
            if (ModelState.IsValid)
            {
                await _NMateria.Agregar(oMateria);
                return RedirectToAction(nameof(Index));
            }
            return View(oMateria);
        }

        // GET: Materia/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Materia oMateria = await _NMateria.Consultar(id);
            return View(oMateria);
        }

        // POST: Materia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Creditos")] Materia oMateria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _NMateria.Actualizar(oMateria);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(oMateria);
        }

        // GET: Materia/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Materia oMateria = await _NMateria.Consultar(id);
            return View(oMateria);
        }

        // POST: Materia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _NMateria.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
