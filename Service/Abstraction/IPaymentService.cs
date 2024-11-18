using HospitalSystem.Models;

namespace HospitalSystem.Service.Abstraction
{
    public interface IPaymentService
    {
        public Bill ViewBill(int id);
    }
}
