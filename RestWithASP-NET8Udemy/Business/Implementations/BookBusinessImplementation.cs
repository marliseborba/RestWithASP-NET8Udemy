using Microsoft.AspNetCore.Mvc.ModelBinding;
using RestWithASP_NET8Udemy.Model;
using RestWithASP_NET8Udemy.Model.Context;
using RestWithASP_NET8Udemy.Repository;
using System;

namespace RestWithASP_NET8Udemy.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;

        public BookBusinessImplementation(IRepository<Book> repository)
        {   
            _repository = repository;
        }
        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Book Create(Book books)
        {
            return _repository.Create(books);
        }
        public Book Update(Book books)
        {
            return _repository.Update(books);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        } 
    }
}
