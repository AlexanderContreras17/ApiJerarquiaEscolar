using Microsoft.AspNetCore.Http;
using AutoMapper;
using ApiJerarquia.Helpers;
using ApiJerarquia.Models.DTOs;
using ApiJerarquia.Models.Entities;
using ApiJerarquia.Models.Validators;
using ApiJerarquia.Repositories;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ApiJerarquia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly DepartamentosRepository _repository;
        private readonly ActividadesRepository _actividadesRepository;
        private readonly IMapper _mapper;
        public DepartamentoController(DepartamentosRepository repositoryDepartamento, IMapper mapper, ActividadesRepository actividadesRepository)
        {
            _repository = repositoryDepartamento;
            _mapper = mapper;
            _actividadesRepository = actividadesRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var departamentos= _repository.GetAll()
                .Select(x=> new DepartamentoDTO 
                {
                    Id= x.Id,
                    Nombre= x.Nombre,
                    DepartamentoSuperior=x.IdSuperiorNavigation!=null? x.IdSuperiorNavigation.Nombre:null
                });
            return Ok(departamentos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var departamento = _repository.Get(id);
            if (departamento != null)
            {
                var departamentoDto=_mapper.Map<DepartamentoDTO>(departamento);
                return Ok(departamentoDto);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
