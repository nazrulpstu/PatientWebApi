using PatientWebApi.Models;
using System.Data.SqlClient;
using System.Data;
using PatientWebApi.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace PatientWebApi.Provider
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
            string query = "insert into patient(Id,FirstName,LastName,FullName,Age,Gender,Address,City,Province,PostCode,IsActive,IsDelete) OUTPUT INSERTED.ID values('" + obj.Id + "','" + obj.FirstName + "','"
               + obj.LastName + "','" + obj.FullName + "','" + obj.Age + "','"
               + obj.Gender + "','" + obj.Address + "','" + obj.City + "','" + obj.Province + "','"
               + obj.PostCode + "','" + obj.IsActive + "','" + obj.IsDelete + "')";
            using (SqlConnection con = new SqlConnection(_dbContext))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int result = (int)cmd.ExecuteScalar();

                    return (result > 0, result, null);
                   
                }
            }




           // return (result > 0, order.Id, null);
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
                                FullName = Convert.ToString(dr[3]),
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
                                FullName = Convert.ToString(dr[3]),
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
                 " LastName='" + obj.LastName + "' " +
                 "where Id='" + Id + "' ";
            using (SqlConnection con = new SqlConnection(_dbContext))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int i = cmd.ExecuteNonQuery();
                    return (Id>0,Id,"Update Sucessfully!");
                }
            }
        }
    }
}
