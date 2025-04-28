namespace Api.DeductionEngine
{
    public enum Applied
    {
        Yearly
    }
    public class DeductionConfig
    {
        public DependentDeductionConfig DependentDeduction { get; set; }
        public DependentDeductionByAgeConfig DependentDeductionByAge { get; set; }
        public EmployeeBaseDeductionConfig EmployeeBaseDeduction { get; set; }
        public EmployeeWithHigherSalaryDeductionConfig EmployeeWithHigherSalaryDeduction { get; set; }
    }

    public class DependentDeductionConfig
    {
        public decimal AmountDeducted { get; set; }
        public Applied DeductionApplied { get; set; }
    }

    public class DependentDeductionByAgeConfig
    {
        public int Age { get; set; }
        public decimal AmountDeducted { get; set; }
        public Applied DeductionApplied { get; set; }
    }

    public class EmployeeBaseDeductionConfig
    {
        public decimal AmountDeducted { get; set; }
        public Applied DeductionApplied { get; set; }
    }

    public class EmployeeWithHigherSalaryDeductionConfig
    {
        public decimal AmountDeductedPercent { get; set; }
        public decimal SalaryThreshold { get; set; }
        public Applied DeductionApplied { get; set; }
    }
}
