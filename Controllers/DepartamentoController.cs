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
        [HttpPost]
        public IActionResult Agregar(DepartamentoCreateDTO dto)
        {
            if(dto != null)
            {
            ValidationResult validate = DepartamentoValidator.Validate(dto, _repository.Context);
               if(validate.IsValid)
                {
                    var departamento = new Departamentos
                    {
                        Nombre = dto.Nombre,
                        Password = Encriptacion.StringToSHA512(dto.Contraseña),
                        Username = dto.Usuario,
                        IdSuperior = dto.IdSuperior
                    };
                    _repository.Insert(departamento);
                    return Ok(departamento);
                }
                else
                {
                    return BadRequest(validate.Errors.Select(x=>x.ErrorMessage));
                }
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]
        public IActionResult Editar(DepartamentoCreateDTO dto)
        {
                ValidationResult validate = DepartamentoValidator.Validate(dto, _repository.Context);

            if (validate.IsValid)
            {
                var departamento = _repository.Get(dto.Id ?? 0);

                if (departamento != null)

                {
                    departamento.Nombre = dto.Nombre;
                    departamento.Password = Encriptacion
                        .StringToSHA512(dto.Contraseña);
                    departamento.IdSuperior = dto.IdSuperior;


                    _repository.Insert(departamento);
                    return Ok(departamento);
                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                return BadRequest(validate.Errors.Select(x => x.ErrorMessage));
            }
           
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var departamento=_repository.Get(id);
            var actividadDepartamento =
                _actividadesRepository.GetActividadesporDepartamento(id)?.ToList();
            if (actividadDepartamento != null)
            {
                foreach(var actividad in actividadDepartamento)
                {
                    _actividadesRepository.Delete(actividad);
                }
            }
            if(departamento!=null)
            {
                departamento.IdSuperior = null;
                _repository.Update(departamento);
                _repository.Delete(departamento);
                return Ok("Departamento eliminado");
            }
            else
            {
                NotFound();
            }
        }
    }
}
