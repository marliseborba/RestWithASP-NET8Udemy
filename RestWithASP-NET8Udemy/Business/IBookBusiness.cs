using RestWithASP_NET8Udemy.Data.VO;
using RestWithASP_NET8Udemy.Hypermedia.Utils;
using RestWithASP_NET8Udemy.Model;

namespace RestWithASP_NET8Udemy.Business
{
    public interface IBookBusiness
    {
        BookVO Create (BookVO books);
        BookVO FindById (long id);
        List<BookVO> FindByName(string title);
        List<BookVO> FindAll();
        PagedSearchVO<BookVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
        BookVO Update (BookVO person);
        void Delete (long id);
    }
}
