using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;

namespace QAHub.Models
{
    public class User
    {
        public int UserId {get;set;}
        public string UserName {get;set;}
        public string UserEmail {get;set;}
        public string UserTeam {get;set;}
        public DateTime UserCreatedTime {get;set;}
        public List<Ticket> UserTickets {get;set;}
        public List<Reply> UserReplies {get;set;}
        public static string[] teams = {"platform", "ui", "data", "qa", "sales", "support", "unassigned"};

        public User()
        {

        }
        public User(string userName, string userEmail, string userTeam, DateTime createdDate)
        {
            UserName = userName;
            UserEmail = userEmail;
            UserTeam = userTeam;
            UserCreatedTime = createdDate;
        }
        public User(int userId, string userName, string userEmail, string userTeam, DateTime createdDate)
        {
            UserId = userId;
            UserName = userName;
            UserEmail = userEmail;
            UserTeam = userTeam;
            UserCreatedTime = createdDate;
            UserTickets = new List<Ticket>{};
            UserReplies = new List<Reply>{};
        }
        public static List<User> Get()
        {
            List<User> allUsers = new List<User>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT * FROM users";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int UserId = rdr.GetInt32(0);
                string UserName = rdr.GetString(1);
                string UserEmail = rdr.GetString(2);
                string UserTeam = rdr.GetString(3);
                DateTime UserCreatedTime = rdr.GetDateTime(4);
                User newUser = new User(UserId,UserName,UserEmail,UserTeam,UserCreatedTime);
                allUsers.Add(newUser);
            }
            return allUsers;
        }
        public static List<User> GetUser(int id)
        {
            List<User> allUsers = new List<User>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM users INNER JOIN tickets ON users.userid = tickets.ticketauthor INNER JOIN replies ON users.userid = replies.replyauthor WHERE users.userid = @thisId;";
            cmd.Parameters.AddWithValue("@thisId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int UserId = rdr.GetInt32(0);
                string UserName = rdr.GetString(1);
                string UserEmail = rdr.GetString(2);
                string UserTeam = rdr.GetString(3);
                DateTime UserCreatedTime = rdr.GetDateTime(4);
                int TicketId = rdr.GetInt32(5);
                string TicketTitle = rdr.GetString(6);
                string TicketCategory = rdr.GetString(7);
                string TicketBody = rdr.GetString(8);
                int TicketAuthor = rdr.GetInt32(9);
                DateTime TicketTime = rdr.GetDateTime(10);
                DateTime TicketUpdate = rdr.GetDateTime(11);
                int ReplyId = rdr.GetInt32(12);
                int ReplyAuthor = rdr.GetInt32(13);
                string ReplyBody = rdr.GetString(14);
                DateTime ReplyTime = rdr.GetDateTime(15);
                DateTime ReplyUpdate = rdr.GetDateTime(16);
                int RTicketId = rdr.GetInt32(17);
                
                var user = allUsers.Where(p => p.UserId == UserId).FirstOrDefault();
                if (user == null)
                {
                    
                    User newUser = new User(UserId,UserName,UserEmail,UserTeam,UserCreatedTime);
                    Ticket newTicket = new Ticket(TicketId,TicketTitle,TicketCategory,TicketBody,TicketAuthor,TicketTime, TicketUpdate);

                    newUser.UserTickets.Add(newTicket);

                    Reply newReply = new Reply(ReplyId,ReplyAuthor,ReplyBody,ReplyTime,ReplyUpdate,RTicketId);

                    newUser.UserReplies.Add(newReply);
                    allUsers.Add(newUser);
                }
                else
                {
                    Ticket newTicket = new Ticket(TicketId,TicketTitle,TicketCategory,TicketBody,TicketAuthor,TicketTime, TicketUpdate);
                    user.UserTickets.Add(newTicket);
                    Reply newReply = new Reply(ReplyId,ReplyAuthor,ReplyBody,ReplyTime,ReplyUpdate,RTicketId);
                    user.UserReplies.Add(newReply);
                }
            }
            if (conn != null)
            {
                conn.Dispose();
            }
            conn.Close();
            Console.WriteLine(allUsers.Count);
            return allUsers;
        }
        public void SaveUser()
        {
            this.UserCreatedTime = DateTime.Now;
            this.CheckTeam();

            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"INSERT INTO users (username, useremail, userteam, usercreatedtime) VALUES (@name,@email, @team, @time);";
            cmd.Parameters.AddWithValue("@name", this.UserName);
            cmd.Parameters.AddWithValue("@email",this.UserEmail);
            cmd.Parameters.AddWithValue("@team", this.UserTeam);
            cmd.Parameters.AddWithValue("@time", this.UserCreatedTime);

            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
        public void Update(int id)
        {
            this.CheckTeam();
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"Update users SET username = IFNULL(@thisName, username), useremail = IFNULL(@thisEmail, useremail), userteam = IFNULL(@thisTeam, userteam) WHERE userid = @thisId;";
            cmd.Parameters.AddWithValue("@thisName", this.UserName);
            cmd.Parameters.AddWithValue("@thisEmail",this.UserEmail);
            cmd.Parameters.AddWithValue("@thisTeam",this.UserTeam);
            cmd.Parameters.AddWithValue("@thisId", id);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            } 
        }
        public void CheckTeam()
        {
            //IndexOf returns -1 if array does not contain element
            int position = Array.IndexOf(User.teams, this.UserTeam.ToLower());
            if (position < 0)
            {
                this.UserTeam = "unassigned";
            }
        }
    }
}