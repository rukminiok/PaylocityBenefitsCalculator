namespace Api.Dtos.Employee
{
    public class GetEmployeePayCheckDto
    {
        public Int64 EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string SSN { get; set; }

        public string EmploymentId { get; set; }

        public List<GetPayCheckPerPeriodDto> PayCheckPerPeriod { get; set; }

        public GetEmployeePayCheckDto()
        {
            PayCheckPerPeriod = new List<GetPayCheckPerPeriodDto>();
        }
    }
    public class GetPayCheckPerPeriodDto
    {
        public string PayPeriod { get; set; } 

        public decimal BaseSalary { get; set; }

        public Dictionary<string, decimal> Deductions { get; set; }=new Dictionary<string, decimal>();

        public decimal NetSalary { get; set; } // BaseSalary - Deductions

        public GetPayCheckPerPeriodDto()
        {
            Deductions = new Dictionary<string, decimal>();
        }
    }
}
