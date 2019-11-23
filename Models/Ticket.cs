//add create date/time, priority, timeline
//change sting TicketAutor to int TicketAuthor when users is implemented
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;


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
        public List<Reply> TicketReplies {get;set;}
        
        public Ticket()
        {

        }
        //overload for editing ticket
        public Ticket(string title, string category, string body)
        {   
            TicketTitle = title;
            TicketCategory = category;
            TicketBody = body;
        }
        //overload for new Ticket
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
            TicketReplies = new List<Reply>{};
        }
        public static List<Ticket> GetAll()
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
                currentTicket.TicketReplies = Reply.GetReplies(currentTicket.TicketId);
                allTickets.Add(currentTicket);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allTickets;
        }
        public static List<Ticket> GetTicket(int id)
        {
            List<Ticket> allTickets = new List<Ticket>();
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT * FROM tickets INNER JOIN replies ON tickets.ticketid = replies.ticketid WHERE tickets.ticketid = @thisId;";
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
                DateTime TicketUpdate = rdr.GetDateTime(6);
                int ReplyId = rdr.GetInt32(7);
                string ReplyAuthor = rdr.GetString(8);
                string ReplyBody = rdr.GetString(9);
                DateTime ReplyTime = rdr.GetDateTime(10);
                DateTime ReplyUpdate = rdr.GetDateTime(11);
                int ReplyTicketId = rdr.GetInt32(12);
                

                // bool contains = pricePublicList.Any(p => p.Size == 200);
                var ticket  = allTickets.Where(p => p.TicketId == TicketId).FirstOrDefault();
                if (ticket == null)
                {
                    Ticket newTicket = new Ticket(TicketId, TicketTitle,  TicketCategory, TicketBody, TicketAuthor, TicketTime);
                    Reply newReply = new Reply(ReplyId,ReplyAuthor,ReplyBody,ReplyTime,ReplyUpdate,ReplyTicketId);
                    newTicket.TicketReplies.Add(newReply);
                    allTickets.Add(newTicket);
                }
                else
                {
                    Reply newReply = new Reply(ReplyId,ReplyAuthor,ReplyBody,ReplyTime,ReplyUpdate,ReplyTicketId);
                    ticket.TicketReplies.Add(newReply);
                }
                
            }
            if (conn != null)
            {
                conn.Dispose();
            }
            conn.Close();
            return allTickets;
        }
        public void SaveTicket()
        {
            this.TicketTime = DateTime.Now;
            //Move the following into ticket class
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            
            cmd.CommandText = @" INSERT INTO tickets (tickettitle,ticketcategory,ticketbody,ticketauthor, tickettime) VALUES ( @title, @category, @body, @author, @time);";
            cmd.Parameters.AddWithValue("@title",this.TicketTitle);
            cmd.Parameters.AddWithValue("@category", this.TicketCategory);
            cmd.Parameters.AddWithValue("@body",this.TicketBody);
            cmd.Parameters.AddWithValue("@author",this.TicketAuthor);
            cmd.Parameters.AddWithValue("@time", this.TicketTime);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
        
        public void Update(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            //UPDATE test_accs
   
            
            cmd.CommandText = @"UPDATE tickets SET ticketcategory = IFNULL(@thisCategory, ticketcategory), tickettitle = IFNULL(@thisTitle, tickettitle), ticketbody = IFNULL(@thisBody, ticketbody) WHERE ticketid = @thisId;";
            cmd.Parameters.AddWithValue("@thisCategory", this.TicketCategory);
            cmd.Parameters.AddWithValue("@thisTitle", this.TicketTitle);
            cmd.Parameters.AddWithValue("@thisBody", this.TicketBody);
            cmd.Parameters.AddWithValue("@thisId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            
            // cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
                
        }
    }
}