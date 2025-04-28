
using Api.Dtos.Employee;
namespace Api.DeductionEngine
{
    public class EmployeeWithHigherSalaryDeduction : IDeduction
    {
        EmployeeWithHigherSalaryDeductionConfig _configuration;

        public EmployeeWithHigherSalaryDeduction(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("Deductions").Get<DeductionConfig>().EmployeeWithHigherSalaryDeduction;
        }
        /// <summary>
        /// This is to calculate and implement deduction when the employee with higher base salary >80k rule
        /// </summary>
        /// <param name="employeeDetails"></param>
        /// <param name="startPayPeriod"></param>
        /// <param name="endPayPeriod"></param>
        /// <param name="payCheckPerPeriod"></param>
        /// <returns></returns>
        public async Task Execute(GetEmployeeDto employeeDetails, DateTime startPayPeriod, DateTime endPayPeriod, GetPayCheckPerPeriodDto payCheckPerPeriod)
        {
            if (payCheckPerPeriod.BaseSalary*26 > _configuration.SalaryThreshold && _configuration.DeductionApplied == Applied.Yearly)
            {
                var additionalCostPerYear = (payCheckPerPeriod.BaseSalary*26) * _configuration.AmountDeductedPercent / 100;
                var additionalCostperPayPeriod = additionalCostPerYear / 26;

                payCheckPerPeriod.Deductions.Add("EmployeeWithHigherSalaryDeduction", Math.Round ( additionalCostperPayPeriod,2));  
            }
        }
    }
}
