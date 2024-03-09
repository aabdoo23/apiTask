//mongodb

using System.Collections.Generic;
using System.Threading.Tasks;
using apiTask.Data;
using apiTask.Model;
using MongoDB.Driver;

namespace apiTask.Service
{
    public class PatientService
    {
        private readonly IMongoCollection<Patient> _patients;

        public PatientService(DB context)
        {
            _patients = context.Patients;
        }

        public async Task CreatePatientAsync(Patient patient)
        {
            await _patients.InsertOneAsync(patient);
        }
        public async Task<Patient> GetPatientByNationalIdAsync(string nationalId)
        {
            return await _patients.Find(patient => patient.NationalId == nationalId).FirstOrDefaultAsync();
        }
        public async Task<Patient> GetPatientByIdAsync(long id)
        {
            return await _patients.Find(patient => patient.PatientId == id).FirstOrDefaultAsync();
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            await _patients.ReplaceOneAsync(p => p.PatientId == patient.PatientId, patient);
        }

        public async Task DeletePatientAsync(int id)
        {
            await _patients.DeleteOneAsync(patient => patient.PatientId == id);
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            return await _patients.Find(patient => true).ToListAsync();
        }
    }
}
/////////////////////////////////////////////////
//sql
////////////////////////////////////////////////


//using System;
//using Microsoft.EntityFrameworkCore;
//using System.Threading.Tasks;
//using System.Linq;
//using apiTask.Data;
//using apiTask.Model;
//using MongoDB.Driver;
//namespace apiTask.Service
//{
//    public class PatientService
//    {
//        private readonly DB _context;
//        public PatientService(DB context)
//        {
//            _context = context;
//        }
//        public async Task<Patient> CreatePatientAsync(Patient patient)
//        {
//            _context.Patients.Add(patient);
//            await _context.SaveChangesAsync();
//            return patient;
//        }

//        public async Task<Patient> GetPatientByIdAsync(int id)
//        {
//            return await _context.Patients.FindAsync(id);
//        }

//        public async Task<bool> UpdatePatientAsync(Patient patient)
//        {
//            _context.Entry(patient).State = EntityState.Modified;
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        public async Task<bool> DeletePatientAsync(int id)
//        {
//            var patient = await _context.Patients.FindAsync(id);
//            if (patient == null)
//                return false;

//            _context.Patients.Remove(patient);
//            await _context.SaveChangesAsync();
//            return true;
//        }
//        public async Task<Patient[]> GetPatientsAsync()
//        {
//            return await _context.Patients.ToArrayAsync();
//        }
//    }
//}

