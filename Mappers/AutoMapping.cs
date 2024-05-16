﻿using ApiJerarquia.Models.Entities;
using AutoMapper;
using ApiJerarquia.Models.DTOs;
namespace ApiJerarquia.Mappers
    
{
    public class AutoMapping:Profile
    {
        public AutoMapping() 
        {
            CreateMap<Actividades, ActividadDTO>()
                .ForMember(destinationMember=>destinationMember.Titulo);
            CreateMap<ActividadDTO, Actividades>();
            CreateMap<Departamentos,DepartamentoDTO>();
            CreateMap<DepartamentoDTO, Departamentos>();
        }

    }
}
