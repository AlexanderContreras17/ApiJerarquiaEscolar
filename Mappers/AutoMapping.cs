using ApiJerarquia.Models.Entities;
using AutoMapper;
using ApiJerarquia.Models.DTOs;
using Microsoft.Extensions.Options;
namespace ApiJerarquia.Mappers
    
{
    public class AutoMapping:Profile
    {
        public AutoMapping() 
        {
            CreateMap<Actividades, ActividadDTO>()
                .ForMember(destination=>destination.Titulo, opt=>opt.MapFrom(soruce=>soruce.Titulo))
                .ForMember(destination=>destination.Departamento, opt=>opt.MapFrom(source=>source.IdDepartamentoNavigation.Nombre))
                .ForMember(destination=>destination.Id,opt=>opt.MapFrom(source=>source.Id))
                .ForMember(destination=>destination.Descripcion, opt=>opt.MapFrom(source=>source.Descripcion))
                .ForMember(destination=>destination.FechaActualizacion,opt=>opt.MapFrom(source=>source.Descripcion))
                .ForMember(destination=>destination.FechaRealizacion, opt=>opt.MapFrom(source=>source.FechaRealizacion))
                .ForMember(destination=>destination.FechaCreacion,opt=>opt.MapFrom(source=>source.FechaCreacion));
            CreateMap<ActividadDTO, Actividades>();
            CreateMap<Departamentos,DepartamentoDTO>()
                .ForMember(x=>x.Id,opt=>opt.MapFrom(source=>source.Id))
                .ForMember(x=>x.Nombre,opt=>opt.MapFrom(source=>source.Nombre))
                .ForMember(x=>x.DepartamentoSuperior,opt=>opt.MapFrom(source=>source.IdSuperiorNavigation.Nombre));
            CreateMap<DepartamentoDTO, Departamentos>();
        }

    }
}
