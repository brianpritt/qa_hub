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
        public static List<Reply> GetAll()
        {
            List<Reply> allReplies = new List<Reply>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT * FROM replies;";
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
        public static List<Reply> GetReply(int id)
        {
            List<Reply> allReplies = new List<Reply>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT * FROM replies WHERE replyid = @thisId;";
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
        public void SaveReply(int id)
        {
            this.ReplyTime = DateTime.Now;
            this.ReplyUpdate = DateTime.Now;
            Console.WriteLine(DateTime.Now);

            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"INSERT INTO replies (replyauthor,replybody, replytime, replyupdate, ticketid) VALUES (@author, @body, @replytime, @updatetime, @ticketId);";

            cmd.Parameters.AddWithValue("@author", this.ReplyAuthor);
            cmd.Parameters.AddWithValue("@body", this.ReplyBody);
            cmd.Parameters.AddWithValue("@replytime", this.ReplyTime);
            cmd.Parameters.AddWithValue("@updatetime", this.ReplyUpdate);
            cmd.Parameters.AddWithValue("@ticketId", id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void Update(int id)
        {
            this.ReplyUpdate = DateTime.Now;
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"Update replies SET replybody = IFNULL(@thisBody, replybody), replyupdate = @thisUpdateTime WHERE replyid = @thisId;";

            cmd.Parameters.AddWithValue("@thisBody", this.ReplyBody);
            cmd.Parameters.AddWithValue("@thisUpdateTime", this.ReplyUpdate);
            cmd.Parameters.AddWithValue("thisId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            } 
        }
        public static void Delete(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM replies WHERE replyid = @relyId;";
            cmd.Parameters.AddWithValue("@replyId", id);
            cmd.ExecuteNonQuery();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}