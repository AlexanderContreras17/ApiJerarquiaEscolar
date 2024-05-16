namespace ApiJerarquia.Models.DTOs
{
    public class ActividadCreateDTO
    {
        public int? Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int Departamento { get; set; }
        public DateOnly? FechaRealizacion { get; set; }//Para tener la fecha de registro
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public int Estado { get; set; } = 1;
    }
}
