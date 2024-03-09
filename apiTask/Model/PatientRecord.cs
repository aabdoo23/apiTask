namespace apiTask.Model
{
    public class PatientRecord
    {
        public long PatientRecordId { get; set; }
        public long PatientId { get; set; }
        public string Diagnosis { get; set; }
        public DateTime DateOfDiagnosis { get; set; }
        public PatientRecord()
        {
        }
        public PatientRecord(long id, long patientId, string diagnosis, DateTime dateOfDiagnosis)
        {
            this.PatientRecordId = id;
            this.PatientId = patientId;
            this.Diagnosis = diagnosis;
            this.DateOfDiagnosis = dateOfDiagnosis;
        }
    }
}
