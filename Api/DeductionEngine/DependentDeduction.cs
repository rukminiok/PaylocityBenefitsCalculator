using Api.Dtos.Employee;

namespace Api.DeductionEngine
{
    public class DependentDeduction : IDeduction
    {
        DependentDeductionConfig _configuration;

        public DependentDeduction(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("Deductions").Get<DeductionConfig>().DependentDeduction;
        }
        /// <summary>
        /// This is to calculate and implement dependent's base deduction - rule
        /// </summary>
        /// <param name="employeeDetails"></param>
        /// <param name="startPayPeriod"></param>
        /// <param name="endPayPeriod"></param>
        /// <param name="payCheckPerPeriod"></param>
        /// <returns></returns>
        public async Task Execute(GetEmployeeDto employeeDetails, DateTime startPayPeriod, DateTime endPayPeriod, GetPayCheckPerPeriodDto payCheckPerPeriod)
        {
            if ((_configuration.DeductionApplied == Applied.Yearly) & (employeeDetails.Dependents.Count >0))
                     payCheckPerPeriod.Deductions.Add("DependentDeduction", Math.Round((_configuration.AmountDeducted * employeeDetails.Dependents.Count) / 26,2));
            

        }
    }
}
