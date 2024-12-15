using Microsoft.AspNetCore.Mvc;

namespace CollaboraRTC.Controllers
{
    public class HomeController : Controller
    {
        // Action method to render the VideoCall view
        public IActionResult VideoCall()
        {
            return View();
        }
    }
}
