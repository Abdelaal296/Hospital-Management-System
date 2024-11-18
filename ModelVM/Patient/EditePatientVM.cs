namespace HospitalSystem.ModelVM.Patient
{
    public class EditePatientVM
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string? Image { get; set; }
        public IFormFile? NewProfileImage { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }

    }
}
