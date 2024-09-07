using System;
using System.Collections.Generic;

namespace ControlEscolar.Models.Entidades
{
    public partial class Materia
    {
        public Materia()
        {
            MateriasAlumno = new HashSet<MateriasAlumno>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int? Creditos { get; set; }

        public virtual ICollection<MateriasAlumno> MateriasAlumno { get; set; }
    }
}
