using HospitalSystem.Models;

namespace HospitalSystem.Repo.Abstraction
{
    public interface IPaymentRepo
    {
        public Bill ViewBill(int id);
    }
}
