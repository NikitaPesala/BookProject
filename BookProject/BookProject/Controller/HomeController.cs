using Microsoft.AspNetCore.Mvc;

namespace BookProject
{
    public class HomeController : Controller
    {
        public ViewResult Home()
        {
            return View();
        }
    }
}
