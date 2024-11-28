using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace web6;

[ApiController]
[Route("api")]
public class ToastController : ControllerBase
{
    [HttpPost("save")]
    public IActionResult SaveObject([FromBody] Toast? newToast)
    {
        const string filePath = "/tmp/file.json";
        if (newToast == null) return BadRequest(new { message = "Invalid data" });
        if (System.IO.File.Exists(filePath))
        {
            var jsonString = System.IO.File.ReadAllText(filePath);
            var list = JsonSerializer.Deserialize<List<Toast>>(jsonString);
            list!.Add(newToast);
            jsonString = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(filePath, jsonString);
        }
        else
        {
            var list = new List<Toast> { newToast };
            var jsonString = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(filePath, jsonString);
        }
        return Ok(new { message = $"Тост від \"{newToast.Sender}\" з повідомленням \"{newToast.Message}\" успішно збережено!" });
    }

    [HttpGet("toasts")]
    public IActionResult GetObjects()
    {
        const string filePath = "/tmp/file.json";
        if (!System.IO.File.Exists(filePath)) 
        {
            return Ok(new List<Toast>());
        }
        var toasts = System.IO.File.ReadAllText(filePath);
        var objectDataList = JsonSerializer.Deserialize<List<Toast>>(toasts);
        System.IO.File.Delete(filePath);
        return Ok(objectDataList);
    }
}

public class Toast(string? sender, string? message)
{
    public string? Sender { get; } = sender;

    public string? Message { get; } = message;
}