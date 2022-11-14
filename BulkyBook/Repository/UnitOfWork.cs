using BulkyBook.Data;
using BulkyBook.Repository.IRepository;

namespace BulkyBook.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }

        public IProductRepository Product { get; private set; }

        public UnitOfWork(ApplicationDbContext db) {
            _db = db;

            Category = new CategoryRepository(_db);

            CoverType = new CoverTypeRepository(_db);

            Product = new ProductRepository(_db);
        }

        void IUnitOfWork.Save()
        {
            _db.SaveChanges();
        }
    }
}
