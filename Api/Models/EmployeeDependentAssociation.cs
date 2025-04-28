namespace Api.Models
{
    public class EmployeeDependentAssociation
    {
        public Int64 EmployeeDependentAssociationID {get; set; }
        public Int64 EmployeeeID { get; set; }
        public Int64 DependentID { get;  set; }
        public int RelationshipID { get;  set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime LastEditDate { get; set; }
        public long LastEditBy { get; set; }
    }
}
