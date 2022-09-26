using DiningHall.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiningHall.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddOrderController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            Utility.FinishedOrders.Add(order);
            return Ok();
        }
    }
}
