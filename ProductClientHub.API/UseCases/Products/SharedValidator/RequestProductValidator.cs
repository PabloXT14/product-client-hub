using FluentValidation;
using ProductClientHub.Communication.Requests;

namespace ProductClientHub.API.UseCases.Products.SharedValidator;

public class RequestProductValidator : AbstractValidator<RequestProductJson>
{
    public RequestProductValidator()
    {
        RuleFor(product => product.Name).NotEmpty().WithMessage("O nome do produto é obrigatório.");
        RuleFor(product => product.Brand).NotEmpty().WithMessage("A marca do produto é obrigatória.");
        RuleFor(product => product.Price).GreaterThan(0).WithMessage("O preço do produto deve ser maior que 0.");
    }
}