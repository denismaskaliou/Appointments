using Appointments.WebAPI.Application.Services;
using Appointments.WebAPI.Models.DTOs;
using Appointments.WebAPI.Web.Controllers;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Appointments.Tests;

public class AvailableSlotsControllerTests
{
    private readonly AvailableSlotsController _controller;
    private readonly Mock<IAppointmentSlotsService> _mockService;
    private readonly Mock<IValidator<AvailableSlotsQueryDto>> _mockValidator;

    public AvailableSlotsControllerTests()
    {
        _mockService = new Mock<IAppointmentSlotsService>();
        _mockValidator = new Mock<IValidator<AvailableSlotsQueryDto>>();
        var mockLogger = new Mock<ILogger<AvailableSlotsController>>();

        _controller = new AvailableSlotsController(mockLogger.Object, _mockValidator.Object, _mockService.Object);
    }

    [Fact]
    public async Task GetAvailableSlotsAsync_ReturnsOk_WhenValidRequest()
    {
        // Arrange
        var query = new AvailableSlotsQueryDto
        {
            Date = DateTime.UtcNow.AddDays(1),
            Products = ["SolarPanels"],
            Language = "German",
            Rating = "Gold"
        };
        var expectedSlots = new[] { new AvailableSlotDto() };

        _mockValidator.Setup(v => v.ValidateAsync(query, default))
            .ReturnsAsync(new ValidationResult());

        _mockService.Setup(s => s.GetAvailableSlotsAsync(query))
            .ReturnsAsync(expectedSlots);

        // Act
        var result = await _controller.GetAvailableSlotsAsync(query);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(expectedSlots, okResult.Value);
    }

    [Fact]
    public async Task GetAvailableSlotsAsync_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        var query = new AvailableSlotsQueryDto();
        var validationFailures = new List<ValidationFailure> { new("Date", "Date is required.") };

        _mockValidator.Setup(v => v.ValidateAsync(query, default))
            .ReturnsAsync(new ValidationResult(validationFailures));

        // Act
        var result = await _controller.GetAvailableSlotsAsync(query);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var errorDto = Assert.IsType<ErrorDto>(badRequestResult.Value);
        Assert.Contains("Date: Date is required.", errorDto.Details);
    }

    [Fact]
    public async Task GetAvailableSlotsAsync_ReturnsServerError_WhenExceptionOccurs()
    {
        // Arrange
        var query = new AvailableSlotsQueryDto
        {
            Date = DateTime.UtcNow.AddDays(1),
            Products = ["SolarPanels"],
            Language = "German",
            Rating = "Gold"
        };
        var exception = new Exception("Service failure");

        _mockValidator.Setup(v => v.ValidateAsync(query, default))
            .ReturnsAsync(new ValidationResult());

        _mockService.Setup(s => s.GetAvailableSlotsAsync(query))
            .ThrowsAsync(exception);

        // Act
        var result = await _controller.GetAvailableSlotsAsync(query);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, objectResult.StatusCode);
        var errorDto = Assert.IsType<ErrorDto>(objectResult.Value);
        Assert.Equal("An unexpected error occurred.", errorDto.Message);
        Assert.Equal("Service failure", errorDto.Details);
    }
}