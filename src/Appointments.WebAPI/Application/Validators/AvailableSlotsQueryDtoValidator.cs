using Appointments.WebAPI.Models.DTOs;
using FluentValidation;

namespace Appointments.WebAPI.Application.Validators;

public class AvailableSlotsQueryDtoValidator : AbstractValidator<AvailableSlotsQueryDto>
{
    private static readonly string[] AllowedProducts = ["SolarPanels", "Heatpumps"];
    private static readonly string[] AllowedLanguages = ["German", "English"];
    private static readonly string[] AllowedRatings = ["Gold", "Silver", "Bronze"];

    public AvailableSlotsQueryDtoValidator()
    {
        RuleFor(q => q.Date)
            .NotEmpty().WithMessage("Date is required.");

        RuleFor(q => q.Products)
            .NotEmpty().WithMessage("At least one product must be specified.")
            .Must(products => products.All(p => AllowedProducts.Contains(p)))
            .WithMessage($"Products must be one of: {string.Join(", ", AllowedProducts)}");

        RuleFor(q => q.Language)
            .NotEmpty().WithMessage("Language is required.")
            .Must(lang => AllowedLanguages.Contains(lang))
            .WithMessage($"Language must be one of: {string.Join(", ", AllowedLanguages)}");

        RuleFor(q => q.Rating)
            .NotEmpty().WithMessage("Customer rating is required.")
            .Must(rating => AllowedRatings.Contains(rating))
            .WithMessage($"Rating must be one of: {string.Join(", ", AllowedRatings)}");
    }
}