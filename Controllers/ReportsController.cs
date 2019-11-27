using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QAHub.Models;

//controller for generating reports | not much to see here.
namespace QAHub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
       [HttpGet]
       public IActionResult Get()
       {
           return Ok(Report.Get());
       }

    }
}