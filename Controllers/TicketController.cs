using System.Collections.Generic;
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
        public ActionResult<IEnumerable<Ticket>> Get(string assignment)
        {
           return Ticket.GetAll(assignment);
        }

         //Get TICKET with {id} /ticket/{id}
        [HttpGet("{id}")]
        public ActionResult <IEnumerable<Ticket>> Get(int id)
        {
            return Ticket.GetTicket(id);
        }
      
        //Post Create new TICKET /ticket/new
        [HttpPost]
        public void Post([FromBody]Ticket ticket)
        {
            ticket.SaveTicket();
            
        }
        
        // Put Update TICKET {id} ticket/{id}/update
        [HttpPut("{id}/update")]
        public void Put(int id, [FromBody]Ticket ticket)
        {
            ticket.Update(id);
        }
        //DELETE Ticket {id} ticket/{id}/delete
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