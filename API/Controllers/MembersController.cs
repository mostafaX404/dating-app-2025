using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    
    public class MembersController(AppDbContext context) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var users = await context.Users.ToListAsync();

            return users;
        }

        [HttpGet("check")]
        public async Task<IActionResult> Check()
        {
            var count = await context.Users.CountAsync();
            return Ok(new { Count = count });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null) return NotFound();

            return user;
        }


    }




}
