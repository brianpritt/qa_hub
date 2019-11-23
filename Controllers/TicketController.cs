using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QAHub.Models;
using MySql.Data.MySqlClient;
using System.Reflection;


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

        [HttpDelete]
        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM tickets;";
            cmd.ExecuteNonQuery();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
       
        
        
        
        
        [HttpPut("{id}")]
        // Put Update TICKET {id} ticket/{id}
        public void Put(int id, [FromBody]Ticket ticket)
        {
            ticket.Update(id);
        }
    }
}