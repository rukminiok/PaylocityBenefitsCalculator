
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO.Pipelines;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace Api.Repository
{
    
    public class DependentRepository
    {
        private string _connectionString;
        private DateTime dateTimeDef = default;
        public DependentRepository(IConfiguration _configuration)
        {

            _connectionString = _configuration.GetConnectionString("DefaultConnection"); ;

        }
        /// <summary>
        /// to get a dependent information by depentdent ID and returns dependent object.
        /// </summary>
        /// <param name="dependentId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Dependent> GetDependent(Int64 dependentId)
        {
                Dependent dependent = new Dependent();
                var parameters = new Dictionary<string, object>
                {
                    { "@Dependent_ID", dependentId }
                };


                DataSet dsDependent = await Utils.ExecuteStoredProcedureToGetValues(this._connectionString, "GetDependentsByDependentID", parameters);

                if (dsDependent.Tables[0].Rows.Count == 0)
                  throw new Exception($"Dependent with Dependent ID {dependentId} does not exist. ");

                foreach (DataRow reader in dsDependent.Tables[0].Rows)
                {
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
                     if (Enum.TryParse<Relationship>(reader["Relationship_Type"].ToString(), true, out Relationship relationship)) { dependent.Relationship = relationship; }


            }

                return dependent;
            }
            
        /// <summary>
        /// To get all the dependents
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        public async Task<List<Dependent>> GetAllDependents()
        {
            
            List<Dependent> dependents = new List<Dependent>();
            DataSet dsDependent = await Utils.ExecuteStoredProcedureToGetValues(this._connectionString, "GetAllDependents");

            
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
                if (Enum.TryParse<Relationship>(reader["Relationship_Type"].ToString(), true, out Relationship relationship)) { dependent.Relationship = relationship; }
                dependents.Add(dependent);
            }

            return dependents;

        }

       

        /// <summary>
        /// Code to insert  dependent into the dependent table
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<int> InsertDependent(Dependent dependent)
        {
            throw new NotImplementedException();
        }
    }

}
