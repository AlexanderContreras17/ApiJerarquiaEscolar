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
    }
}
