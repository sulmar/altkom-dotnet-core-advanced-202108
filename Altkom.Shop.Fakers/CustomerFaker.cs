using Altkom.Shop.Models;
using Bogus;
using System;

namespace Altkom.Shop.Fakers
{
    // PMC> Install-Package Bogus
    // dotnet add package Bogus
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.CustomerType, f => f.PickRandom<CustomerType>());
            RuleFor(p => p.Gender, f => (Gender) f.Person.Gender);
            RuleFor(p => p.DateOfBirth, f => f.Date.Past(50));
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
            RuleFor(p => p.CreatedOn, f => f.Date.Past());
            Ignore(p => p.IsSelected);
        }
    }
}
