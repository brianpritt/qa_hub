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
        //GET User /users/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<User>> GetUser(int id)
        {
            
            return QAHub.Models.User.GetUser(id);
        }
        //Post new User /users/new
        [HttpPost]
        public ActionResult Post([FromBody]User user)
        {
            user.SaveUser();
            return StatusCode(201);
        }
        //PUT existing user /users/{id}/update
        [HttpPut("{id}/update")]
        public void Put(int id, [FromBody]User user)
        {
            user.Update(id);
        }
        //Delete user /users/{id}/delete
        [HttpDelete("{id}/delete")]
        public void Delete(int id)
        {
            QAHub.Models.User.DeleteUser(id);
        }
    }
}