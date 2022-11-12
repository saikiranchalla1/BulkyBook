using BulkyBook.Data;
using BulkyBook.Models;
using BulkyBook.Repository.IRepository;

namespace BulkyBook.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        void ICategoryRepository.Save()
        {
            _db.SaveChanges();
        }

        void ICategoryRepository.Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
