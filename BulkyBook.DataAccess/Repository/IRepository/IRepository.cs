using System.Linq.Expressions;

namespace BulkyBook.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T - Category
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);

        IEnumerable<T> GetAll();

        void Add(T item);

        void Remove(T item);

        void RemoveRange(IEnumerable<T> items);
    }
}
