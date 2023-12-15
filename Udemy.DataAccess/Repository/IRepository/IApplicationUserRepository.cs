using Udemy.Models;

namespace Udemy.DataAccess.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        public void Update(ApplicationUser applicationUser);
    }
}
