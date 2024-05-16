using ApiJerarquia.Models.DTOs;
using ApiJerarquia.Models.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace ApiJerarquia.Models.Validators
{
    public class DepartamentoValidator
    {
        public static ValidationResult
            Validate(DepartamentoCreateDTO departamento,
            ItesrcneActividadesContext context)
        {
            var validator = new InlineValidator<DepartamentoCreateDTO>();
            validator.RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre del departamento no puede estar vacío")
                .Must((dto,nombre)=>ExisteDepartamento(nombre,context))
                .WithMessage("El nombre de usuario no puede estar vacío")
                .Must((dto,usuario)=>ExisteUsuario(usuario,context))
                .WithMessage("Ya existe un departamento con este nombre de usuario")
                ;
            validator.RuleFor(x => x.Contraseña).NotEmpty().WithMessage("La contraeña no puede estar vacia");
            return validator.Validate(departamento);

        }

        private static bool ExisteUsuario(string usuario, ItesrcneActividadesContext context)
        {
            return !context.Departamentos.Any(d => d.Username == usuario);
        }

        private static bool ExisteDepartamento(string nombre, ItesrcneActividadesContext context)
        {
           return !context.Departamentos.Any(d=>d.Username == nombre);
        }
    }
}
