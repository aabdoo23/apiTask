using MongoDB.Bson;

namespace apiTask.Model
{
    public class Patient
    {
        public ObjectId _id { get; set; }//lw sql hash this line
        public int PatientId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string NationalId { get; set; }
        public string City { get; set; }
        public Patient(){}
        public Patient(int patientId, string name, DateTime birthDate,string nationalId, string city)
        {
            this.PatientId = patientId;
            this.Name = name;
            this.BirthDate = birthDate;
            this.NationalId = nationalId;
            this.City = city;
        }
    }
}
