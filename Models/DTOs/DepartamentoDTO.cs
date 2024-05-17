﻿namespace ApiJerarquia.Models.DTOs
{
    public class DepartamentoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? DepartamentoSuperior { get; set; }   
    }
}
