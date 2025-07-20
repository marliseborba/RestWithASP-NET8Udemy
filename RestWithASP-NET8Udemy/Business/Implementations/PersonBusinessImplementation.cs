using Microsoft.AspNetCore.Mvc.ModelBinding;
using RestWithASP_NET8Udemy.Data.Converter.Implementations;
using RestWithASP_NET8Udemy.Data.VO;
using RestWithASP_NET8Udemy.Hypermedia.Utils;
using RestWithASP_NET8Udemy.Model;
using RestWithASP_NET8Udemy.Model.Context;
using RestWithASP_NET8Udemy.Repository;
using System;

namespace RestWithASP_NET8Udemy.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {   
            _repository = repository;
            _converter = new PersonConverter();
        }
        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var offset = page > 0 ? (page - 1) : 0;
            var sort = !string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size  = (pageSize < 1) ? 1 : pageSize;

            string query = @"select
            *
            from
            Person p
            where 1 = 1
            and p.name like '%LEO%'
            order by
            p.name asc limit 10 offset 1";

            string countQuery = "";

            var persons = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO>
            {
                CurrentPage = offset,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }
        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

    }
}
