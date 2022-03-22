using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace API.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    public StoreContext Context { get; set; }
    public UserController(StoreContext ctx) {
        Context = ctx;
    }
    [HttpGet("all")]
    public async Task<ActionResult> GetAllUsers(){
        try {
            var users = await Context.User.Select(u => new {
                u.ID,
                u.Name,
                u.Salary
            }).ToListAsync();
            if(users != null){
                return Ok(users);
            } else {
                return NotFound("No users in database");
            }
        } catch (Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingleUser(int id){
        try {
            var user = await Context.User.Where(u => u.ID == id).FirstOrDefaultAsync();
            
            if(user != null){
                return Ok(new {
                    ID = user.ID,
                    Name = user.Name,
                    Salary = user.Salary,
                    Workplaces = await Context.Employments.Where(e => e.UserID == user.ID).ToListAsync()
                });
            } else {
                return NotFound("User not found");
            }
        } catch (Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpPost("{id}/workplace/add")]
    public async Task<ActionResult> AddWorkplace(int id, [FromBody] WorksIn workplace) {
        try {
            var user = await Context.User.Where(u => u.ID == id).FirstOrDefaultAsync();
            if(user != null) {
                if(user.Workplaces != null){
                    user.Workplaces.Add(workplace);
                } else {
                    user.Workplaces = new List<WorksIn>();
                    user.Workplaces.Add(workplace);
                }
                await Context.SaveChangesAsync();
                return Ok("Operation succesful");
            } else {
                return NotFound("User not found!");
            }
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(int id, [FromBody] User updatedUser){
        try {
            var user = await Context.User.Where(u => u.ID == id).FirstOrDefaultAsync();
            
            if(user != null){
                user.Name = updatedUser.Name;
                user.Salary = updatedUser.Salary;
                await Context.SaveChangesAsync();
                return Ok(user);
            } else {
                return NotFound("User not found");
            }
        } catch (Exception e){
            return BadRequest(e.Message);
        }
    }
}