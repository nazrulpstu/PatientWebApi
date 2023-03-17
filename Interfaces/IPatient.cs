using PatientWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApi.Interfaces
{
    public interface IPatient
    {
        Task<(bool IsSuccess, IEnumerable<Patients> Patients, String ErrorMessage)> GetPatientsAsync();
        Task<(bool IsSuccess, int Id, String ErrorMessage)> Add(Patients obj);
       // public bool Edit(int id, Patients obj);
        Task<(bool IsSuccess, int Id, String ErrorMessage)> UpdateAsync(int Id, Patients obj);
        //public bool DeletePatent(int id);
        Task<(bool IsSuccess, String ErrorMessage)> DeleteAsync(int Id);

        Task<(bool IsSuccess, IEnumerable<Patients> Patients, String ErrorMessage)> GetPatientByIdAsync(int patientId);
               
       
    }
}
