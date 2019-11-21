//add create date/time, priority, timeline
//change sting TicketAutor to int TicketAuthor when users is implemented
using System;
using System.Collections.Generic;

namespace QAHub.Models
{
    public class Ticket
    {
        
        
        public int TicketId {get; set;}
        public string TicketTitle {get; set;}
        public string TicketCategory {get; set;}
        public string TicketBody {get; set;}
        public string TicketAuthor{get; set;}
        public DateTime TicketTime {get;set;}
        public DateTime TicketUpdate {get;set;}
        
        public Ticket()
        {
            
        }
        public Ticket( string ticketTitle, string ticketCategory, string ticketBody, string ticketAuthor, DateTime ticketTime)
        {
            TicketTitle = ticketTitle;
            TicketCategory = ticketCategory;
            TicketBody = ticketBody;
            TicketAuthor = ticketAuthor;
            TicketTime = ticketTime;
        }
        //Overload for sending a record that has been retrieved
        public Ticket(int ticketId, string ticketTitle, string ticketCategory, string ticketBody, string ticketAuthor, DateTime ticketTime)
        {
            TicketId = ticketId;
            TicketTitle = ticketTitle;
            TicketCategory = ticketCategory;
            TicketBody = ticketBody;
            TicketAuthor = ticketAuthor;
            TicketTime = ticketTime;
        }
    }
}