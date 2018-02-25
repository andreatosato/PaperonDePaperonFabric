using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using PaperonDePaperoni.QuiQuoQua.Interfaces;
using PaperonDePaperoni.Web.Models;

namespace PaperonDePaperoni.Web.Controllers
{
    public class HomeController : Controller
    {
        private static ActorId QuiActorId = new ActorId("Qui");
        private static ActorId QuoActorId = new ActorId("Quo");
        private static ActorId QuaActorId = new ActorId("Qua"); 
        private StatelessServiceContext _statelessServiceContext;

        public HomeController(StatelessServiceContext statelessServiceContext)
        {
            _statelessServiceContext = statelessServiceContext;
        }

        public async Task<IActionResult> Index()
        {
            IQui qui = ActorProxy.Create<IQui>(QuiActorId, 
                $"{_statelessServiceContext.CodePackageActivationContext.ApplicationName}");
            await qui.UpdateMoneyAsync(10.5m, CancellationToken.None);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
