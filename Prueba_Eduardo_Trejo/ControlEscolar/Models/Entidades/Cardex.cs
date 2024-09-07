namespace ControlEscolar.Models.Entidades
{
    public class Cardex
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public int Creditos { get; set; }
        public virtual ICollection<Materia> Materia1 { get; set; }
        public virtual ICollection<Materia> Materia2 { get; set; }
        public virtual ICollection<Materia> Materia3 { get; set; }
        public virtual ICollection<Materia> Materia4 { get; set; }

    }
}
