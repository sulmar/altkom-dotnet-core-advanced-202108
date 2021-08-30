using Altkom.Shop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shop.WebApi.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        // GET api/customers
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            IEnumerable<Customer> customers = new List<Customer>
            {
                new Customer { FirstName = "Krzysztof"},
                new Customer { FirstName = "Łukasz"},
                new Customer { FirstName = "Mateusz"},
            };

            return Ok(customers);
        }

        // GET api/customers/{id}
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            if (id > 999)
                return NotFound();

            return new Customer { FirstName = "Marcin" };
        }

    

        // POST api/customers

        // PUT api/customers/{id} - zamień

        // PATCH api/customers/{id} - zmień 

        // DELETE api/customers/{id}

        // HEAD api/customers/{id} - sprawdź czy istnieje

        // OPTIONS (CORS)
    }
}
