using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace QAHub.Models
{
    public class Reply
    {
        public int ReplyId {get;set;}
        public string ReplyAuthor {get; set;}
        public string ReplyBody {get;set;}
        public DateTime ReplyTime {get; set;}
        public DateTime ReplyUpdate {get;set;}
        public int TicketId {get;set;}
        
    
        public Reply()
        {

        }
        public Reply(string replyAuthor, string replyBody,DateTime replyTime)
        {
            ReplyAuthor=replyAuthor;
            ReplyBody = replyBody;
            ReplyTime = replyTime;
        }
        public Reply(int replyId,string replyAuthor, string replyBody,DateTime replyTime, DateTime replyUpdate, int ticketId)
        {
            ReplyId = replyId;
            ReplyAuthor = replyAuthor;
            ReplyBody = replyBody;
            ReplyTime = replyTime;
            ReplyUpdate = ReplyUpdate;
            TicketId = ticketId;
        }
        //This method would be much faster if i used a join table instead, but you know, quick and dirty.
        public static List<Reply> GetReplies(int id)
        {
            List<Reply> allReplies = new List<Reply>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT * FROM replies WHERE ticketid = @thisId;";
            cmd.Parameters.AddWithValue("@thisId", id);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            
                while (rdr.Read())
                {
                    int ReplyId = rdr.GetInt32(0);
                    string ReplyAuthor = rdr.GetString(1);
                    string ReplyBody = rdr.GetString(2);
                    DateTime ReplyTime = rdr.GetDateTime(3);
                    DateTime ReplyUpdate = rdr.GetDateTime(4);
                    int TicketId = rdr.GetInt32(5);

                    Reply currentReply = new Reply(ReplyId, ReplyAuthor, ReplyBody, ReplyTime, ReplyUpdate, TicketId);
                    allReplies.Add(currentReply);
                
            }
            if (conn != null)
            {
                conn.Dispose();
            }
            return allReplies;
        }
    }
}