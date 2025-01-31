using Appointments.WebAPI.Application.Validators;
using Appointments.WebAPI.Models.DTOs;
using FluentValidation.TestHelper;

namespace Appointments.Tests;

public class AvailableSlotsQueryDtoValidatorTests
{
    private readonly AvailableSlotsQueryDtoValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_DateIsEmpty()
    {
        // Arrange
        var model = new AvailableSlotsQueryDto { Date = default };
        
        // Act
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Date)
              .WithErrorMessage("Date is required.");
    }

    [Fact]
    public void Should_HaveError_When_ProductsAreEmpty()
    {
        // Arrange
        var model = new AvailableSlotsQueryDto { Products = [] };
        
        // Act
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Products)
              .WithErrorMessage("At least one product must be specified.");
    }

    [Fact]
    public void Should_HaveError_When_ProductIsInvalid()
    {
        // Arrange
        var model = new AvailableSlotsQueryDto { Products = ["InvalidProduct"] };
        
        // Act
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Products)
              .WithErrorMessage("Products must be one of: SolarPanels, Heatpumps");
    }

    [Fact]
    public void Should_HaveError_When_LanguageIsEmpty()
    {
        // Arrange
        var model = new AvailableSlotsQueryDto { Language = "" };
        
        // Act
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Language)
              .WithErrorMessage("Language is required.");
    }

    [Fact]
    public void Should_HaveError_When_LanguageIsInvalid()
    {
        // Arrange
        var model = new AvailableSlotsQueryDto { Language = "French" };
        
        // Act
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Language)
              .WithErrorMessage("Language must be one of: German, English");
    }

    [Fact]
    public void Should_HaveError_When_RatingIsEmpty()
    {
        // Arrange
        var model = new AvailableSlotsQueryDto { Rating = "" };
        
        // Act
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Rating)
              .WithErrorMessage("Customer rating is required.");
    }

    [Fact]
    public void Should_HaveError_When_RatingIsInvalid()
    {
        // Arrange
        var model = new AvailableSlotsQueryDto { Rating = "Platinum" };
        
        // Act
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Rating)
              .WithErrorMessage("Rating must be one of: Gold, Silver, Bronze");
    }

    [Fact]
    public void Should_Pass_When_AllFieldsAreValid()
    {
        // Arrange
        var model = new AvailableSlotsQueryDto
        {
            Date = DateTime.UtcNow.AddDays(1),
            Products = ["SolarPanels"],
            Language = "German",
            Rating = "Gold"
        };
        
        // Act
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}