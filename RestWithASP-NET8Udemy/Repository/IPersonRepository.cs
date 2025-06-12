using RestWithASP_NET8Udemy.Model;

namespace RestWithASP_NET8Udemy.Repository
{
    public interface IPersonRepository
    {
        Person Create (Person person);
        Person FindById (long id);
        List<Person> FindAll();
        Person Update (Person person);
        void Delete (long id);
        bool Exists(long id);
    }
}
