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
        //POST REPLY
        [HttpPost("{id}")]
        //Put ticket/reply/{ticketId}
        public void Post(int id, [FromBody]Reply reply)
        {
            reply.SaveReply(id);  
        }
        //PUT REPLY
        [HttpPut("{id}/update")]
        public void Update(int id, [FromBody]Reply reply)
        {
            reply.Update(id);
        }
        //Delete REPLY
        [HttpDelete("{id}/delete")]
        public void Delete(int id)
        {

        }
        
    }
}