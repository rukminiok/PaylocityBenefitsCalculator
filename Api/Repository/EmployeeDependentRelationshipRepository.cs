using Api.Models;
using System.Data.SqlClient;
using System.Data;
using System;

namespace Api.Repository
{
    public class EmployeeDependentRelationshipRepository
    {
        private string _connectionString;
        public EmployeeDependentRelationshipRepository(IConfiguration _configuration)
        {

            _connectionString = _configuration.GetConnectionString("DefaultConnection");

        }

        /// <summary>
        /// Used for inserting EmployeeDependentRelationship
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="dependentId"></param>
        /// <param name="relationship"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task InsertEmployeeDependentRelationship(int employeeId, int dependentId, Relationship relationship)
        {
           throw new NotImplementedException();
        }

        /// <summary>
        /// To get the list of dependents for the given employee 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<List<Dependent>> GetDependents(Int64 employeeId)
        {
            List<Dependent> dependents = new List<Dependent>();

            var parameters = new Dictionary<string, object>
                {
                    { "@Employee_ID", employeeId }
                };
            DataSet dsDependent = await Utils.ExecuteStoredProcedureToGetValues(this._connectionString, "GetDependentsByEmployeeID", parameters);

            
            foreach (DataRow reader in dsDependent.Tables[0].Rows)
            {
                Dependent dependent = new Dependent();
                dependent.DependentId = Utils.ConvertColumnValue<Int64>(reader, "Dependent_ID");
                dependent.FirstName = Utils.ConvertColumnValue<String>(reader, "First_Name");
                dependent.MiddleName = Utils.ConvertColumnValue<String>(reader, "Middle_Name");
                dependent.LastName = Utils.ConvertColumnValue<String>(reader, "Last_Name");
                dependent.DateOfBirth = Utils.ConvertColumnValue<DateTime>(reader, "Date_Of_Birth");
                dependent.Gender = Utils.ConvertColumnValue<String>(reader, "Gender");
                dependent.SSN = Utils.ConvertColumnValue<String>(reader, "SSN");
                dependent.Active = Utils.ConvertColumnValue<Boolean>(reader, "Active");
                dependent.CreatedOn = Utils.ConvertColumnValue<DateTime>(reader, "Created_On");
                dependent.CreatedBy = Utils.ConvertColumnValue<Int64>(reader, "Created_By");
                dependent.LastEditDate = Utils.ConvertColumnValue<DateTime>(reader, "Last_Edit_Date");
                dependent.LastEditBy = Utils.ConvertColumnValue<Int64>(reader, "Last_Edit_By");
                if (Enum.TryParse<Relationship>(reader["Relationship_Type"].ToString(), true, out Relationship relationship)) { dependent.Relationship = relationship ; }
                dependents.Add(dependent);
            }
            return dependents;
        }

            
        
    }
}
