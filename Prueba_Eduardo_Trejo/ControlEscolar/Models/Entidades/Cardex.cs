namespace ControlEscolar.Models.Entidades
{
    public class Cardex
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public int Edad { get; set; }
        public int Creditos { get; set; }
        public virtual List<MateriasEstatus>? Materias { get; set; } 

        public Cardex()
        {
            Materias = new List<MateriasEstatus>();
        }
    }
}
