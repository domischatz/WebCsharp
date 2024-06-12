using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Grundlagen.Models;

namespace Web_Grundlagen.Api
{
    [Route("/api/")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly MyContext context;
        public UserApiController()
        {
            context = new MyContext(); //Datenbank
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return new JsonResult(await context.Users.Select(u => u.Role.ToString()).Distinct().ToListAsync()); //select nur noch role von user 
            //distinc löscht doppelte einträge , ToListAsync macht es zu liste wird returnt 
        }

        [HttpGet("GetUserOfRole")]
        public async Task<IActionResult> GetUserOfRole(string roleString)
        {
            if (roleString.Equals("Alle Rollen"))
            {
                return new JsonResult(await context.Users.ToListAsync());
            }
            UserRole role;
            if (!Enum.TryParse(roleString, out role))
            {
                return new JsonResult("Fehler");
            }
            return new JsonResult(await context.Users.Where(u => u.Role == role).ToListAsync());
        }
        [HttpDelete("delete/{email}")]
        public  IActionResult DeleteUser(string email)
        {
            var user = context.Users.Find(email);
            context.Users.Remove(user);
            context.SaveChanges();
            return NoContent();
        }
    }
}