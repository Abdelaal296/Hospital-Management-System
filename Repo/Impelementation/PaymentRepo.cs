using HospitalSystem.Data;
using HospitalSystem.Models;
using HospitalSystem.Repo.Abstraction;

namespace HospitalSystem.Repo.Impelementation
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly AppDbContext db;
        public PaymentRepo(AppDbContext db)
        {
            this.db = db;
        }
        public Bill ViewBill(int id)
        {
            return db.Bills.FirstOrDefault(b => b.ID == id);
        }

    }
}
