using System;
using System.Collections.Generic;

namespace API_Alumnos.Model.Entidades
{
    public partial class MateriasAlumno
    {
        public int Id { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdMateria { get; set; }

        public virtual Alumno? IdAlumnoNavigation { get; set; }
        public virtual Materia? IdMateriaNavigation { get; set; }
    }
}
