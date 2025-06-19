using Microsoft.AspNetCore.Mvc.ModelBinding;
using RestWithASP_NET8Udemy.Model;
using RestWithASP_NET8Udemy.Model.Context;
using RestWithASP_NET8Udemy.Repository;
using System;

namespace RestWithASP_NET8Udemy.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;

        public PersonBusinessImplementation(IRepository<Person> repository)
        {   
            _repository = repository;
        }
        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }
        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        } 
    }
}
