using PatientWebApi.Interfaces;
using PatientWebApi.Models;
using System.Data.SqlClient;
using System.Data;

namespace PatientWebApi.Repository
{
    public class LoginRepo : IAuthenticate
    {
        private readonly string _dbContext;


        private readonly ILogger<LoginRepo> ILogger;


        public LoginRepo(ILogger<LoginRepo> Logger)
        {
            this._dbContext = ConnectionStrings.DbConnection;
            this.ILogger = Logger;

        }

       public async Task<(bool IsSuccess, Users user, string ErrorMessage)> AuthenticateUser(string username, string passcode)
        {
                
            try
            {
                Users usr = new Users();
                    

                string query = "Select * from Appuser  where UserName='" + username + "'";
                //string query = "Select * from user  where UserName='" + username + "' AND PassWord='" + passcode + "' ";
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

                            usr.Id = Convert.ToInt32(dr[0]);
                            usr.Name = Convert.ToString(dr[1]);
                            usr.UserName = Convert.ToString(dr[2]);
                            usr.Email = Convert.ToString(dr[4]);


                            
                        }
                    }
                }
                if (usr.Id > 0)
                {

                    return (true, usr, "Success");
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                ILogger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }

        }

        public Task<IEnumerable<Users>> getuser()
        {
            throw new NotImplementedException();
        }

     
    }
}
