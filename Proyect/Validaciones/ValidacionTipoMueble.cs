﻿using FluentValidation;
using Proyect.Models;

namespace Proyect.Validaciones
{
    public class ValidacionTipoMueble : AbstractValidator<TipoMueble>
    {
        public ValidacionTipoMueble() 
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del rol es obligatorio.")
                .Length(3, 50).WithMessage("El nombre del rol debe tener entre 3 y 50 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(200).WithMessage("La descripción no puede tener más de 200 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Descripcion));
        }
    }
}
