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
            List<Kardex> lstKardex = new List<Kardex>();

            foreach(Alumno oAlumno in lstAlumnos)
            {
                Kardex oKardex = await ConsultarKardex(oAlumno.Id);
                lstKardex.Add(oKardex);
            }
            return View(lstKardex);
        }

        // GET: MateriasAlumno/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Kardex oKardex = await ConsultarKardex(id);
            return View(oKardex);
        }

        // GET: MateriasAlumno/Create
        public async Task<IActionResult> Create()
        {
            List<Materia> lstMaterias = await _NMateria.Consultar();
            Kardex oKardex = new Kardex();

            foreach(Materia oMateria in lstMaterias)
            {
                MateriasEstatus oMateriasEstatus = new MateriasEstatus();
                oMateriasEstatus.Id = oMateria.Id;
                oMateriasEstatus.Nombre = oMateria.Nombre;
                oMateriasEstatus.Creditos = oMateria.Creditos;
                oMateriasEstatus.Estatus = false;
                oKardex.Materias.Add(oMateriasEstatus);
            }
            
            return View(oKardex);
        }

        // POST: MateriasAlumno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Apellidos,Edad,Materias")] Kardex oKardex)
        {
            List<MateriasAlumno> lstMateriasAlumno = await _NMateriasAlumno.Consultar();
            if (ModelState.IsValid)
            {
                Alumno oAlumno = new Alumno();
                oAlumno.Nombre = oKardex.Nombre;
                oAlumno.Apellidos = oKardex.Apellidos;
                oAlumno.Edad = oKardex.Edad;
                Alumno oAlumnoRespuesta = await _NAlumno.Agregar(oAlumno);

                MateriasAlumno oMateriasAlumno = new MateriasAlumno();
                oMateriasAlumno.IdAlumno = oAlumnoRespuesta.Id;
                foreach(MateriasEstatus oMateria in oKardex.Materias)
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
            return View(oKardex);
        }

        // GET: MateriasAlumno/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Kardex oKardex = await ConsultarKardex(id);
            return View(oKardex);
        }

        // POST: MateriasAlumno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellidos,Edad,Materias")] Kardex oKardex)
        {
            List<MateriasAlumno> lstMateriasAlumno = await _NMateriasAlumno.Consultar();
            if (ModelState.IsValid)
            {
                Alumno oAlumno = new Alumno();
                oAlumno.Id = oKardex.Id;
                oAlumno.Nombre = oKardex.Nombre;
                oAlumno.Apellidos = oKardex.Apellidos;
                oAlumno.Edad = oKardex.Edad;
                await _NAlumno.Actualizar(oAlumno);

                MateriasAlumno oMateriasAlumno = new MateriasAlumno();
                oMateriasAlumno.IdAlumno = oKardex.Id;
                foreach (MateriasEstatus oMateria in oKardex.Materias)
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
            return View(oKardex);
        }

        // GET: MateriasAlumno/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Kardex oKardex = await ConsultarKardex(id);
            return View(oKardex);
        }

        // POST: MateriasAlumno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _NAlumno.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<Kardex> ConsultarKardex(int id)
        {
            Alumno oAlumno = await _NAlumno.Consultar(id);
            Kardex oKardex = new Kardex();
            List<MateriasAlumno> lstMateriasAlumnos = await _NMateriasAlumno.Consultar();
            List<Materia> lstMaterias = await _NMateria.Consultar();

            oKardex.Id = oAlumno.Id;
            oKardex.Nombre = oAlumno.Nombre;
            oKardex.Apellidos = oAlumno.Apellidos;
            oKardex.Edad = oAlumno.Edad;

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
                        oKardex.Creditos += materia.Creditos;
                    }
                }
                oKardex.Materias.Add(oMateriaEstatus);
            }
            return oKardex;
        }
    }
}
