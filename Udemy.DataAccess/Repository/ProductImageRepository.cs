using Udemy.DataAccess.Data;
using Udemy.DataAccess.Repository.IRepository;
using Udemy.Models;

namespace Udemy.DataAccess.Repository
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        private ApplicationDbContext _db;
        public ProductImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductImage obj)
        {
            _db.ProductImages.Update(obj);
        }
    }
}
