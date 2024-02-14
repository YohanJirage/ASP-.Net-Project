using DI_service_lifetime.Models;
using DI_service_lifetime.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace DI_service_lifetime.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISingeltonGuidService singleton1;
        private readonly ISingeltonGuidService singleton2;

        private readonly IScopedGuidService scoped1;
        private readonly IScopedGuidService scoped2;

        private readonly ITransientGuidService transient1;
        private readonly ITransientGuidService transient2;


        public HomeController(ISingeltonGuidService singletonGuid1, ISingeltonGuidService singletonGuid2,
            IScopedGuidService scopedGuid1, IScopedGuidService scopedguid2,
            ITransientGuidService transientGuid1, ITransientGuidService transientGuid2            )
        {

            singleton1 = singletonGuid1;
            singleton2 = singletonGuid2;
            scoped1 = scopedGuid1;
            scoped2 = scopedguid2;
            transient1 = transientGuid1;
            transient2 = transientGuid2;
        }

        

        public IActionResult Index()
        {
            StringBuilder message = new StringBuilder();
            message.Append($"Transient 1 : {transient1.GetGuid()}\n");
            message.Append($"Transient 2 : {transient2.GetGuid()}\n\n");
            message.Append($"Scoped 1 : {scoped1.GetGuid()}\n");
            message.Append($"Scoped 2 : {scoped2.GetGuid()}\n\n");
            message.Append($"Singleton 1 : {singleton1.GetGuid()}\n");
            message.Append($"SingleTon 2 : {singleton2.GetGuid()}\n\n");
            return View(message.ToString());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}