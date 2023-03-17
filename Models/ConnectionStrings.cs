using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PatientWebApi.Models
{
    public static class ConnectionStrings
    {
        

        public static string DbConnection { get; set; }
    }
        /*    public List<Patients> GetPatients()
        {
            List<Patients> list = new List<Patients>();
            string query = "Select * from patient";
            using (SqlConnection con = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(new Patients { 
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
            return list;
        }

        public bool Add(Patients obj)
        {
            string query = "insert into patient(Id,FirstName,LastName,FullName,Age,Gender,Address,City,Province,PostCode,IsActive,IsDelete)values('" + obj.Id + "','" + obj.FirstName + "','"
                + obj.LastName + "','" + obj.FullName + "','" + obj.Age + "','" 
                + obj.Gender + "','" + obj.Address + "','" + obj.City + "','" + obj.Province + "','"
                + obj.PostCode + "','" + obj.IsActive + "','" + obj.IsDelete + "')";
            using (SqlConnection con = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool Edit(int id, Patients obj)
        {
            string query = "update patient " +
                "set FirstName= '" + obj.FirstName + "'," +
                " LastName='" + obj.LastName + "' " +
                "where Id='" + id + "' ";
            using (SqlConnection con = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool DeletePatent(int id)
        {
            string query = "delete patient where Id='" + id + "'";
            using (SqlConnection con = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    
    */
    
}
