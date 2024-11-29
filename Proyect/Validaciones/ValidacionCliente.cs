using FluentValidation;
using Proyect.Models;

namespace Proyect.Validaciones
{
    public class ValidacionCliente : AbstractValidator<Cliente>
    {
        public ValidacionCliente() 
        {
            RuleFor(x => x.IdTipoDocumento)
                .NotEmpty().WithMessage("Debe seleccionar un tipo de documento");

            RuleFor(x => x.NroDocumento)
                .NotEmpty().WithMessage("El documento es obligatorio.")
                .Matches(@"^\d+$").WithMessage("El documento solo puede contener números.");

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
        }
    }
}
