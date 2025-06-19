using RestWithASP_NET8Udemy.Data.VO;
using RestWithASP_NET8Udemy.Model;

namespace RestWithASP_NET8Udemy.Business
{
    public interface IBookBusiness
    {
        BookVO Create (BookVO books);
        BookVO FindById (long id);
        List<BookVO> FindAll();
        BookVO Update (BookVO person);
        void Delete (long id);
    }
}
