using Microsoft.AspNetCore.Mvc.ModelBinding;
using RestWithASP_NET8Udemy.Model;
using RestWithASP_NET8Udemy.Model.Context;
using RestWithASP_NET8Udemy.Repository;
using System;

namespace RestWithASP_NET8Udemy.Business.Implementations
{
    public class BooksBusinessImplementation : IBooksBusiness
    {
        private readonly IBooksRepository _repository;

        public BooksBusinessImplementation(IBooksRepository repository)
        {   
            _repository = repository;
        }
        public List<Books> FindAll()
        {
            return _repository.FindAll();
        }

        public Books FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Books Create(Books books)
        {
            return _repository.Create(books);
        }
        public Books Update(Books books)
        {
            return _repository.Update(books);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        } 
    }
}
