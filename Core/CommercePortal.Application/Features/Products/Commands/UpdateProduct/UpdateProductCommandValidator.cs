﻿using CommercePortal.Application.Common.Validators;
using FluentValidation;

namespace CommercePortal.Application.Features.Products.Commands.UpdateProduct;

/// <summary>
/// Validator for the <see cref="UpdateProductCommandRequest"/> class.
/// </summary>
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
                .WithMessage("Id is required.");

        RuleFor(x => x.Name)
            .MaximumLength(100)
            .MinimumLength(3)
                .WithMessage("Name must be between 3 and 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
                .WithMessage("Description must be less than 500 characters.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
                .WithMessage("Stock must be greater than or equal to 0.");

        RuleFor(x => x.StandardPrice)
            .SetValidator(new MoneyValidator()!)
            .When(x => x.StandardPrice != null);

        RuleFor(x => x.CategoryIds)
            .Must(x => x.Count > 0)
                .WithMessage("At least one category is required.");
    }
}