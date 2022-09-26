using DiningHall.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiningHall.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogInfoController : ControllerBase
    {
        private ILogger _logger;
        public LogInfoController(ILogger<LogInfoController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public IActionResult LogInfo(LogInfoDto message)
        {

            _logger.LogInformation($"{message.Message}");
            return Ok();
        }
    }
}
