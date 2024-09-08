using DTO.DTO.Request;
using FluentValidation;
using System.Globalization;

namespace BCuentas.Helpers.Validators
{
    public class EstadoCuentaRequestDtoValidator : AbstractValidator<EstadoCuentaRequestDto>
    {
        public EstadoCuentaRequestDtoValidator()
        {
            RuleFor(x => x.TarjetaId)
                .GreaterThan(0).WithMessage("El 'TarjetaId' debe ser mayor que 0.");

            RuleFor(x => x.FechaInicio)
                .LessThan(x => x.FechaFin).WithMessage("La 'FechaInicio' debe ser anterior a la 'FechaFin'.");

            RuleFor(x => x.FechaInicio)
                .NotEqual(DateTime.MinValue).WithMessage("La 'FechaInicio' no es válida.");

            RuleFor(x => x.FechaFin)
                .NotEqual(DateTime.MinValue).WithMessage("La 'FechaFin' no es válida.");
        }
    }
}
