using BulkyBook.Data;
using BulkyBook.Repository.IRepository;

namespace BulkyBook.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }

        ICategoryRepository IUnitOfWork.Category => throw new NotImplementedException();

        public UnitOfWork(ApplicationDbContext db) {
            _db = db;
            Category = new CategoryRepository(_db);
        }

        void IUnitOfWork.Save()
        {
            _db.SaveChanges();
        }
    }
}
