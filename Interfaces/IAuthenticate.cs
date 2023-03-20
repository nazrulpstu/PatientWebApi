using PatientWebApi.Models;

namespace PatientWebApi.Interfaces
{
    public interface IAuthenticate
    {

        Task<IEnumerable<Users>> getuser();
        Task<(bool IsSuccess, Users user, String ErrorMessage)> AuthenticateUser(string username, string passcode);
      
    }
}
