using Microsoft.AspNetCore.Mvc;

namespace BookProject
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}
