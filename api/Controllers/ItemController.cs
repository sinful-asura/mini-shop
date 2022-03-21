using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace API.Controllers;

[ApiController]
[Route("item")]
public class ItemController : ControllerBase
{
    public StoreContext Context { get; set; }
    public ItemController(StoreContext ctx) {
        Context = ctx;
    }
    [HttpGet("all")]
    public async Task<ActionResult> GetAllItems(){
        try {
            var sections = await Context.Item.ToListAsync();
            if(sections != null) {
                return Ok(sections);
            } else {
                return NotFound("There are no sections in database.");
            }
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingleItem(int id){
        try {
            var sections = await Context.Item.Where(i => i.ID == id).ToListAsync();
            if(sections != null) {
                return Ok(sections);
            } else {
                return NotFound("There are no sections in database.");
            }
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateNewItem([FromBody] Item item){
        try {
            await Context.Item.AddAsync(item);
            await Context.SaveChangesAsync();
            return Ok(item);
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id) {
        try{
            var target = await Context.Item.Where(i => i.ID == id).FirstOrDefaultAsync();
            if(target != null){
                Context.Item.Remove(target);
                await Context.SaveChangesAsync();
                return Ok(target);
            } else {
                return NotFound("Item doesn't exist!");
            }
        } catch(Exception e){
            return BadRequest(e.Message);
        }
    }
}