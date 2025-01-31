using Appointments.WebAPI.Application.Services;
using Appointments.WebAPI.Models.DTOs;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.WebAPI.Web.Controllers;

[ApiController]
[Route("calendar")]
public class AvailableSlotsController(
    ILogger<AvailableSlotsController> logger,
    IValidator<AvailableSlotsQueryDto> validator,
    IAppointmentSlotsService appointmentSlotsService) : ControllerBase
{
    [HttpPost("query")]
    public async Task<ActionResult<AvailableSlotDto[]>> GetAvailableSlotsAsync(AvailableSlotsQueryDto query)
    {
        var validationResult = await validator.ValidateAsync(query);
        if (!validationResult.IsValid)
        {
            return CreateBadRequestResult(validationResult);
        }

        try
        {
            var availableSlots = await appointmentSlotsService.GetAvailableSlotsAsync(query);
            return Ok(availableSlots);
        }
        catch (Exception ex)
        {
            return CreateServerErrorResult(ex);
        }
    }

    private ObjectResult CreateBadRequestResult(ValidationResult validationResult)
    {
        var error = new ErrorDto
        {
            Message = "An validation error occurred",
            Details = string.Join(
                Environment.NewLine,
                validationResult.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}")
            )
        };

        logger.LogWarning("{message}: {details}", error.Message, error.Details);

        return BadRequest(error);
    }

    private ObjectResult CreateServerErrorResult(Exception exception)
    {
        var error = new ErrorDto
        {
            Message = "An unexpected error occurred.",
            Details = exception.Message
        };

        logger.LogError(exception, "{message}: {details}", error.Message, error.Details);

        return StatusCode(500, error);
    }
}