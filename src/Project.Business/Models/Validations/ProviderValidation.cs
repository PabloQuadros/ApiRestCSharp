using FluentValidation;
using Project.Business.Models;
using Project.Business.Models.Validations.Documents;

namespace Project.Business.Models.Validations;

public class ProviderValidation: AbstractValidator<Provider>
{
    public ProviderValidation()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 100)
            .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        When(p => p.ProviderType == ProviderType.NaturalPerson, () =>
        {
            RuleFor(p => p.Document.Length).Equal(CpfValidation.CpfLength)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
            RuleFor(p=> CpfValidation.Validate(p.Document)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");
        });

        When(p => p.ProviderType == ProviderType.LegalEntity, () =>
        {
            RuleFor(p => p.Document.Length).Equal(CnpjValidation.CnpjLength)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
            RuleFor(p => CnpjValidation.Validate(p.Document)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");
        });
    }
}