using Api.Dtos.Employee;
using Microsoft.Extensions.Configuration;

namespace Api.DeductionEngine
{
    public class DependentDeductionByAge : IDeduction
    {
        DependentDeductionByAgeConfig _configuration;

        public DependentDeductionByAge(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("Deductions").Get<DeductionConfig>().DependentDeductionByAge; ;
        }
        /// <summary>
        /// This is to calculate when the Dependent age > 50 years, to include the deduction rule
        /// </summary>
        /// <param name="employeeDetails"></param>
        /// <param name="startPayPeriod"></param>
        /// <param name="endPayPeriod"></param>
        /// <param name="payCheckPerPeriod"></param>
        /// <returns></returns>
        public async Task Execute(GetEmployeeDto employeeDetails, DateTime startPayPeriod, DateTime endPayPeriod,GetPayCheckPerPeriodDto payCheckPerPeriod)
        {
            // implement employee base deduction 

            if (employeeDetails.Dependents.Any(x => x.DateOfBirth.Year + _configuration.Age > DateTime.Now.Year))
            {
                var countDependentsAbove50 = employeeDetails.Dependents.Where(x => x.DateOfBirth.Year + _configuration.Age > DateTime.Now.Year).Count();

                if (_configuration.DeductionApplied == Applied.Yearly)
                    payCheckPerPeriod.Deductions.Add("DependentsDeductionByAge", Math.Round ( (countDependentsAbove50 * _configuration.AmountDeducted) / 26,2));
            }
        }
    }
}
