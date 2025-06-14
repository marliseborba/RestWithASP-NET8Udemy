using RestWithASP_NET8Udemy.Model;

namespace RestWithASP_NET8Udemy.Repository
{
    public interface IBooksRepository
    {
        Books Create (Books books);
        Books FindById (long id);
        List<Books> FindAll();
        Books Update (Books books);
        void Delete (long id);
        bool Exists(long id);
    }
}
