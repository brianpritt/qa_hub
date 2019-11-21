using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QAHub.Models;
using MySql.Data.MySqlClient;

// using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace QAHub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        [HttpGet]
        // GET all TICKETS /tickets
        public ActionResult<IEnumerable<Ticket>> Get(string tickettitle, string ticketcategory, string ticketbody, string ticketauthor)
        
        {
            List<Ticket> allTickets = new List<Ticket>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tickets;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int TicketId = rdr.GetInt32(0);
                string TicketTitle = rdr.GetString(1);
                string TicketCategory = rdr.GetString(2);
                string TicketBody = rdr.GetString(3);
                string TicketAuthor = rdr.GetString(4);
                DateTime TicketTime = rdr.GetDateTime(5);
                
                Ticket currentTicket = new Ticket(TicketId, TicketTitle, TicketCategory, TicketBody, TicketAuthor, TicketTime);
                allTickets.Add(currentTicket);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allTickets;
        }
         //Get TICKET with {id} /ticket/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Ticket>> GetAction(int id)
        {
            List<Ticket> allTickets = new List<Ticket>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT * FROM tickets WHERE ticketid = @thisId;";
            cmd.Parameters.AddWithValue("@thisId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int TicketId = rdr.GetInt32(0);
                string TicketTitle = rdr.GetString(1);
                string TicketCategory = rdr.GetString(2);
                string TicketBody = rdr.GetString(3);
                string TicketAuthor = rdr.GetString(4);
                DateTime TicketTime = rdr.GetDateTime(5);
                
                Ticket currentTicket = new Ticket(TicketId, TicketTitle, TicketCategory, TicketBody, TicketAuthor, TicketTime);
                allTickets.Add(currentTicket);
            }
            if (conn != null)
            {
                conn.Dispose();
            }
            return allTickets;

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
       
        
        [HttpPost]
        
        public void Post([FromBody]Ticket ticket)
        {
            Console.WriteLine(ticket.TicketTitle);
            ticket.TicketTime = DateTime.Now;
            //Move the following into ticket class
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            
            cmd.CommandText = @" INSERT INTO tickets (tickettitle,ticketcategory,ticketbody,ticketauthor, tickettime) VALUES ( @title, @category, @body, @author, @time);";
            cmd.Parameters.AddWithValue("@title",ticket.TicketTitle);
            cmd.Parameters.AddWithValue("@category", ticket.TicketCategory);
            cmd.Parameters.AddWithValue("@body",ticket.TicketBody);
            cmd.Parameters.AddWithValue("@author",ticket.TicketAuthor);
            cmd.Parameters.AddWithValue("@time", ticket.TicketTime);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
        //Post Create new TICKET /ticket
        
        [HttpPut("{id}")]
        // Put Update TICKET {id} ticket/{id}
        public void Put(int id, [FromBody]Ticket ticket)
        {
        //     var record = _db.Tickets.First(entry => entry.TicketId == id);
        //     if (record != null)
        //     {
        //         if (ticket.TicketTitle != null)
        //         {
        //             record.TicketTitle = ticket.TicketTitle;
        //         }
        //         if (ticket.TicketCategory != null)
        //         {
        //             record.TicketCategory = ticket.TicketCategory;
        //         }
        //         if (ticket.TicketBody != null)
        //         {
        //             record.TicketBody = ticket.TicketBody;
        //         }
        //         record.TicketUpdate = DateTime.Now;
        //     }
        //     _db.Entry(record).State = EntityState.Modified;
        //     _db.SaveChanges();
        }
        
    }
}