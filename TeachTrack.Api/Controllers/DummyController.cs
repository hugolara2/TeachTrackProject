using Microsoft.AspNetCore.Mvc;

namespace TeachTrackApi.Controllers;

[Route("api/[controller]")]
public class DummyController : ControllerBase {
   [HttpGet]
   public string DummyGet() {
      return "Hello World";
   }
}