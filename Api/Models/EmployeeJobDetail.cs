namespace Api.Models
{
    public class EmployeeJobDetail
    {
        public Int64 EmployeeJobDetailID { get; set; }

        public Int64 EmployeeId { get; set; }

        public string? JobDescription { get; set; } = null;

        public decimal BasicSalary { get; set; }
        public decimal DefaultDeduction { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; } = null;
         public DateTime CreatedOn { get; set; }
         public long CreatedBy { get; set; }
         public DateTime LastEditDate { get; set; }
         public long LastEditBy { get; set; }
    }
}
