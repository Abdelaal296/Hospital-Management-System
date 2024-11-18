using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.ModelVM.Doctor
{
    public class GetAllDoctorsVM
    {
        [Key]
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Specialization { get; set; }
        public string? Image { get; set; }
        public bool IsDelete { get; set; }
    }
}
