using BulkyBook.Models;

namespace BulkyBook.Repository.IRepository
{
    public interface ICategoryRepository: IRepository<Category>
    {
        void Update(Category category);
    }
}
