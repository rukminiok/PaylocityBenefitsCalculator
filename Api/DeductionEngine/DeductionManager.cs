using Api.Dtos.Employee;

namespace Api.DeductionEngine
{
   
        public class DeductionManager
        {
            List<IDeduction> _deductionRules = new List<IDeduction>();
            public DeductionManager(IConfiguration configuration)
            {

                _deductionRules.Add(new EmployeeBaseDeduction(configuration));
                _deductionRules.Add(new DependentDeductionByAge(configuration));
                _deductionRules.Add(new EmployeeWithHigherSalaryDeduction(configuration));
                _deductionRules.Add(new DependentDeduction(configuration));
            }
        /// <summary>
        /// Calculating Net Salary
        /// </summary>
        /// <param name="employeeDetails"></param>
        /// <param name="startPayPeriod"></param>
        /// <param name="endPayPeriod"></param>
        /// <param name="payCheckPerPeriod"></param>
        /// <returns></returns>
            public async Task RunDeductions(GetEmployeeDto employeeDetails, DateTime startPayPeriod, DateTime endPayPeriod, GetPayCheckPerPeriodDto payCheckPerPeriod)
            {
            try
            {

                foreach (var deductionRule in _deductionRules)
                {
                    await deductionRule.Execute(employeeDetails, startPayPeriod, endPayPeriod, payCheckPerPeriod);
                }

                if (payCheckPerPeriod.Deductions.Any())
                {
                    payCheckPerPeriod.NetSalary = Math.Round ( payCheckPerPeriod.BaseSalary - payCheckPerPeriod.Deductions.Values.Sum(),2);
                }
                else
                {
                    payCheckPerPeriod.NetSalary = Math.Round ( payCheckPerPeriod.BaseSalary,2);

                }

            }
            catch  (Exception ex)
            { throw; }

            }
        }
    }

