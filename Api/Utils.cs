using System.Reflection.Metadata.Ecma335;
using System.Data;
using System.Data.SqlClient;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Reflection;
namespace Api
{
    
    public class Utils
    {
     /// <summary>
     /// This returns 26 payperiods for given year.
     /// </summary>
     /// <param name="year"></param>
     /// <returns></returns>
        public  static Dictionary<int, (DateTime startDate, DateTime endDate)> GetPayPeriods(int year)
        {
            //List<(DateTime StartDate, DateTime EndDate)> periods = new List<(DateTime, DateTime)>();
            var payPeriods = new Dictionary<int, (DateTime startDate, DateTime endDate)>();
            DateTime startDate = new DateTime(year, 1, 1);
            DateTime endDate = new DateTime(year, 12, 31);
            int totalDays = (endDate - startDate).Days + 1; // Include last day
            int periodLength = totalDays / 26;

            for (int i = 0; i < 26; i++)
            {
                DateTime periodEndDate = startDate.AddDays(periodLength - 1);

                // Ensure last period ends on the year's last day
                if (i == 25) periodEndDate = endDate;
                payPeriods.Add(i, (startDate, periodEndDate));
                
                startDate = periodEndDate.AddDays(1); // Move to next period
            }

            return payPeriods;
        }
        public static async Task ExecuteStoredProcedure(string connectionString, string storedProcedureName, Dictionary<string, object> parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Assign parameters from dictionary
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        await command.ExecuteNonQueryAsync();

                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public static async Task<DataSet> ExecuteStoredProcedureToGetValues(string connectionString, string storedProcedureName, Dictionary<string, object> parameters)
        {
            try
            {
                DataSet dataSet = new DataSet();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Assign parameters from dictionary
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }


                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            
                            adapter.Fill(dataSet);
                           
                        }
                    }

                }
                return dataSet;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public static async Task<DataSet> ExecuteStoredProcedureToGetValues(string connectionString, string storedProcedureName)
        {
            try
            {
                DataSet dataSet = new DataSet();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {

                            adapter.Fill(dataSet);

                        }
                    }

                }
                return dataSet;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public static T ConvertColumnValue<T>(DataRow row, string columnName, T defaultValue = default )
        {
            if (row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
            {
                return (T)Convert.ChangeType(row[columnName], typeof(T));
            }
            return defaultValue;
        }
    }

}