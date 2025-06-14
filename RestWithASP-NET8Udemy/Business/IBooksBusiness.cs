using RestWithASP_NET8Udemy.Model;

namespace RestWithASP_NET8Udemy.Business
{
    public interface IBooksBusiness
    {
        Books Create (Books books);
        Books FindById (long id);
        List<Books> FindAll();
        Books Update (Books person);
        void Delete (long id);
    }
}
