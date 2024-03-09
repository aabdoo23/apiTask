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
    public class PatientRecordController : ControllerBase
    {

        private readonly PatientRecordsService _patientRecordsService;
        public PatientRecordController()
        {
            _patientRecordsService = new PatientRecordsService(new DB());
        }

        [HttpPost("addNewPatientRecord")]
        public async Task<ActionResult<PatientRecord>> CreatePatientRecord(PatientRecord patientRecord)
        {
            // Check if any of the fields is null or empty
            if (string.IsNullOrEmpty(patientRecord.Diagnosis) || long.IsNegative(patientRecord.PatientId))
            {
                return BadRequest("Please fill all required fields.");
            }
            await _patientRecordsService.CreatePatientRecordAsync(patientRecord);
            return Ok("Success");
        }
        [HttpGet("getRecordById/{id}")]
        public async Task<ActionResult<PatientRecord>> GetPatientRecordById(long id)
        {
            var patientRecord = await _patientRecordsService.GetPatientRecordByIdAsync(id);
            if (patientRecord == null)
                return NotFound();
            return patientRecord;
        }
        [HttpPut("updatePatientRecord/{id}")]
        public async Task<IActionResult> UpdatePatientRecord(long id, PatientRecord patientRecord)
        {
            if (id != patientRecord.PatientRecordId)
                return BadRequest();
            await _patientRecordsService.UpdatePatientRecordAsync(patientRecord);
            return NoContent();
        }

        [HttpDelete("deletePatientRecord/{id}")]
        public async Task<IActionResult> DeletePatientRecord(long id)
        {
            await _patientRecordsService.DeletePatientRecordAsync((int)id);
            return NoContent();
        }
        [HttpGet("getAllRecords")]
        public async Task<ActionResult<PatientRecord[]>> GetPatientRecords()
        {
            var patientRecords = await _patientRecordsService.GetPatientRecordsAsync();
            return Ok(patientRecords);
        }

        [HttpGet("getAllPatientRecordsByID/{id}")]
        public async Task<ActionResult<PatientRecord[]>> GetPatientRecordsByPatientId(long id)
        {
            var patientRecords = await _patientRecordsService.GetPatientRecordsByPatientIdAsync(id);
            if (patientRecords == null)
                return NotFound();
            return Ok(patientRecords);
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

