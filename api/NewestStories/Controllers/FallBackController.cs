using Microsoft.AspNetCore.Mvc;

namespace NewestStories.Controllers
{
    public class FallBackController:Controller
    {
        public ActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}
