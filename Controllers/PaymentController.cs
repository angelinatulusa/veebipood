using Microsoft.AspNetCore.Mvc;

namespace veebipood.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
