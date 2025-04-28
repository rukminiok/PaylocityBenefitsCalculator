using Api.Models;
using System.Data.SqlClient;
using System.Data;
using Api.Dtos.Employee;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Repository
{
    public class EmployeeRepository
    {
        private string _connectionString;
        private DateTime dateTimeDef = default;  //If the database datetime value has null, setting the value null instead of datetime default value
        public EmployeeRepository(IConfiguration _configuration)
        {
            
            _connectionString = _configuration.GetConnectionString("DefaultConnection");

        }
        /// <summary>
        /// To get List of all the employees.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> GetEmployees()
        {
        
                List<Employee> employees = new List<Employee>();
                DataSet dsEmployee = await Utils.ExecuteStoredProcedureToGetValues(this._connectionString, "GetAllEmployees");
                if (dsEmployee.Tables[0].Rows.Count == 0)
                    throw new Exception("No employees exist.");

            foreach (DataRow reader in dsEmployee.Tables[0].Rows)
                {
                    Employee employee = new Employee();
                    employee.Id = Utils.ConvertColumnValue<Int64>(reader, "Employee_id");
                    employee.FirstName = Utils.ConvertColumnValue<String>(reader, "First_Name");
                    employee.MiddleName = Utils.ConvertColumnValue<String>(reader, "Middle_Name");
                    employee.LastName = Utils.ConvertColumnValue<String>(reader, "Last_Name");
                    employee.DateOfBirth = Utils.ConvertColumnValue<DateTime>(reader, "Date_Of_Birth");
                    employee.Gender = Utils.ConvertColumnValue<String>(reader, "Gender");
                    employee.SSN = Utils.ConvertColumnValue<String>(reader, "SSN");
                    employee.EmploymentID = Utils.ConvertColumnValue<String>(reader, "Employment_ID");
                    employee.EmailAddress = Utils.ConvertColumnValue<String>(reader, "Email_Address");
                    employee.PhoneNumber = Utils.ConvertColumnValue<String>(reader, "Phone_Number");
                    employee.Active = Utils.ConvertColumnValue<Boolean>(reader, "Active");
                    employee.CreatedOn = Utils.ConvertColumnValue<DateTime>(reader, "Created_On");
                    employee.CreatedBy = Utils.ConvertColumnValue<Int64>(reader, "Created_By");
                    employee.LastEditDate = Utils.ConvertColumnValue<DateTime>(reader, "Last_Edit_Date");
                    employee.LastEditBy = Utils.ConvertColumnValue<Int64>(reader, "Last_Edit_By");
                    employee.Salary =Math.Round(Utils.ConvertColumnValue<Decimal>(reader, "Basic_Salary"),2);
                    employees.Add(employee);
                }


                return employees;
            }
            
        /// <summary>
        /// Get employee information for the given Employee returns employee object
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>        
        

        public async Task<Employee> GetEmployee(Int64 employeeId)
        {

            Employee employee = new Employee();
            var parameters = new Dictionary<string, object>
                {
                    { "@Employee_ID", employeeId }
                };
           
            DataSet dsEmployee = await Utils.ExecuteStoredProcedureToGetValues(this._connectionString, "GetEmployeeByID", parameters);
            if (dsEmployee.Tables[0].Rows.Count == 0)
                throw new Exception($"Employee with employee Id {employeeId} does not exist.");

            foreach (DataRow reader in dsEmployee.Tables[0].Rows)
            {

                employee.Id = Utils.ConvertColumnValue<Int64>(reader, "Employee_id");
                employee.FirstName = Utils.ConvertColumnValue<String>(reader, "First_Name");
                employee.MiddleName = Utils.ConvertColumnValue<String>(reader, "Middle_Name");
                employee.LastName = Utils.ConvertColumnValue<String>(reader, "Last_Name");
                employee.DateOfBirth = Utils.ConvertColumnValue<DateTime>(reader, "Date_Of_Birth");
                employee.Gender = Utils.ConvertColumnValue<String>(reader, "Gender");
                employee.SSN = Utils.ConvertColumnValue<String>(reader, "SSN");
                employee.EmploymentID = Utils.ConvertColumnValue<String>(reader, "Employment_ID");
                employee.EmailAddress = Utils.ConvertColumnValue<String>(reader, "Email_Address");
                employee.PhoneNumber = Utils.ConvertColumnValue<String>(reader, "Phone_Number");
                employee.Active = Utils.ConvertColumnValue<Boolean>(reader, "Active");
                employee.Salary = Math.Round(Utils.ConvertColumnValue<Decimal>(reader, "Basic_Salary"),2);
                employee.CreatedOn = Utils.ConvertColumnValue<DateTime>(reader, "Created_On");
                employee.CreatedBy = Utils.ConvertColumnValue<Int64>(reader, "Created_By");
                employee.LastEditDate = Utils.ConvertColumnValue<DateTime>(reader, "Last_Edit_Date");
                employee.LastEditBy = Utils.ConvertColumnValue<Int64>(reader, "Last_Edit_By");
            }

                return employee;
        }
        /// <summary>
        /// To retrieve the job details for the given employee id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<EmployeeJobDetail>> GetEmplopyeeJobDetails(Int64 employeeId)
        {
            
            List<EmployeeJobDetail> employeeJobDetails = new List<EmployeeJobDetail>();
            //Get Employee Job details
            var parameters = new Dictionary<string, object>
                {
                    { "@Employee_ID", employeeId }
                };


            DataSet dsEmployeeJob = await Utils.ExecuteStoredProcedureToGetValues(this._connectionString, "GetEmployeeJobDetails", parameters);
            if (dsEmployeeJob.Tables[0].Rows.Count == 0)
                throw new Exception($"Employee Job information does not exist for the employee Id {employeeId}.");


            foreach (DataRow reader in dsEmployeeJob.Tables[0].Rows)
            {
                EmployeeJobDetail employeeJob = new EmployeeJobDetail();
                employeeJob.EmployeeJobDetailID = Utils.ConvertColumnValue<Int64>(reader, "Employee_Job_Detail_ID");
                employeeJob.EmployeeId = Utils.ConvertColumnValue<Int64>(reader, "Employee_ID");
                employeeJob.JobDescription = Utils.ConvertColumnValue<String>(reader, "Job_Description");
                employeeJob.BasicSalary =Math.Round ( Utils.ConvertColumnValue<Decimal>(reader, "Basic_Salary"),2);
                employeeJob.DefaultDeduction  = Utils.ConvertColumnValue<Decimal>(reader, "Default_Deduction");
                employeeJob.StartDate = Utils.ConvertColumnValue<DateTime>(reader, "Start_Date");
                employeeJob.EndDate = Utils.ConvertColumnValue<DateTime>(reader, "End_Date") == dateTimeDef ? null: Utils.ConvertColumnValue<DateTime>(reader, "End_Date");
                employeeJob.CreatedOn = Utils.ConvertColumnValue<DateTime>(reader, "Created_On");
                employeeJob.CreatedBy = Utils.ConvertColumnValue<Int64>(reader, "Created_By");
                employeeJob.LastEditDate = Utils.ConvertColumnValue<DateTime>(reader, "Last_Edit_Date");
                employeeJob.LastEditBy = Utils.ConvertColumnValue<Int64>(reader, "Last_Edit_By");

                employeeJobDetails.Add(employeeJob);
            }

            return employeeJobDetails;

            
        }
            }
}
