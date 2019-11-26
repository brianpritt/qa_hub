using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net;
using QAHub.Models;
//returns added to previous void methods for future error reporting.
namespace QAHub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        [HttpGet]
        // GET all TICKETS /tickets
        public ActionResult<IEnumerable<Ticket>> Get()
        {
           return Ticket.GetAll();
        }

         //Get TICKET with {id} /ticket/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            List<Ticket> responseTicket =Ticket.GetTicket(id);
            if (responseTicket.Count == 0)
            {
                return StatusCode(204);

            }
            return new ObjectResult(Ticket.GetTicket(id));
        }
      
        //Post Create new TICKET /ticket
        [HttpPost]
        public ActionResult Post([FromBody]Ticket ticket)
        {
            ticket.SaveTicket();
            return StatusCode(201);
        }
        
        [HttpPut("{id}/update")]
        // Put Update TICKET {id} ticket/{id}/update
        // 
        public ActionResult Put(int id, [FromBody]Ticket ticket)
        {
            ticket.Update(id);
            return StatusCode(201);
        }
        // we dont want a delete all, just a /delete/{id}
        [HttpDelete("{id}/delete")]
        public ActionResult Delete(int id)
        {
            Ticket.DeleteTicket(id);
            return StatusCode(200);
        }
        [HttpDelete]
        public void DeleteAll()
        {
            //Endpoint not utilized.
        }
    }
}