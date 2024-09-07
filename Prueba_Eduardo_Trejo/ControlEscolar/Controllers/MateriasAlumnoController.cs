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
using Newtonsoft.Json;

namespace ControlEscolar.Controllers
{
    public class MateriasAlumnoController : Controller
    {
        private readonly NMateriasAlumno _NMateriasAlumno;
        private readonly NMateria _NMateria;
        private readonly NAlumno _NAlumno;

        public MateriasAlumnoController(NMateriasAlumno nMateriasAlumno, NMateria nMateria, NAlumno nAlumno)
        {
            _NMateriasAlumno = nMateriasAlumno;
            _NMateria = nMateria;
            _NAlumno = nAlumno;
        }

        // GET: MateriasAlumno
        public async Task<IActionResult> Index()
        {
            List<MateriasAlumno> lstMateriasAlumnos = await _NMateriasAlumno.Consultar();
            List<Alumno> lstAlumnos = await _NAlumno.Consultar();
            List<Materia> lstMaterias = await _NMateria.Consultar();

            var listadoIndex =
                from caso in lstMateriasAlumnos
                join alumno in lstAlumnos on caso.IdAlumno equals alumno.Id
                join materia in lstMaterias on caso.IdMateria equals materia.Id
                group new {alumno, materia} by new { alumno.Id, alumno.Nombre, alumno.Apellidos, alumno.Edad } into grupo
                select new
                {
                    Id = grupo.Key.Id,
                    Nombre = grupo.Key.Nombre,
                    Apellidos = grupo.Key.Apellidos,
                    Edad = grupo.Key.Edad,
                    Creditos = grupo.Sum(g => g.materia.Creditos)
                };
            string json = JsonConvert.SerializeObject(listadoIndex);
            List<Cardex> lstCardex = JsonConvert.DeserializeObject<List<Cardex>>(json);
            return View(lstCardex);
        }

        // GET: MateriasAlumno/Details/5
        public async Task<IActionResult> Details(int id)
        {
            MateriasAlumno oMateriasAlumno = await _NMateriasAlumno.Consultar(id);
            return View(oMateriasAlumno);
        }

        // GET: MateriasAlumno/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IdAlumno"] = new SelectList(await _NAlumno.Consultar(), "Id", "Nombre");
            ViewData["IdMateria"] = new SelectList(await _NMateria.Consultar(), "Id", "Nombre");
            return View();
        }

        // POST: MateriasAlumno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdAlumno,IdMateria")] MateriasAlumno oMateriasAlumno)
        {
            if (ModelState.IsValid)
            {
                await _NMateriasAlumno.Agregar(oMateriasAlumno);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlumno"] = new SelectList(await _NAlumno.Consultar(), "Id", "Nombre");
            ViewData["IdMateria"] = new SelectList(await _NMateria.Consultar(), "Id", "Nombre");
            return View(oMateriasAlumno);
        }

        // GET: MateriasAlumno/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            MateriasAlumno oMateriasAlumno = await _NMateriasAlumno.Consultar(id);
            ViewData["IdAlumno"] = new SelectList(await _NAlumno.Consultar(), "Id", "Nombre");
            ViewData["IdMateria"] = new SelectList(await _NMateria.Consultar(), "Id", "Nombre");
            return View(oMateriasAlumno);
        }

        // POST: MateriasAlumno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdAlumno,IdMateria")] MateriasAlumno oMateriasAlumno)
        {
            if (ModelState.IsValid)
            {
                await _NMateriasAlumno.Actualizar(oMateriasAlumno);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlumno"] = new SelectList(await _NAlumno.Consultar(), "Id", "Nombre", oMateriasAlumno.IdAlumno);
            ViewData["IdMateria"] = new SelectList(await _NMateria.Consultar(), "Id", "Nombre", oMateriasAlumno.IdMateria);
            return View(oMateriasAlumno);
        }

        // GET: MateriasAlumno/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            MateriasAlumno oMateriasAlumno = await _NMateriasAlumno.Consultar(id);
            return View(oMateriasAlumno);
        }

        // POST: MateriasAlumno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _NMateriasAlumno.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
