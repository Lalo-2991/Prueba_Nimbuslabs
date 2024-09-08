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
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            List<Alumno> lstAlumnos = await _NAlumno.Consultar();
            List<Cardex> lstCardex = new List<Cardex>();

            foreach(Alumno oAlumno in lstAlumnos)
            {
                Cardex oCardex = await ConsultarCardex(oAlumno.Id);
                lstCardex.Add(oCardex);
            }
            return View(lstCardex);
        }

        // GET: MateriasAlumno/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Cardex oCardex = await ConsultarCardex(id);
            return View(oCardex);
        }

        // GET: MateriasAlumno/Create
        public async Task<IActionResult> Create()
        {
            List<Materia> lstMaterias = await _NMateria.Consultar();
            Cardex oCardex = new Cardex();

            foreach(Materia oMateria in lstMaterias)
            {
                MateriasEstatus oMateriasEstatus = new MateriasEstatus();
                oMateriasEstatus.Id = oMateria.Id;
                oMateriasEstatus.Nombre = oMateria.Nombre;
                oMateriasEstatus.Creditos = oMateria.Creditos;
                oMateriasEstatus.Estatus = false;
                oCardex.Materias.Add(oMateriasEstatus);
            }
            
            return View(oCardex);
        }

        // POST: MateriasAlumno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Apellidos,Edad,Materias")] Cardex oCardex)
        {
            List<MateriasAlumno> lstMateriasAlumno = await _NMateriasAlumno.Consultar();
            if (ModelState.IsValid)
            {
                Alumno oAlumno = new Alumno();
                oAlumno.Nombre = oCardex.Nombre;
                oAlumno.Apellidos = oCardex.Apellidos;
                oAlumno.Edad = oCardex.Edad;
                Alumno oAlumnoRespuesta = await _NAlumno.Agregar(oAlumno);

                MateriasAlumno oMateriasAlumno = new MateriasAlumno();
                oMateriasAlumno.IdAlumno = oAlumnoRespuesta.Id;
                foreach(MateriasEstatus oMateria in oCardex.Materias)
                {
                    if (oMateria.Estatus == true)
                    {
                        if (lstMateriasAlumno.Find(x=> x.IdAlumno == oMateriasAlumno.Id && x.IdMateria == oMateria.Id) == null)
                        {
                            oMateriasAlumno.IdMateria = oMateria.Id;
                            await _NMateriasAlumno.Agregar(oMateriasAlumno);
                        }
                    }
                    else
                    {
                        if (lstMateriasAlumno.Find(x => x.IdAlumno == oMateriasAlumno.Id && x.IdMateria == oMateria.Id) != null)
                        {
                            MateriasAlumno oMateriasAlumnoEliminar = lstMateriasAlumno.Find(x => x.IdAlumno == oMateriasAlumno.Id && x.IdMateria == oMateria.Id);
                            await _NMateriasAlumno.Eliminar(oMateriasAlumnoEliminar.Id);
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(oCardex);
        }

        // GET: MateriasAlumno/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Cardex oCardex = await ConsultarCardex(id);
            return View(oCardex);
        }

        // POST: MateriasAlumno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellidos,Edad,Materias")] Cardex oCardex)
        {
            List<MateriasAlumno> lstMateriasAlumno = await _NMateriasAlumno.Consultar();
            if (ModelState.IsValid)
            {
                Alumno oAlumno = new Alumno();
                oAlumno.Id = oCardex.Id;
                oAlumno.Nombre = oCardex.Nombre;
                oAlumno.Apellidos = oCardex.Apellidos;
                oAlumno.Edad = oCardex.Edad;
                await _NAlumno.Actualizar(oAlumno);

                MateriasAlumno oMateriasAlumno = new MateriasAlumno();
                oMateriasAlumno.IdAlumno = oCardex.Id;
                foreach (MateriasEstatus oMateria in oCardex.Materias)
                {
                    if (oMateria.Estatus == true)
                    {
                        if (lstMateriasAlumno.Find(x => x.IdAlumno == oMateriasAlumno.IdAlumno && x.IdMateria == oMateria.Id) == null)
                        {
                            oMateriasAlumno.IdMateria = oMateria.Id;
                            await _NMateriasAlumno.Agregar(oMateriasAlumno);
                        }
                    }
                    else
                    {
                        if (lstMateriasAlumno.Find(x => x.IdAlumno == oMateriasAlumno.IdAlumno && x.IdMateria == oMateria.Id) != null)
                        {
                            MateriasAlumno oMateriasAlumnoEliminar = lstMateriasAlumno.Find(x => x.IdAlumno == oMateriasAlumno.IdAlumno && x.IdMateria == oMateria.Id);
                            await _NMateriasAlumno.Eliminar(oMateriasAlumnoEliminar.Id);
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(oCardex);
        }

        // GET: MateriasAlumno/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Cardex oCardex = await ConsultarCardex(id);
            return View(oCardex);
        }

        // POST: MateriasAlumno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _NAlumno.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<Cardex> ConsultarCardex(int id)
        {
            Alumno oAlumno = await _NAlumno.Consultar(id);
            Cardex oCardex = new Cardex();
            List<MateriasAlumno> lstMateriasAlumnos = await _NMateriasAlumno.Consultar();
            List<Materia> lstMaterias = await _NMateria.Consultar();

            oCardex.Id = oAlumno.Id;
            oCardex.Nombre = oAlumno.Nombre;
            oCardex.Apellidos = oAlumno.Apellidos;
            oCardex.Edad = oAlumno.Edad;

            foreach (Materia materia in lstMaterias)
            {
                MateriasEstatus oMateriaEstatus = new MateriasEstatus();
                oMateriaEstatus.Id = materia.Id;
                oMateriaEstatus.Nombre = materia.Nombre;
                oMateriaEstatus.Creditos = materia.Creditos;
                oMateriaEstatus.Estatus = false;
                foreach (MateriasAlumno registro in lstMateriasAlumnos)
                {
                    if (registro.IdAlumno == id && registro.IdMateria == materia.Id)
                    {
                        oMateriaEstatus.Estatus = true;
                        oCardex.Creditos += materia.Creditos;
                    }
                }
                oCardex.Materias.Add(oMateriaEstatus);
            }
            return oCardex;
        }
    }
}
