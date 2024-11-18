using HospitalSystem.Data;
using HospitalSystem.Models;
using HospitalSystem.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace HospitalSystem.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService paymentService;
        private readonly string _secretKey = "sk_test_51PyDZPKHC4DYBQLbDO0J11xfrIJ3EZBN5G33Y6MeVnMyVRr3Dodyr8XoHlH8BuWmLHMHUbf4woSdoNgGKbRqm1qJ00DpjFEqgq"; 
        private readonly AppDbContext db;

        public PaymentController(IPaymentService paymentService, AppDbContext db)
        {
            this.paymentService = paymentService;
            StripeConfiguration.ApiKey = _secretKey;
            this.db = db;
        }




        [HttpGet]
        public IActionResult ViewBill(int id)
        {
            
            Bill bill = paymentService.ViewBill(id);

            if (bill == null)
            {
                return NotFound("Bill not found");
            }

            return View(bill);
        }


        [HttpGet]
        public IActionResult PayBill(int id)
        {

            Bill bill = paymentService.ViewBill(id);

            if (bill == null)
            {
                return NotFound("Bill not found");
            }

            if (bill.IsPaid)
            {
                ViewBag.Message = "This bill has already been paid.";
                return RedirectToAction("ViewBill", new { id });
            }

            ViewBag.PublishableKey = "pk_test_51PyDZPKHC4DYBQLbAzmBpu7PhnF8iHIRZ1D9Sq9HYldT6tIt1Vut18JbK1BBCErKGeJytEeTj9lBWt01hKVaMNht00hRV3poG0"; // Replace with your Stripe Publishable Key
            return View("PaymentForm", bill); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessBillPayment(string stripeToken, int billID, decimal amount)
        {
            try
            {
                var options = new ChargeCreateOptions
                {
                    Amount = (long)(amount * 100), // Stripe works with amounts in cents
                    Currency = "usd",
                    Description = $"Bill Payment for Bill ID: {billID}",
                    Source = stripeToken,
                    Metadata = new Dictionary<string, string>
                    {
                        { "BillID", billID.ToString() }
                    }
                };

                var service = new ChargeService();
                Charge charge = service.Create(options);

               
                Bill bill = paymentService.ViewBill(billID);
                if (bill != null)
                {
                    bill.StripePaymentId = charge.Id;
                    bill.PaymentDate = DateTime.Now;
                    bill.IsPaid = true;

                    db.SaveChanges(); 

                    ViewBag.Message = "Payment successful!";
                }

                return RedirectToAction("ViewBill", new { id = billID });
            }
            catch (StripeException ex)
            {
                ViewBag.Message = $"Payment failed: {ex.Message}";
                return View("ViewBill");
            }
        }



    }
}
