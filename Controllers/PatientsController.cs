using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PatientWebApi.Interfaces;
using PatientWebApi.Models;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PatientWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        //DBContext db = new DBContext();
        private IPatient Dbcontext;
         public PatientsController(IPatient dbcontext)
        {
            this.Dbcontext = dbcontext;
        }

        //[EnableQuery]
     
        [HttpGet("GetAllPatients"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Patients>>> GetAllPatient()
        {
            var result = await Dbcontext.GetPatientsAsync();

            return Ok(result.Patients);
        }

        // GET: api/<PatientsController>

        [HttpGet("GetPatientById/{patientId}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPatientById(int patientId)
        {
            var result = await Dbcontext.GetPatientByIdAsync(patientId);
            if (result.IsSuccess)
            {

                return Ok(result.Patients.FirstOrDefault());

            }
            return NotFound();
        }

        [HttpPost("AddPatient"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Patients>> AddPatient([FromBody] Patients obj)
        {
          var response = await Dbcontext.Add(obj);
            if (response.IsSuccess)
            {
                return Ok(response.Id);
            }

            return NotFound();


        }

        [HttpPut("UpdatePatient/{Id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePatient(int Id, [FromBody] Patients obj)
        {

            var response = await Dbcontext.UpdateAsync(Id, obj);
            if (response.IsSuccess)
            {
                return Ok(response.Id);
            }

            return NotFound();
        }


        [HttpDelete("DeletePatient/{Id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePatient(int Id)
        {
            var response = await Dbcontext.DeleteAsync(Id);
            if (response.IsSuccess)
            {
                return Ok(new { response.ErrorMessage });
            }
            return NoContent();
        }


    }
}
