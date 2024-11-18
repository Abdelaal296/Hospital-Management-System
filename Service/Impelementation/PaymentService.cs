using HospitalSystem.Models;
using HospitalSystem.Repo.Abstraction;
using HospitalSystem.Service.Abstraction;

namespace HospitalSystem.Service.Impelementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepo paymentRepo;
        public PaymentService(IPaymentRepo paymentRepo)
        { this.paymentRepo = paymentRepo; }

        public Bill ViewBill(int id)
        {
            return paymentRepo.ViewBill(id);
        }
    }
}
