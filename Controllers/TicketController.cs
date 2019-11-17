using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QAHub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        [HttpGet]
        // GET /ticket
        public void Get()
        {
            Console.WriteLine("Api Working Successfully!");
        }    
        
    }
}