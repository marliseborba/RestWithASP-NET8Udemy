using RestWithASP_NET8Udemy.Model;

namespace RestWithASP_NET8Udemy.Business
{
    public interface IBookBusiness
    {
        Book Create (Book books);
        Book FindById (long id);
        List<Book> FindAll();
        Book Update (Book person);
        void Delete (long id);
    }
}
