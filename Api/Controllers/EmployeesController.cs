using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    BenefitService _service;
    public EmployeesController(IConfiguration configuration, BenefitService service)
    {
        _service = service;
    }
    /// <summary>
    /// To return EmployeeDto for a given employee id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(Int64 id)  
    {
        try
        {
            GetEmployeeDto employee = await _service.GetEmployeeById(id);
            if (employee.Id == 0) { return NotFound(); }
            return new ApiResponse<GetEmployeeDto>()
            {
                Data = employee,
                Success = true
            };
        }
        catch(Exception ex)
        {
            return BadRequest(new ApiResponse<GetEmployeeDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
        
    }
    /// <summary>
    /// This is to return employee dto for all the employees as list
    /// </summary>
    /// <returns></returns>
    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        try
        {
            var employees = await _service.GetAllEmployees();
            if (employees.Count == 0) { return NotFound(); }
            var result = new ApiResponse<List<GetEmployeeDto>>
            {
                Data = employees,
                Success = true
            };
            return result;
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<List<GetEmployeeDto>>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }
    /// <summary>
    /// This is to return 26 pay checks
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    /// <exception cref="Microsoft.AspNetCore.Http.BadHttpRequestException"></exception>
        [SwaggerOperation(Summary = "Get all 26 paychecks for employee")]
        [HttpGet("paycheck")]
        public async Task<ActionResult<ApiResponse<GetEmployeePayCheckDto>>> GetPayCheckForEmployee(Int64 employeeId, int year)
        {
            try
            {
               if (year < 1980 || year > DateTime.Now.Year)
                 throw new Microsoft.AspNetCore.Http.BadHttpRequestException($"Year needs to between 1980 and {DateTime.Now.Year}");

            if (employeeId <=0)
                throw new Microsoft.AspNetCore.Http.BadHttpRequestException($"Not a valid Employee Id {employeeId}");


            

                var employees = await _service.GetEmployeePayChecks(employeeId, year);

                var result = new ApiResponse<GetEmployeePayCheckDto>
                {
                    Data = employees,
                    Success = true
                };

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<GetEmployeePayCheckDto>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
}
