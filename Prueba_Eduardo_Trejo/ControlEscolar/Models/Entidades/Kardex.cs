using System.ComponentModel.DataAnnotations;

namespace ControlEscolar.Models.Entidades
{
    public class Kardex
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string? Apellidos { get; set; }

        [Required(ErrorMessage = "La edad es obligatoria.")]
        [Range(5, 100, ErrorMessage = "La edad debe estar entre 5 y 100 años")]
        public int Edad { get; set; }

        public int Creditos { get; set; }
        public virtual List<MateriasEstatus>? Materias { get; set; } 

        public Kardex()
        {
            Materias = new List<MateriasEstatus>();
        }
    }
}
