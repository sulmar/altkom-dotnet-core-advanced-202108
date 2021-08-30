using Altkom.Shop.IServices;
using Altkom.Shop.Models;
using System;
using System.Collections.Generic;

namespace Altkom.Shop.FakeServices
{
    public class FakeCustomerService : ICustomerService
    {
        private readonly IEnumerable<Customer> customers;

        public FakeCustomerService()
        {
            this.customers = new List<Customer>
            {
                new Customer { FirstName = "Krzysztof"},
                new Customer { FirstName = "Łukasz"},
                new Customer { FirstName = "Mateusz"},
            };

        }

        public void Add(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get()
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
