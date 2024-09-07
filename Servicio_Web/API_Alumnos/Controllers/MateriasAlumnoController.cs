using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Alumnos.Model.Context;
using API_Alumnos.Model.Entidades;

namespace API_Alumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasAlumnoController : ControllerBase
    {
        private readonly ControlContext _context;

        public MateriasAlumnoController(ControlContext context)
        {
            _context = context;
        }

        // GET: api/MateriasAlumno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriasAlumno>>> GetMateriasAlumno()
        {
          if (_context.MateriasAlumno == null)
          {
              return NotFound();
          }
            return await _context.MateriasAlumno.ToListAsync();
        }

        // GET: api/MateriasAlumno/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriasAlumno>> GetMateriasAlumno(int id)
        {
          if (_context.MateriasAlumno == null)
          {
              return NotFound();
          }
            var materiasAlumno = await _context.MateriasAlumno.FindAsync(id);

            if (materiasAlumno == null)
            {
                return NotFound();
            }

            return materiasAlumno;
        }

        // PUT: api/MateriasAlumno/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateriasAlumno(int id, MateriasAlumno materiasAlumno)
        {
            if (id != materiasAlumno.Id)
            {
                return BadRequest();
            }

            _context.Entry(materiasAlumno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriasAlumnoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MateriasAlumno
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MateriasAlumno>> PostMateriasAlumno(MateriasAlumno materiasAlumno)
        {
          if (_context.MateriasAlumno == null)
          {
              return Problem("Entity set 'ControlContext.MateriasAlumno'  is null.");
          }
            _context.MateriasAlumno.Add(materiasAlumno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMateriasAlumno", new { id = materiasAlumno.Id }, materiasAlumno);
        }

        // DELETE: api/MateriasAlumno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateriasAlumno(int id)
        {
            if (_context.MateriasAlumno == null)
            {
                return NotFound();
            }
            var materiasAlumno = await _context.MateriasAlumno.FindAsync(id);
            if (materiasAlumno == null)
            {
                return NotFound();
            }

            _context.MateriasAlumno.Remove(materiasAlumno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MateriasAlumnoExists(int id)
        {
            return (_context.MateriasAlumno?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
