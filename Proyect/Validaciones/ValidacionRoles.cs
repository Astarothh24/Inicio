using FluentValidation;
using Proyect.Models;

namespace Proyect.Validaciones
{
    public class ValidacionRoles : AbstractValidator<Role>
    {
        public ValidacionRoles() 
        {
            RuleFor(x => x.NomRol)
                .NotEmpty().WithMessage("El nombre del rol es obligatorio.")
                .Length(3, 50).WithMessage("El nombre del rol debe tener entre 3 y 50 caracteres.");
        }
    }
}
