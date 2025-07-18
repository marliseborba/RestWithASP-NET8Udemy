using RestWithASP_NET8Udemy.Data.VO;

namespace RestWithASP_NET8Udemy.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create (PersonVO person);
        PersonVO FindById (long id);
        List<PersonVO> FindAll();
        PersonVO Update (PersonVO person);
        PersonVO Disable(long id);
        void Delete (long id);
    }
}
