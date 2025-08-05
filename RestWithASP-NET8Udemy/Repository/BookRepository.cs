using RestWithASP_NET8Udemy.Model;
using RestWithASP_NET8Udemy.Model.Context;
using RestWithASP_NET8Udemy.Repository.Generic;

namespace RestWithASP_NET8Udemy.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(MySQLContext context) : base(context) { }
        public List<Book> FindByName(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                return _context.Books.Where(p => p.Title.Contains(title)).ToList();
            }
            return null;
        }
    }
}
