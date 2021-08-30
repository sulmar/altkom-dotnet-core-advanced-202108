using Altkom.Shop.Models;
using Bogus;

namespace Altkom.Shop.Fakers
{
    public class AddressFaker : Faker<Address>
    {
        public AddressFaker()
        {
            RuleFor(p => p.City, f => f.Address.City());
            RuleFor(p => p.Street, f => f.Address.StreetName());
            RuleFor(p => p.ZipCode, f => f.Address.ZipCode());
        }
    }
}
