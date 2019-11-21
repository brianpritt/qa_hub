// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using QAHub.Models;
// using Microsoft.EntityFrameworkCore;
// using System.Reflection;

// namespace QAHub.Controllers
// {
//     [Route("[controller]")]
//     [ApiController]
//     public class RepliesController : ControllerBase
//     {
//         private QAHubContext _db;
//         public RepliesController(QAHubContext db)
//         {
//             _db = db;
//         }
//         [HttpGet]
//         //useless route
//         public ActionResult<IEnumerable<Reply>> Get()
//         {
//             var query = _db.Replies;
//             return query;
//         }
//         [HttpPost("{id}")]
//         //Put ticket/reply/{ticketId}
//         public void Post(int id, [FromBody]Reply reply)
//         {
//                 reply.TicketId = id;
//                 _db.Replies.Add(reply);
//                 _db.SaveChanges();
                
//         }
        
//     }
// }