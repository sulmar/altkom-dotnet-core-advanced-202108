using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Shop.Models.SearchCriterias
{

    public abstract class SearchCriteria : Base
    {

    }

    public class CustomerSearchCritiera : SearchCriteria
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
    }
}
