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
        public static string[] Assignments = {"Platform", "UI", "Data", "Unassigned"};
        
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
        public Ticket(int ticketId, string ticketTitle, string ticketCategory, string ticketBody, string ticketAuthor, DateTime ticketTime, DateTime ticketUpdate)
        {
            TicketId = ticketId;
            TicketTitle = ticketTitle;
            TicketCategory = ticketCategory;
            TicketBody = ticketBody;
            TicketAuthor = ticketAuthor;
            TicketTime = ticketTime;
            TicketUpdate = ticketUpdate;
            TicketReplies = new List<Reply>{};
        }
        //should get all pull all of the replies as well?
        // replace .GetReplies() with newest method.
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
                DateTime TicketUpdate =rdr.GetDateTime(6);
                
                Ticket currentTicket = new Ticket(TicketId, TicketTitle, TicketCategory, TicketBody, TicketAuthor, TicketTime, TicketUpdate);
                // currentTicket.TicketReplies = Reply.GetReplies(currentTicket.TicketId);
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
                
                var ticket  = allTickets.Where(p => p.TicketId == TicketId).FirstOrDefault();
                if (ticket == null)
                {
                    Ticket newTicket = new Ticket(TicketId, TicketTitle,  TicketCategory, TicketBody, TicketAuthor, TicketTime, TicketUpdate);
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
            this.TicketUpdate = DateTime.Now;
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            
            cmd.CommandText = @"INSERT INTO tickets (tickettitle,ticketcategory,ticketbody,ticketauthor, tickettime, ticketupdate) VALUES ( @title, @category, @body, @author, @time, @update);";
            cmd.Parameters.AddWithValue("@title",this.TicketTitle);
            cmd.Parameters.AddWithValue("@category", this.TicketCategory);
            cmd.Parameters.AddWithValue("@body",this.TicketBody);
            cmd.Parameters.AddWithValue("@author",this.TicketAuthor);
            cmd.Parameters.AddWithValue("@time", this.TicketTime);
            cmd.Parameters.AddWithValue("@update", this.TicketUpdate);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
        public void Update(int id)
        {
            this.TicketUpdate = DateTime.Now;
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            //UPDATE test_accs
            Console.WriteLine(this.TicketUpdate);
            
            cmd.CommandText = @"UPDATE tickets SET ticketcategory = IFNULL(@thisCategory, ticketcategory), tickettitle = IFNULL(@thisTitle, tickettitle), ticketbody = IFNULL(@thisBody, ticketbody), ticketupdate = @thisUpdateTime WHERE ticketid = @thisId;";
            cmd.Parameters.AddWithValue("@thisUpdateTime", this.TicketUpdate);
            cmd.Parameters.AddWithValue("@thisCategory", this.TicketCategory);
            cmd.Parameters.AddWithValue("@thisTitle", this.TicketTitle);
            cmd.Parameters.AddWithValue("@thisBody", this.TicketBody);
            cmd.Parameters.AddWithValue("@thisId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }   
        }

        //Deletes Ticket and all replies associated with it.
        public static void DeleteTicket(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            //Foreign Key on replies set to Cascade, it fet a little dirty to do it.
            cmd.CommandText = @"DELETE FROM tickets, replies USING tickets INNER JOIN replies WHERE tickets.ticketid = @thisId AND tickets.ticketid = replies.ticketid OR tickets.ticketid = @thisId;";
            cmd.Parameters.AddWithValue("@thisid", id);

            cmd.ExecuteNonQuery();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void CheckAssignment()
        {
            // bool a = Array.Exists(Ticket.Assignments, this.TicketCategory);
            // Console.WriteLine(this.TicketCategory)
        }
    }
}