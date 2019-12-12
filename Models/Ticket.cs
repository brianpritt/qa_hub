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
        public int TicketAuthor{get; set;}
        public DateTime TicketTime {get;set;}
        public DateTime TicketUpdate {get;set;}
        public List<Reply> TicketReplies {get;set;}
        public static string[] Assignments = {"platform", "ui", "data", "unassigned"};
        
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
        public Ticket( string ticketTitle, string ticketCategory, string ticketBody, int ticketAuthor, DateTime ticketTime)
        {
            TicketTitle = ticketTitle;
            TicketCategory = ticketCategory;
            TicketBody = ticketBody;
            TicketAuthor = ticketAuthor;
            TicketTime = ticketTime;
        }
        //Overload for sending a record that has been retrieved 
        public Ticket(int ticketId, string ticketTitle, string ticketCategory, string ticketBody, int ticketAuthor, DateTime ticketTime, DateTime ticketUpdate)
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
        public static List<Ticket> GetAll(string assignment)
        {
            List<Ticket> allTickets = new List<Ticket>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            if (assignment == null)
            {
                cmd.CommandText = @"SELECT * FROM tickets;";
            }
            else 
            {
                cmd.CommandText = @"SELECT * FROM tickets WHERE ticketcategory = @assignment;";
                cmd.Parameters.AddWithValue("@assignment", assignment);
            }
            
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int TicketId = rdr.GetInt32(0);
                string TicketTitle = rdr.GetString(1);
                string TicketCategory = rdr.GetString(2);
                string TicketBody = rdr.GetString(3);
                int TicketAuthor = rdr.GetInt32(4);
                DateTime TicketTime = rdr.GetDateTime(5);
                DateTime TicketUpdate =rdr.GetDateTime(6);
                
                Ticket currentTicket = new Ticket(TicketId, TicketTitle, TicketCategory, TicketBody, TicketAuthor, TicketTime, TicketUpdate);
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

            cmd.CommandText = @"SELECT * FROM tickets LEFT JOIN replies ON tickets.ticketid = replies.ticketid WHERE tickets.ticketid = @thisId;";
            cmd.Parameters.AddWithValue("@thisId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            
            while (rdr.Read())
            {
                
                // Console.WriteLine(rdr.GetBytes(0));
                int TicketId = rdr.GetInt32(0);
                string TicketTitle = rdr.GetString(1);
                string TicketCategory = rdr.GetString(2);
                string TicketBody = rdr.GetString(3);
                int TicketAuthor = rdr.GetInt32(4);
                DateTime TicketTime = rdr.GetDateTime(5);
                DateTime TicketUpdate = rdr.GetDateTime(6);
                //This if was put in place to prevent DBNull errors
                if (rdr["replyid"] != DBNull.Value){
                    var ReplyId = rdr.GetInt32(7);
                    int ReplyAuthor = rdr.GetInt32(8);
                    string ReplyBody = rdr.GetString(9);
                    DateTime ReplyTime = rdr.GetDateTime(10);
                    DateTime ReplyUpdate = rdr.GetDateTime(11);
                    int ReplyTicketId = rdr.GetInt32(12);
                    
                    var ticket  = allTickets.Where(p => p.TicketId == TicketId).FirstOrDefault();
                    //if there is no ticket in the list, create it here, otherwise skip to populating replies
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
                else
                {
                 Ticket newTicket = new Ticket(TicketId, TicketTitle,  TicketCategory, TicketBody, TicketAuthor, TicketTime, TicketUpdate);  
                 allTickets.Add(newTicket); 
                }
            }
            if (conn != null)
            {
                conn.Dispose();
            }
            conn.Close();
            
            return allTickets;
        }
        public string SaveTicket()
        {
            this.TicketTime = DateTime.Now;
            this.TicketUpdate = DateTime.Now;
            this.CheckAssignment();
            if (this.TicketAuthor == 0 || this.TicketBody == null || this.TicketTitle == null)
            {
                return "Not all fields have been supplied";
            }
            
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
            return "Success.";
        }
        public void Update(int id)
        {
            this.TicketUpdate = DateTime.Now;
            this.CheckAssignment();
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            //UPDATE test_accs
            
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

            //This makes two queries to the DB. Had to use joins because of foreign key constraints.  Is there a better method to do both?

            cmd.CommandText = @"DELETE FROM tickets, replies USING tickets INNER JOIN replies WHERE tickets.ticketid = @thisId AND tickets.ticketid = replies.ticketid; DELETE a FROM tickets a LEFT JOIN replies b ON a.ticketid = b.ticketid WHERE a.ticketid = @thisId AND b.ticketid is null;";
            cmd.Parameters.AddWithValue("@thisid", id);

            cmd.ExecuteNonQuery();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void CheckAssignment()
        {
            //check if user supplied category is in the accepted list.
            int position = Array.IndexOf(Ticket.Assignments, this.TicketCategory.ToLower());
            if (position < 0 )
            {
                this.TicketCategory = "unassigned";
            }
        }
    }
}