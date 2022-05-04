using Recipes.Shared.Models;
using Recipes.Shared.Paging;
using System.Linq;

namespace Recipes.Server.Services.Interfaces
{
    public interface IRepository<T, TId>
    {
        public bool Exists(T item);
        public bool Exists(TId id);
        public bool Create(T item);
        public bool Update(T item);
        public bool Delete(TId id);
        public T GetById(TId id);
        public IQueryable<T> GetAll();
        public IQueryable<T> GetStartingWithString(string name);
        public PagedList<T> GetPage(Parameters parameters);
    }

}
