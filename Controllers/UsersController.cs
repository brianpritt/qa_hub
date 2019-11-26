using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QAHub.Models;
// Change Users to something else, used by SYSTEM and gets confusing
namespace QAHub.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        //GET ALL /users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return QAHub.Models.User.Get();
        }
        
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<User>> GetUser(int id)
        {
            
            return QAHub.Models.User.GetUser(id);
        }
        [HttpPost]
        public ActionResult Post([FromBody]User user)
        {
            user.SaveUser();
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]User user)
        {
            user.Update(id);
        }
    }
}