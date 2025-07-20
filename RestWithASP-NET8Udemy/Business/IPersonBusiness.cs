﻿using RestWithASP_NET8Udemy.Data.VO;
using RestWithASP_NET8Udemy.Hypermedia.Utils;
using RestWithASP_NET8Udemy.Model;

namespace RestWithASP_NET8Udemy.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create (PersonVO person);
        PersonVO FindById (long id);
        List<PersonVO> FindByName(string firstName, string lastName);
        List<PersonVO> FindAll();
        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
        PersonVO Update (PersonVO person);
        PersonVO Disable(long id);
        void Delete (long id);
    }
}
