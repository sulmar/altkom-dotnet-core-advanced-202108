using Altkom.Shop.Fakers;
using Altkom.Shop.IServices;
using Altkom.Shop.Models;
using Altkom.Shop.Models.SearchCriterias;
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

        public IEnumerable<Customer> Get(CustomerSearchCritiera searchCritiera)
        {
            var query = customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchCritiera.City))
            {
                query = query.Where(c => c.ShipAddress.City == searchCritiera.City);
            }

            if (!string.IsNullOrEmpty(searchCritiera.Street))
            {
                query = query.Where(c => c.ShipAddress.Street == searchCritiera.Street);
            }

            if (!string.IsNullOrEmpty(searchCritiera.ZipCode))
            {
                query = query.Where(c => c.ShipAddress.ZipCode == searchCritiera.ZipCode);
            }

            return query.ToList();

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
