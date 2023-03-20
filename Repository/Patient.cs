using PatientWebApi.Models;
using System.Data.SqlClient;
using System.Data;
using PatientWebApi.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static IdentityServer4.Models.IdentityResources;
using System.Reflection;
using System.Security.Cryptography;

namespace PatientWebApi.Repository
{
    public class Patient : IPatient
    {
        private readonly string _dbContext;
       

        private readonly ILogger<Patient> ILogger;
       

        public Patient( ILogger<Patient> Logger)
        {
            this._dbContext = ConnectionStrings.DbConnection;
            this.ILogger = Logger;
          
        }

        public async Task<(bool IsSuccess, int Id, string ErrorMessage)> Add(Patients obj)
        {

                      
            using var connection = new SqlConnection(_dbContext);
            connection.Open();

            // Create a command object that represents the stored procedure
            using var command = new SqlCommand("PatientInfoCRUD", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@StatementType", "Insert");
            command.Parameters.AddWithValue("@Id", obj.Id);
            command.Parameters.AddWithValue("@FirstName", obj.FirstName);
            command.Parameters.AddWithValue("@LastName", obj.LastName);
            command.Parameters.AddWithValue("@FullName", obj.FullName);
            command.Parameters.AddWithValue("@Age", obj.Age);
            command.Parameters.AddWithValue("@Gender", obj.Gender);
            command.Parameters.AddWithValue("@Address", obj.Address);
            command.Parameters.AddWithValue("@City", obj.City);
            command.Parameters.AddWithValue("@Province", obj.Province);
            command.Parameters.AddWithValue("@PostCode", obj.PostCode);
            command.Parameters.AddWithValue("@IsActive", obj.IsActive);
            command.Parameters.AddWithValue("@IsDelete", obj.IsDelete);
             int result = (int) command.ExecuteScalar();
            return (result > 0, result, null);
           
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteAsync(int Id)
        {
            string query = "delete patient where Id='" + Id + "'";
            using (SqlConnection con = new SqlConnection(_dbContext))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int i = cmd.ExecuteNonQuery();
                    return (i > 0, "Success");
                }
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Patients> Patients, string ErrorMessage)> GetPatientByIdAsync(int patientId)
        {
            try
            {
                List<Patients> list = new List<Patients>();

                string query = "Select * from patient  where Id='" + patientId + "' ";
                using (SqlConnection con = new SqlConnection(_dbContext))
                {
                    using (SqlCommand cmd =  new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable dt =  new DataTable();
                        adp.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(new Patients
                            {
                                Id = Convert.ToInt32(dr[0]),
                                FirstName = Convert.ToString(dr[1]),
                                LastName = Convert.ToString(dr[2]),
                                //FullName = Convert.ToString(dr[3]),
                                Age = (short)dr[4],
                                Gender = Convert.ToString(dr[5]),
                                Address = Convert.ToString(dr[6]),
                                City = Convert.ToString(dr[7]),
                                Province = Convert.ToString(dr[8]),
                                PostCode = Convert.ToString(dr[9]),
                                IsActive = (short)(dr[10])

                            });
                        }
                    }
                }
                if (list.Count > 0)
                {

                    return (true, list, "Success");
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                ILogger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Patients> Patients, string ErrorMessage)> GetPatientsAsync()
        {

            try
            {
                List<Patients> list = new List<Patients>();


                string query = "Select * from patient";
                using (SqlConnection con = new SqlConnection(_dbContext))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(new Patients
                            {
                                Id = Convert.ToInt32(dr[0]),
                                FirstName = Convert.ToString(dr[1]),
                                LastName = Convert.ToString(dr[2]),
                                //FullName = Convert.ToString(dr[3]),
                                Age = (short)dr[4],
                                Gender = Convert.ToString(dr[5]),
                                Address = Convert.ToString(dr[6]),
                                City = Convert.ToString(dr[7]),
                                Province = Convert.ToString(dr[8]),
                                PostCode = Convert.ToString(dr[9]),
                                IsActive = (short)(dr[10])

                            });
                        }
                    }
                }
                if (list.Count > 0) {

                    return (true, list, "Success");
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                ILogger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, int Id, string ErrorMessage)> UpdateAsync(int Id, Patients obj)
        {
            string query = "update patient " +
                 "set FirstName= '" + obj.FirstName + "'," +
                 " LastName='" + obj.LastName + "', " +
                  " fullName='" + obj.FullName + "', " +
                   " Gender='" + obj.Gender + "', " +
                   " Age='" + obj.Age + "', " +
                   " Address='" + obj.Address + "' ," +
                   " City='" + obj.City + "' " +
                 "where Id='" + Id + "' ";
            using (SqlConnection con = new SqlConnection(_dbContext))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int i = cmd.ExecuteNonQuery();
                    return (i > 0,Id,"Update Sucessfully!");
                }
            }
        }

       
    }
}
