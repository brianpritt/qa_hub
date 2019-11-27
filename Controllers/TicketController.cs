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
        public ActionResult<IEnumerable<Ticket>> Get(string assignment)
        {
           return Ticket.GetAll(assignment);
        }

         //Get TICKET with {id} /ticket/{id}
        [HttpGet("{id}")]
        public ActionResult <IEnumerable<Ticket>> Get(int id)
        {
            List<Ticket> responseTicket =Ticket.GetTicket(id);
            return Ticket.GetTicket(id);
        }
      
        //Post Create new TICKET /ticket/new
        [HttpPost]
        public ActionResult Post([FromBody]Ticket ticket)
        {
            string ret = ticket.SaveTicket();
            return Ok(ret);
        }
        
        [HttpPut("{id}/update")]
        // Put Update TICKET {id} ticket/{id}/update
        // 
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