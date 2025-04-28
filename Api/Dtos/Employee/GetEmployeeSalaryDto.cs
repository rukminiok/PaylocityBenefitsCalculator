namespace Api.Dtos.Employee
{
    public class GetEmployeeSalaryDto
    {
        public string JobDescription { get; set; }

        public decimal Salary { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; } = null;
    }
}
