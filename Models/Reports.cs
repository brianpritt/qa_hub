using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace QAHub.Models
{
    public class Report
    {
        public static Dictionary<string, int> Get()
        {
            List<Ticket> allTickets = new List<Ticket>(){};

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
            int ticketCount = allTickets.Count;
            int userCount = 0;

            Dictionary<string, int> assign = new Dictionary<string, int>(){{"platform",0},{"ui",0},{"data",0},{"unassigned",0},{"ticketcout",ticketCount}, {"usercount", userCount}};
            
            foreach (var tick in allTickets)
            {
                assign[tick.TicketCategory.ToLower()] +=1;    
            }
            return assign;
        } 
    }
}