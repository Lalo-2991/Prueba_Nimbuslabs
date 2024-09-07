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
    public class AlumnoController : Controller
    {
        private readonly NAlumno _NAlumno;

        public AlumnoController(NAlumno nAlumno)
        {
            _NAlumno = nAlumno;
        }

        // GET: Alumno
        public async Task<IActionResult> Index()
        {
            List<Alumno>lstAlumnos = await _NAlumno.Consultar();
            return View(lstAlumnos);
        }

        // GET: Alumno/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Alumno oAlumno = await _NAlumno.Consultar(id);
            return View(oAlumno);
        }

        // GET: Alumno/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alumno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellidos,Edad")] Alumno oAlumno)
        {
            if (ModelState.IsValid)
            {
                await _NAlumno.Agregar(oAlumno);
                return RedirectToAction(nameof(Index));
            }
            return View(oAlumno);
        }

        // GET: Alumno/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Alumno oAlumno = await _NAlumno.Consultar(id);
            return View(oAlumno);
        }

        // POST: Alumno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellidos,Edad")] Alumno oAlumno)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _NAlumno.Actualizar(oAlumno);
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(oAlumno);
        }

        // GET: Alumno/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Alumno oAlumno = await _NAlumno.Consultar(id);
            return View(oAlumno);
        }

        // POST: Alumno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _NAlumno.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
