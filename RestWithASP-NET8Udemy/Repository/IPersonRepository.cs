using RestWithASP_NET8Udemy.Data.VO;
using RestWithASP_NET8Udemy.Model;

namespace RestWithASP_NET8Udemy.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
        List<Person> FindByName(string firstName, string secondName);
    }
}
