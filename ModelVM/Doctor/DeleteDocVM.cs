using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.ModelVM.Doctor
{
    public class DeleteDocVM
    {
        [Key]
        public int Id { get; set; }
        public bool IsDelete { get; set; }
    }
}
