// using System;
// using System.Collections.Generic;
// using MySql.Data.MySqlClient;
// using System.Linq;

// namespace QAHub.Models
// {
//     public class User
//     {
//         public int UserId {get;set;}
//         public string UserName {get;set;}
//         public string UserEmail {get;set;}
//         public string UserTeam {get;set;}
//         public DateTime UserCreatedTime {get;set;}
//         public List<Ticket> UserTickets {get;set;}
//         public List<Reply> UserReplies {get;set;}

//          public User()
//         {

//         }
//         public User(string userName, string userEmail, string userTeam, DateTime createdDate)
//         {
//             UserName = userName;
//             UserEmail = userEmail;
//             UserTeam = userTeam;
//             UserCreatedTime = createdDate;
//         }
//         public User(int userId, string userName, string userEmail, string userTeam, DateTime createdDate)
//         {
//             UserId = userId;
//             UserName = userName;
//             UserEmail = userEmail;
//             UserTeam = userTeam;
//             UserCreatedTime = createdDate;
//         }
//     }
// }