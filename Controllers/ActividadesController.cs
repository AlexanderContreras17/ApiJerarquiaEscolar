using ApiJerarquia.Models.DTOs;
using ApiJerarquia.Models.Validators;
using ApiJerarquia.Repositories;
using AutoMapper;
using ApiJerarquia.Models.Entities;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiJerarquia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadesController : ControllerBase
    {
        private readonly ActividadesRepository _repository;
        private readonly IMapper mapper;
        public ActividadesController(ActividadesRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }
        [HttpGet("Publicadas")]
        public IActionResult GetActividadPublicada()
        {
            var actividades = _repository.GetActividades()?
                .OrderBy(x => x.Titulo)
                .Select(x => new ActividadDTO
            {
                Id = x.Id,
                Titulo = x.Titulo,
                Departamento = x.IdDepartamentoNavigation.Nombre,
                Descripcion = x.Descripcion,
                FechaActualizacion = x.FechaActualizacion,
                FechaCreacion = x.FechaCreacion,
                FechaRealizacion = x.FechaRealizacion
            });
            return Ok(actividades);
        }
        [HttpGet("Borradores")]
        public IActionResult GetBorradores()
        {
            var actividades = _repository.GetBorradores()?
                .OrderBy(x => x.Titulo)
                .Select(x => new ActividadDTO
                {
                    Id = x.Id,
                    Titulo = x.Titulo,
                    Departamento = x.IdDepartamentoNavigation.Nombre,
                    Descripcion = x.Descripcion,
                    FechaActualizacion = x.FechaActualizacion,
                    FechaCreacion = x.FechaCreacion,
                    FechaRealizacion = x.FechaRealizacion
                });
            return Ok(actividades);
        }
        [HttpGet ("Eliminadas")]
        public IActionResult GetActividadesEliminadas()
        {
            var actividades = _repository.GetBorradores()?
               .OrderBy(x => x.Titulo)
               .Select(x => new ActividadDTO
               {
                   Id = x.Id,
                   Titulo = x.Titulo,
                   Departamento = x.IdDepartamentoNavigation.Nombre,
                   Descripcion = x.Descripcion,
                   FechaActualizacion = x.FechaActualizacion,
                   FechaCreacion = x.FechaCreacion,
                   FechaRealizacion = x.FechaRealizacion
               });
            return Ok(actividades);
        }
        [HttpGet("{id}")]
        public IActionResult GetActividad(int id)
        {
            var actividad = _repository.Get(id);
            if (actividad != null)
            {
                var actividadDto = mapper.Map<ActividadDTO>(actividad);
                return Ok(actividadDto);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult Agregar(ActividadCreateDTO actividad)
        {
            if(actividad != null)
            {
                ValidationResult validate = ActividadValidator.Validate(actividad);
                if (validate.IsValid)
                {
                    var actividadAdd = new Actividades
                    {
                        Descripcion = actividad.Descripcion,
                        Titulo = actividad.Titulo,
                        IdDepartamento = actividad.Departamento,
                        FechaCreacion = actividad.FechaCreacion,
                        FechaRealizacion = actividad.FechaRealizacion,
                        FechaActualizacion = actividad.FechaActualizacion,
                        Estado = 1
                    };
                    _repository.Insert(actividadAdd);
                    return Ok(actividadAdd);
                }
                else
                {
                    return BadRequest(validate.Errors
                        .Select(x => x.ErrorMessage));
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult Editar(ActividadCreateDTO actividad)
        {
            ValidationResult validate= ActividadValidator.Validate(actividad);
            if (validate.IsValid)
            {
                var actividadEditar = _repository.Get(actividad.Id ?? 0);

                if( actividadEditar != null)
                {
                    actividadEditar.Titulo=actividad.Titulo;
                    actividadEditar.Estado=actividad.Estado;
                    actividadEditar.Descripcion=actividad.Descripcion;
                    actividadEditar.IdDepartamento = actividad.Departamento;
                    actividadEditar.FechaActualizacion = DateTime.Now;
                    actividadEditar.FechaRealizacion = actividad.FechaRealizacion;
                    _repository.Update(actividadEditar);
                    return Ok(actividadEditar);
                }
                else
                {
                    return NotFound();
                }
               

            }
            else
            {
                return BadRequest(validate.Errors.Select(x=>x.ErrorMessage));
            }

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var actividadDelete=_repository.Get(id);
            if(actividadDelete != null)
            {
                actividadDelete.Estado = 2;
                _repository.Update(actividadDelete); return Ok(actividadDelete);

            }
            else
            {
                return NotFound();
            }
        }
    }
}
