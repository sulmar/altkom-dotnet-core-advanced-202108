using Altkom.Shop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shop.WebApi.Controllers
{
    [Route("api/orders")]
    public class OrdersController
    {
        [HttpGet("~/api/customers/{customerId}/orders")]
        public ActionResult<Order> Get(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
