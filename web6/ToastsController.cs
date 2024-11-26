using Microsoft.AspNetCore.Mvc;

namespace web6;

[ApiController]
[Route("api")]
public class ToastController : ControllerBase
{
    private static readonly List<Toast?> Toasts = [];
    
    [HttpPost("save")]
    public IActionResult SaveObject([FromBody] Toast? newToast)
    {
        if (newToast == null) return BadRequest("Invalid data");
        Toasts.Add(newToast);
        return Ok("Object saved");
    }

    [HttpGet("objects")]
    public IActionResult GetObjects()
    {
        return Ok(Toasts);
    }
}

public class Toast
{
    public string? Sender { get; set; }
    
    public string? Message { get; set; }
}