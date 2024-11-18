using HospitalSystem.Data;
using HospitalSystem.Models;
using HospitalSystem.Repo.Abstraction;

namespace HospitalSystem.Repo.Impelementation
{
    public class DoctorRepo : IDoctorRepo
    {
        private readonly AppDbContext entitiy;

        public DoctorRepo(AppDbContext entitiy)
        {
            this.entitiy = entitiy;
        }
        bool IDoctorRepo.Create(Doctor doc)
        {
            try
            {
                entitiy.Doctors.Add(doc);
                entitiy.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        bool IDoctorRepo.Delete(Doctor doc)
        {
            try
            {

                var data = entitiy.Doctors.Where(e => e.Id == doc.Id).FirstOrDefault();
                data.IsDelete = !data.IsDelete;
                entitiy.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        bool IDoctorRepo.Edit(Doctor doc)
        {
            try
            {
                var data = entitiy.Doctors.Where(d => d.Id == doc.Id).FirstOrDefault();
                data.FullName = doc.FullName;
                data.Address = doc.Address;
                data.PhoneNumber = doc.PhoneNumber;
                data.Email = doc.Email;
                data.Gender = data.Gender;
                data.Specialization = data.Specialization;
                entitiy.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        Doctor IDoctorRepo.GetById(int id)
        {
            return entitiy.Doctors.Where(e => e.Id == id).FirstOrDefault();
        }

        List<Doctor> IDoctorRepo.GetDoctors()
        {
            return entitiy.Doctors.ToList();
        }

        Doctor IDoctorRepo.GetDoctorByEmail(string email)
        {
             var doc =entitiy.Doctors.Where(e => e.Email == email).FirstOrDefault();
            return doc;
        }
    }
}
