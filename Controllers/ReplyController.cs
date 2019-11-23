using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QAHub.Models;
using MySql.Data.MySqlClient;

using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace QAHub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        
        [HttpGet]
        //useless route
        public ActionResult<IEnumerable<Reply>> Get()
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

                Reply currentReply = new Reply(ReplyId, ReplyAuthor,ReplyBody, ReplyTime, ReplyUpdate, TicketId);
                allReplies.Add(currentReply);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allReplies;
        }
        [HttpPost("{id}")]
        //Put ticket/reply/{ticketId}
        public void Post(int id, [FromBody]Reply reply)
        {
                
                
        }
        
    }
}