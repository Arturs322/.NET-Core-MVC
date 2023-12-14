using Microsoft.AspNetCore.Mvc.Rendering;

namespace Udemy.Models.ViewModels
{
    public class UserManagerVM
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
        public IEnumerable<SelectListItem> CompanyList { get; set; }

    }
}
