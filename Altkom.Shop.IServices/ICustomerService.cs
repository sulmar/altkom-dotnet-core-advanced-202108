using Altkom.Shop.Models;
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

    }
}
