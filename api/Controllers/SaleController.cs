using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace API.Controllers;

[ApiController]
[Route("sale")]
public class SaleController : ControllerBase
{
    public StoreContext Context { get; set; }
    public SaleController(StoreContext ctx) {
        Context = ctx;
    }
    [HttpGet("all")]
    public async Task<ActionResult> GetAllSales(){
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
    public async Task<ActionResult> GetSingleSale(int id){
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
    public async Task<ActionResult> CreateNewSale([FromBody] Sale sale){
        try {
            await Context.Sale.AddAsync(sale);
            await Context.SaveChangesAsync();
            return Ok(sale);
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSale(int id) {
        try{
            var target = await Context.Sale.Where(i => i.ID == id).FirstOrDefaultAsync();
            if(target != null){
                Context.Sale.Remove(target);
                await Context.SaveChangesAsync();
                return Ok(target);
            } else {
                return NotFound("Sale doesn't exist!");
            }
        } catch(Exception e){
            return BadRequest(e.Message);
        }
    }
}