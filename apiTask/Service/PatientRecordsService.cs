//mongodb

using System.Collections.Generic;
using System.Threading.Tasks;
using apiTask.Data;
using apiTask.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace apiTask.Service
{
    public class PatientRecordsService
    {
        private readonly IMongoCollection<PatientRecord> _patientRecords;

        public PatientRecordsService(DB context)
        {
            _patientRecords = context.PatientRecords;
        }

        public async Task CreatePatientRecordAsync(PatientRecord patientRecord)
        {
            await _patientRecords.InsertOneAsync(patientRecord);
        }

        public async Task<PatientRecord> GetPatientRecordByIdAsync(long id)
        {
            return await _patientRecords.Find(patientRecord => patientRecord.PatientRecordId == id).FirstOrDefaultAsync();
        }

        public async Task UpdatePatientRecordAsync(PatientRecord patientRecord)
        {
            await _patientRecords.ReplaceOneAsync(p => p.PatientRecordId == patientRecord.PatientRecordId, patientRecord);
        }

        public async Task DeletePatientRecordAsync(int id)
        {
            await _patientRecords.DeleteOneAsync(patientRecord => patientRecord.PatientRecordId == id);
        }

        public async Task<IEnumerable<PatientRecord>> GetPatientRecordsAsync()
        {
            return await _patientRecords.Find(patientRecord => true).ToListAsync();
        }

        public async Task<IEnumerable<PatientRecord>> GetPatientRecordsByPatientIdAsync(long id)
        {
            return await _patientRecords.Find(patientRecord => patientRecord.PatientId == id).ToListAsync();
        }
    }
    
}