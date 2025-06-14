using RestWithASP_NET8Udemy.Model;

namespace RestWithASP_NET8Udemy.Repository
{
    public interface IBookRepository
    {
        Book Create (Book books);
        Book FindById (long id);
        List<Book> FindAll();
        Book Update (Book books);
        void Delete (long id);
        bool Exists(long id);
    }
}
