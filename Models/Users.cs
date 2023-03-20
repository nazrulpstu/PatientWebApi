namespace PatientWebApi.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; } = DateTime.Now;
        public DateTime TokenExpires { get; set; }


    }

    public class Credentialinfo { 
    
    public string username { get; set; }
        public string password { get; set; }
    }
}

