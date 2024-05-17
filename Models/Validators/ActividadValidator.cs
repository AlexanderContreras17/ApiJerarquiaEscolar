using ApiJerarquia.Models.DTOs;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ApiJerarquia.Models.Validators
{
    public class ActividadValidator
    {
        public static FluentValidation.Results.ValidationResult Validate(ActividadCreateDTO Actividad)
        {
            var validator = new InlineValidator<ActividadCreateDTO>();
            validator.RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("Se necesita el titulo de la actividad");
            validator.RuleFor(x => x.Departamento)
                .NotEmpty()
                .WithMessage("Se necesita el departamento al que pertenece la actividad");
            validator.RuleFor(x => x.Estado)
                .NotEmpty()
                .WithMessage("Se necesita el estado");
            validator.RuleFor(x => x.Estado)
                .GreaterThan(0)
                .LessThan(3)
                .WithMessage("El estado no existe");

            //return validator.Validate(Actividad);
            return validator.Validate(Actividad);
        }
    }
}
