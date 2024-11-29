using FluentValidation;
using Proyect.Models;

namespace Proyect.Validaciones
{
    public class ValidacionPermiso : AbstractValidator<Permiso>
    {
        public ValidacionPermiso() 
        {
            RuleFor(x => x.NomPermiso)
           .NotEmpty().WithMessage("El nombre del permiso es obligatorio.")
           .Length(5, 20).WithMessage("El nombre del permiso debe tener entre 5 y 20 caracteres.");
        }
    }
}
