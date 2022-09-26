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
    public class StartSimulationController : ControllerBase
    {

        [HttpPost]
        public void StartSimulation(int numberOfTables)
        {
            var simulation = new Simulation(numberOfTables);
            simulation.RunSimulation();
        }

    }
}
