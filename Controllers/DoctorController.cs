using HospitalSystem.ModelVM.Doctor;
using HospitalSystem.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDocServices docServices;

        public DoctorController(IDocServices docServices)
        {
            this.docServices = docServices;
        }

        public IActionResult Index()
        {
            var Data = docServices.GetAllDoctors();
            if (Data != null)
            {
                return View(Data);

            }
            return View();

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateDocVM createDocVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    docServices.Create(createDocVM);
                    return RedirectToAction("Index");
                }

                return View(createDocVM);
            }
            catch (Exception)
            {
                return View(createDocVM);
            }

        }
        [Authorize(Roles = "Admin")]


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = docServices.GetByIdDocVM(id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditDocVM editDocVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    docServices.Edit(editDocVM);
                    return RedirectToAction("Index");
                }

                return View(editDocVM);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(editDocVM);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = docServices.GetByIdDocVM(id);
            try
            {

                if (data != null)
                {
                    DeleteDocVM newdata = new()
                    {
                        Id = data.Id,
                        IsDelete = data.IsDelete,
                    };

                    var employee = docServices.Delete(newdata);
                    return RedirectToAction("Index", "Doctor");
                }
                return View(data);

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(data);
            }

        }

    }
}
