using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QAHub.Models;



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
        public List<Ticket> GetAction(int id)
        {
            return Ticket.GetTicket(id);
        }

        //Post Create new TICKET /ticket
        [HttpPost]
        public void Post([FromBody]Ticket ticket)
        {
            ticket.SaveTicket();
        }
        
        [HttpPut("{id}/update")]
        // Put Update TICKET {id} ticket/{id}/update
        public void Put(int id, [FromBody]Ticket ticket)
        {
            ticket.Update(id);
        }
        // we dont want a delete all, just a /delete/{id}
        [HttpDelete("{id}/delete")]
        public void Delete(int id)
        {
            Ticket.DeleteTicket(id);
        }
        [HttpDelete]
        public void DeleteAll()
        {
            //Endpoint not utilized.
        }
    }
}