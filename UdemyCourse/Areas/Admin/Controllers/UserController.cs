using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Udemy.DataAccess.Repository.IRepository;
using Udemy.Models;
using Udemy.Models.ViewModels;
using Udemy.Utilities;

namespace UdemyCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        public UserManagerVM UserManagerVM { get; private set; }

        public UserController(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Permission(string userId)
        {
            UserManagerVM userManagerVM = new UserManagerVM()
            {
                ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId),
                CompanyList = _unitOfWork.Company.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                RoleList = _roleManager.
                Roles.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Name,
                }),
            };

            userManagerVM.ApplicationUser.Role = _userManager.
                GetRolesAsync(_unitOfWork.ApplicationUser.Get(u => u.Id == userId)).GetAwaiter().GetResult().FirstOrDefault();

            return View(userManagerVM);

        }

        [HttpPost]
        public IActionResult Permission(UserManagerVM userManagerVM)
        {
            string oldRole = _userManager.
                GetRolesAsync(_unitOfWork.ApplicationUser.
                Get(u => u.Id == userManagerVM.ApplicationUser.Id)).
                GetAwaiter().GetResult().FirstOrDefault();

            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userManagerVM.ApplicationUser.Id);

            if (!(userManagerVM.ApplicationUser.Role == oldRole))
            {
                if (userManagerVM.ApplicationUser.Role == SD.Role_Company)
                {
                    applicationUser.CompanyId = userManagerVM.ApplicationUser.CompanyId;
                }
                if (oldRole == SD.Role_Company)
                {
                    applicationUser.CompanyId = null;
                }

                _unitOfWork.ApplicationUser.Update(applicationUser);
                _unitOfWork.Save();

                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, userManagerVM.ApplicationUser.Role).GetAwaiter().GetResult();
            }
            else
            {
                if (oldRole == SD.Role_Company && applicationUser.CompanyId != userManagerVM.ApplicationUser.CompanyId)
                {
                    applicationUser.CompanyId = userManagerVM.ApplicationUser.CompanyId;
                    _unitOfWork.ApplicationUser.Update(applicationUser);
                    _unitOfWork.Save();
                }
            }

            return RedirectToAction("Index");

        }


        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> objUserList = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company").ToList();

            foreach (var user in objUserList)
            {
                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
                if (user.Company == null)
                {
                    user.Company = new() { Name = "" };
                }
            }
            return Json(new { data = objUserList });

        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objFromDb = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }

            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }

            _unitOfWork.ApplicationUser.Update(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
