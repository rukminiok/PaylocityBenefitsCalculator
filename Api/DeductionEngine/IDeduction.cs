using Api.Dtos.Employee;
namespace Api.DeductionEngine
{
    public interface IDeduction
    {
        Task Execute(GetEmployeeDto employeeDetails, DateTime startPayPeriod, DateTime endPayPeriod, GetPayCheckPerPeriodDto payCheckPerPeriod);
    }
}
