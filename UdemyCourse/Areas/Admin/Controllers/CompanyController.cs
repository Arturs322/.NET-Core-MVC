using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Udemy.DataAccess.Repository.IRepository;
using Udemy.Models;
using Udemy.Utilities;

namespace UdemyCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Company> companyList = _unitOfWork.Company.GetAll().ToList();
            return View(companyList);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null) return View(new Company());

            Company company = _unitOfWork.Company.Get(company => company.Id == id);
            return View(company);
        }

        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if (company.Id == 0)
            {
                _unitOfWork.Company.Add(company);
                TempData["success"] = "Company created successfully!";
            }
            else
            {
                _unitOfWork.Company.Update(company);
                TempData["success"] = "Company updated successfully!";
            }

            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });

        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });


        }
        #endregion
    }
}
