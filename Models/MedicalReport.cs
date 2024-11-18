using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Models
{
    public class MedicalReport
    {
        [Key]
        public int ID { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public string ReportID { get; set; }
        [ForeignKey("Patient")]
        public string? PatientID { get; set; }
        public string PatientNationalId { get; set; }
        public string? ReportImagePath { get; set; }
    }
}
