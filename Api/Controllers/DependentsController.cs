using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    BenefitService _service;
    public DependentsController(IConfiguration configuration, BenefitService service)
    {
        _service = service;
    }
    /// <summary>
    /// This is to return dependent dto for a given dependent ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(Int64 id)
    {
        try
        {
            GetDependentDto dependent = await _service.GetDependentById(id);

            if (dependent.Id == 0) { return NotFound(); }
            return new ApiResponse<GetDependentDto>()
            {
                Data = dependent,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<GetDependentDto>
            {
                Success = false,
                Message = ex.Message
            });
        }

    }
    /// <summary>
    /// This is to return all the dependents as list of dependentdto
    /// </summary>
    /// <returns></returns>
    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        try
        {
            var dependents = await _service.GetAllDependents();
            if (dependents.Count == 0) { return NotFound(); }
            return new ApiResponse<List<GetDependentDto>>()
            {
                Data = dependents,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<List<GetDependentDto>>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

 }
