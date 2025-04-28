using Api.Dtos.Employee;

namespace Api.DeductionEngine
{
    public class EmployeeBaseDeduction : IDeduction
    {
        EmployeeBaseDeductionConfig _configuration;

        public EmployeeBaseDeduction(IConfiguration configuration)
        {

            _configuration = configuration.GetSection("Deductions").Get<DeductionConfig>().EmployeeBaseDeduction;
        }
        /// <summary>
        /// This is to calculate Employee base deduction - rule
        /// </summary>
        /// <param name="employeeDetails"></param>
        /// <param name="startPayPeriod"></param>
        /// <param name="endPayPeriod"></param>
        /// <param name="payCheckPerPeriod"></param>
        /// <returns></returns>
        public async Task Execute(GetEmployeeDto employeeDetails, DateTime startPayPeriod, DateTime endPayPeriod, GetPayCheckPerPeriodDto payCheckPerPeriod)
        {
            // implement employee base deduction 

            if (_configuration.DeductionApplied == Applied.Yearly)
                payCheckPerPeriod.Deductions.Add("EmployeeBaseDeduction",Math.Round ( _configuration.AmountDeducted / 26,2));

        }
    }
}
