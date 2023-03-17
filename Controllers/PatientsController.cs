using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PatientWebApi.Interfaces;
using PatientWebApi.Models;

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
     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patients>>> GetAllPatient()
        {
            var result = await Dbcontext.GetPatientsAsync();

            return Ok(result.Patients);
        }

        // GET: api/<PatientsController>

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetPatientAsync(int patientId)
        {
            var result = await Dbcontext.GetPatientByIdAsync(patientId);
            if (result.IsSuccess)
            {

                return Ok(result.Patients);

            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Patients>> PostOrder([FromBody] Patients obj)
        {
          var response = await Dbcontext.Add(obj);
            if (response.IsSuccess)
            {
                return Ok(response.Id);
            }

            return NotFound();


        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutOrder(int Id, [FromBody] Patients obj)
        {

            var response = await Dbcontext.UpdateAsync(Id, obj);
            if (response.IsSuccess)
            {
                return Ok(response.Id);
            }

            return NotFound();
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteOrder(int Id)
        {
            var response = await Dbcontext.DeleteAsync(Id);
            if (response.IsSuccess)
            {
                return Ok("Delete successfully");
            }
            return NoContent();
        }


    }
}
