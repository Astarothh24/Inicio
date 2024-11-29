using FluentValidation;
using Proyect.Models;

namespace Proyect.Validaciones
{
    public class ValidacionUsuario : AbstractValidator<Usuario>
    {
        public ValidacionUsuario() 
        {
            RuleFor(x => x.IdTipoDocumento)
                .NotEmpty().WithMessage("El tipo de documento es obligatorio.");

            RuleFor(x => x.Nombres)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .Length(2, 50).WithMessage("El nombre debe tener entre 2 y 50 caracteres.");

            RuleFor(x => x.Apellidos)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .Length(2, 50).WithMessage("El apellido debe tener entre 2 y 50 caracteres.");

            RuleFor(x => x.Celular)
                .Matches(@"^\d{10}$").WithMessage("El celular debe tener exactamente 10 dígitos.")
                .When(x => !string.IsNullOrEmpty(x.Celular));

            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El correo electrónico no es válido.");

            RuleFor(x => x.IdRol)
                .GreaterThan(0).WithMessage("Debe seleccionar un rol.");
        }
    }
}
