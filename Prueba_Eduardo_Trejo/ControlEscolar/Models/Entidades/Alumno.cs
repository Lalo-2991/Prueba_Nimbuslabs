using System;
using System.Collections.Generic;

namespace ControlEscolar.Models.Entidades
{
    public partial class Alumno
    {
        public Alumno()
        {
            MateriasAlumno = new HashSet<MateriasAlumno>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public int Edad { get; set; }

        public virtual ICollection<MateriasAlumno> MateriasAlumno { get; set; }
    }
}
