using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QAHub.Models;

namespace QAHub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
       [HttpGet]
       public ActionResult Get()
       {
           
       }

    }
}