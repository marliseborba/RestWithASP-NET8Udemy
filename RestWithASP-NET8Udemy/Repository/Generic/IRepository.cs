using RestWithASP_NET8Udemy.Model;
using RestWithASP_NET8Udemy.Model.Base;

namespace RestWithASP_NET8Udemy.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create (T item);
        T FindById (long id);
        List<T> FindAll();
        T Update (T item);
        void Delete (long id);
        bool Exists(long id);
    }
}
