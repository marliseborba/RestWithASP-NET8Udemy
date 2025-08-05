using RestWithASP_NET8Udemy.Model;

namespace RestWithASP_NET8Udemy.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        List<Book> FindByName(string title);
    }
}
