using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Repository.IRepository
{
    public interface IProductRepository: IRepository<Product>
    {
        void Update(Product obj);
    }
}
