using Altkom.Shop.Models;
using Altkom.Shop.Models.SearchCriterias;
using System;
using System.Collections.Generic;

namespace Altkom.Shop.IServices
{
    public interface ICustomerService
    {
        IEnumerable<Customer> Get();
        Customer Get(int id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Remove(int id);

        // IEnumerable<Customer> Get(string city, string street, string zipcode);

        IEnumerable<Customer> Get(CustomerSearchCritiera searchCritiera);

    }
}
