//add create date/time, priority, timeline
//change sting TicketAutor to int TicketAuthor when users is implemented
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
        
    }
}