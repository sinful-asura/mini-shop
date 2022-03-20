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

    [HttpPost("test_post")]
    public ActionResult Post() {
        var obj = new {
            x = 5,
            meesage = "random object works"
        };
        return Ok(obj);
    }

    [HttpDelete("test_delete")]
    public ActionResult Delete() {
        return Ok("Delete Works");
    }

    [HttpPut("test_put")]
    public ActionResult Put() {
        return Ok("Put Works");
    }
}
