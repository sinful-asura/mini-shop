using Microsoft.AspNetCore.Mvc;
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
    public ActionResult Get(){
        return Ok(Context.Store);
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

    [HttpPut("update/{id}")]
    public ActionResult UpdateStore(int id, [FromBody] Store store) {
        try{
            var selectedStore = Context.Store
            .Where(s => s.ID == id)
            .FirstOrDefault();
            return Ok(selectedStore);
        } catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("test_delete")]
    public ActionResult Delete() {
        return Ok("Delete Works");
    }
}
