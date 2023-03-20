using PatientWebApi.Models;

namespace PatientWebApi.Interfaces
{
    public interface IAuthenticate
    {

        Task<IEnumerable<Users>> getuser();
        Task<(bool IsSuccess, Users user, String ErrorMessage)> AuthenticateUser(string username, string passcode);
        //Task<(bool IsSuccess, IEnumerable<Users> user, String ErrorMessage)> VerifyPasswordHash(object request);
        //Task<(bool IsSuccess, int Id, String ErrorMessage)> Add(Patients obj);
        //// public bool Edit(int id, Patients obj);
        //Task<(bool IsSuccess, int Id, String ErrorMessage)> UpdateAsync(int Id, Patients obj);
        ////public bool DeletePatent(int id);
        //Task<(bool IsSuccess, String ErrorMessage)> DeleteAsync(int Id);

        //Task<(bool IsSuccess, IEnumerable<Patients> Patients, String ErrorMessage)> GetPatientByIdAsync(int patientId);

    }
}
