using Altkom.Shop.Fakers;
using Altkom.Shop.IServices;
using Altkom.Shop.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.Shop.FakeServices
{
    public class FakeCustomerService : ICustomerService
    {
        private readonly ICollection<Customer> customers;

        public FakeCustomerService(Faker<Customer> faker)
        {
            this.customers = faker.Generate(100);
        }

        public void Add(Customer customer)
        {
            int id = customers.Max(c => c.Id);
            customer.Id = ++id;
            customers.Add(customer);
        }

        public IEnumerable<Customer> Get()
        {
            return customers;
        }

        public Customer Get(int id)
        {
            return customers.SingleOrDefault(c => c.Id == id);
        }

        public void Remove(int id)
        {
            customers.Remove(Get(id));
        }

        public void Update(Customer customer)
        {
            var existingCustomer = Get(customer.Id);
            existingCustomer.IsRemoved = true;
        }
    }
}
