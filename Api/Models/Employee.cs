namespace Api.Models;

public class Employee
{
    public Int64 Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? SSN { get; set; }
    public string? EmploymentID { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();
    public ICollection<EmployeeJobDetail> EmployeeJobDetails { get; set; } = new List<EmployeeJobDetail>();
    public bool Active { get; set; }
    public DateTime CreatedOn { get; set; }
    public long CreatedBy { get; set; }
    public DateTime LastEditDate { get; set; }
    public long LastEditBy { get; set; }
}
