using Altkom.Shop.IServices;
using Altkom.Shop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;

namespace Altkom.Shop.WebApi.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET api/customers
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            var customers = customerService.Get();

            return Ok(customers);
        }

        // GET api/customers/{id}
        [HttpGet("{id}", Name = "GetCustomerById")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Customer> Get( int id)
        {
            Customer customer = customerService.Get(id);

            if (customer==null)
                return NotFound();

            return Ok(customer);
        }

        // POST api/customers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public IActionResult Post(Customer customer)
        {
            customerService.Add(customer);

            return CreatedAtRoute("GetCustomerById", new { Id = customer.Id }, customer);
        }

        // PUT api/customers/{id} - zamień

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Put(int id, Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();
            
            customerService.Update(customer);

            return NoContent();
        }

        // http://jsonpatch.com/
        // PATCH api/customers/{id} - zmień 
        // dotnet add package Microsoft.AspNetCore.JsonPatch
        // content-type: application/json-patch+json

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Patch(int id, JsonPatchDocument<Customer> patchCustomer)
        {
            Customer customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            patchCustomer.ApplyTo(customer, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();

        }


        // DELETE api/customers/{id}

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Delete(int id)
        {
            Customer customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            customerService.Remove(id);

            return Ok();
        }

        // HEAD api/customers/{id} - sprawdź czy istnieje
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Head(int id)
        {
            Customer customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            return Ok();
        }

        // OPTIONS (CORS)
    }
}
