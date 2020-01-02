using Microsoft.AspNetCore.Mvc;

namespace CheckersGame.Controllers
{
    public class BoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}