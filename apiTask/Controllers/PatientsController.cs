//mongodb
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using apiTask.Data;
using apiTask.Model;
using apiTask.Service;

namespace apiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientsController()
        {
            _patientService = new PatientService(new DB());
        }

        [HttpPost("addNewPatient")]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            // Check if any of the fields is null or empty
            if (string.IsNullOrEmpty(patient.Name) || string.IsNullOrEmpty(patient.NationalId) || string.IsNullOrEmpty(patient.City))
            {
                return BadRequest("Name, NationalId, and City are required fields.");
            }

            // Check if the NationalId already exists
            var existingPatient = await _patientService.GetPatientByNationalIdAsync(patient.NationalId);
            if (existingPatient != null)
            {
                return Conflict("Patient with the same NationalId already exists.");
            }

            await _patientService.CreatePatientAsync(patient);
            return Ok("Success");
        }
        [HttpGet("getPatientById/{id}")]
        public async Task<ActionResult<Patient>> Get(long id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
                return NotFound();
            return patient;
        }

        [HttpPut("updatePatient/{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            if (id != patient.PatientId)
                return BadRequest();

            // Check if any of the fields is null or empty
            if (string.IsNullOrEmpty(patient.Name) || string.IsNullOrEmpty(patient.NationalId) || string.IsNullOrEmpty(patient.City))
            {
                return BadRequest("Name, NationalId, and City are required fields.");
            }

            await _patientService.UpdatePatientAsync(patient);
            return NoContent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("deletePatient/{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            await _patientService.DeletePatientAsync(id);
            return NoContent();
        }
        


        /// <summary>
        /// Retrieves all patient records.
        /// </summary>
        /// <returns>A list of patient records.</returns>
        [HttpGet("getAllPatients")]
        [ProducesResponseType(typeof(PatientRecord[]), 200)]

        public async Task<ActionResult<Patient[]>> GetPatients()
        {
            var patients = await _patientService.GetPatientsAsync();
            return Ok(patients);
        }
    }
}



//sql

//using apiTask.Data;
//using apiTask.Model;
//using apiTask.Service;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using System;

//namespace apiTask.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PatientsController : ControllerBase
//    {
//        private readonly PatientService _patientService;
//        public PatientsController()
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<DB>();
//            optionsBuilder.UseSqlServer("Data Source=sql.bsite.net\\MSSQL2016;User ID = nunermien_303; Password = Nu@654321;  Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");

//            var dbContext = new DB(optionsBuilder.Options);
//            _patientService = new PatientService(dbContext);
//        }
//        [HttpPost]
//        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
//        {
//            var newPatient = await _patientService.CreatePatientAsync(patient);
//            return CreatedAtRoute("GetPatient", new { PatientId = newPatient.PatientId }, newPatient);
//        }
//        [HttpGet("{id}", Name = "GetPatient")]
//        public async Task<ActionResult<Patient>> GetPatientById(int id)
//        {
//            var patient = await _patientService.GetPatientByIdAsync(id);
//            if (patient == null)
//                return NotFound();
//            return patient;
//        }


//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
//        {
//            if (id != patient.PatientId)
//                return BadRequest();
//            var result = await _patientService.UpdatePatientAsync(patient);
//            if (!result)
//                return NotFound();
//            return NoContent();
//        }
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeletePatient(int id)
//        {
//            var result = await _patientService.DeletePatientAsync(id);
//            if (!result)
//                return NotFound();
//            return NoContent();
//        }
//        [HttpGet(Name = "GetAllPatients")]
//        public async Task<ActionResult<Patient[]>> GetPatients()
//        {
//            return await _patientService.GetPatientsAsync();
//        }
//    }
//}

