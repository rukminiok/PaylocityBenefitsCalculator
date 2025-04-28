using Api.Dtos.Dependent;

namespace Api.Dtos.Employee;

public class GetEmployeeDto
{
    public Int64 Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<GetEmployeeSalaryDto> SalaryDetail { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<GetDependentDto> Dependents { get; set; } = new List<GetDependentDto>();
}
