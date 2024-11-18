using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.ModelVM.MedicalReport
{
    public class EditReportVM
    {
        public string ReportID { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Please upload a report image.")]
        public IFormFile ReportImage { get; set; }

        [ForeignKey("Patient")]
        public string? PatientID { get; set; }
        public string PatientNationalId { get; set; }

    }
}
