using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QAHub.Models;

namespace QAHub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        
        [HttpGet]
        //useless route /replies/
        public ActionResult<IEnumerable<Reply>> Get()
        {
            return Reply.GetAll();
        }
        //GET one REPLY /replies/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Reply>> Get(int id)
        {   
            List<Reply> allReplies= Reply.GetReply(id);
            if (allReplies.Count == 0)
            {
                return StatusCode(204);
            }
            return allReplies;
        }
        
        [HttpPost("{id}")]
        //POST REPLY to TICKET
        //Put ticket/reply/{ticketId}
        public void Post(int id, [FromBody]Reply reply)
        {
            reply.SaveReply(id); 
        }
        //PUT REPLY /replies/{id}/update
        [HttpPut("{id}/update")]
        public void Update(int id, [FromBody]Reply reply)
        {
            reply.Update(id);
        }
        //Delete REPLY /replies/{id}/delete
        [HttpDelete("{id}/delete")]
        public void Delete(int id)
        {
            Reply.Delete(id);
        }
        
    }
}