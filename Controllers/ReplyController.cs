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
        //useless route
        public ActionResult<IEnumerable<Reply>> Get()
        {
            return Reply.GetAll();
        }
        //GET one REPLY
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Reply>> Get(int id)
        {
            return Reply.GetReply(id);
        }
        //POST REPLY to TICKET
        [HttpPost("{id}")]
        //Put ticket/reply/{ticketId}
        public ActionResult Post(int id, [FromBody]Reply reply)
        {
            reply.SaveReply(id);  
            return StatusCode(201);
        }
        //PUT REPLY
        [HttpPut("{id}/update")]
        public ActionResult Update(int id, [FromBody]Reply reply)
        {
            reply.Update(id);
            return StatusCode(201);
        }
        //Delete REPLY
        [HttpDelete("{id}/delete")]
        public ActionResult Delete(int id)
        {
            Reply.Delete(id);
            return StatusCode(200);
        }
        
    }
}