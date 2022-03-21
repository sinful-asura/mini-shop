using Microsoft.AspNetCore.Mvc;
using Models;
namespace API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public ActionResult TryLogin([FromBody] Login loginInfo){
        if(loginInfo.Username == "StoreOwner" && loginInfo.Password == "store123"){
            return Ok(new {
                message = "Login successful!"
            });
        } else {
            return BadRequest("Wrong Data");
        }
    }
}