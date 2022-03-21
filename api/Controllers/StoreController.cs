using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace API.Controllers;

[ApiController]
[Route("store")]
public class StoreController : ControllerBase
{
    public StoreContext Context { get; set; }
    public StoreController(StoreContext ctx) {
        Context = ctx;
    }
 
    [HttpGet("all")]
    public async Task<ActionResult> GetAllStores(){
        try {
            var stores = await Context.Store.ToListAsync();
            if(stores != null){
                return Ok(stores);
            } else {
                return NotFound("No stores found");
            }
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingleStore(int id){
        try {
            var target = await Context.Store
            .Where(s => s.ID == id)
            .Include(s => s.Staff)
            .FirstOrDefaultAsync();
            if(target != null) {
                return Ok(target);
            } else {
                return NotFound($"No store with ID: {id} found.");
            }
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("add")]
    public async Task<ActionResult> AddStore([FromBody] Store store) {
        try {
            await Context.Store.AddAsync(store);
            await Context.SaveChangesAsync();
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
        return Ok(store);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateStore(int id, [FromBody] Store store) {
        try{
            var target = await Context.Store.Where(s => s.ID == id).FirstOrDefaultAsync();
            if(target != null){
                target.Name = store.Name;
                target.Staff = store.Staff;
                await Context.SaveChangesAsync();
                return Ok(target);
            } else {
                return NotFound("Store doesnt exist!");
            }
        } catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStore(int id) {
        try{
            var target = await Context.Store.Where(s => s.ID == id).FirstOrDefaultAsync();
            if(target != null){
                var removedStore = new {
                    id = target.ID,
                    name = target.Name,
                    action = "DELETED"
                };
                Context.Store.Remove(target);
                await Context.SaveChangesAsync();
                return Ok(removedStore);
            } else {
                return NotFound("Store doesn't exist!");
            }
        } catch(Exception e){
            return BadRequest(e.Message);
        }
    }
}
