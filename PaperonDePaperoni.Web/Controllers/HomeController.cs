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
using PaperonDePaperoni.Bank.Interfaces;
using PaperonDePaperoni.QuiQuoQua.Interfaces;
using PaperonDePaperoni.Web.Models;

namespace PaperonDePaperoni.Web.Controllers
{
    public class HomeController : Controller
    {
        private static ActorId BankActorId = new ActorId("Bank");
        private StatelessServiceContext _statelessServiceContext;
        
        public HomeController(StatelessServiceContext statelessServiceContext)
        {
            _statelessServiceContext = statelessServiceContext;
        }

        public async Task<IActionResult> Index()
        {
            IBank bank = ActorProxy.Create<IBank>(BankActorId,
               $"{_statelessServiceContext.CodePackageActivationContext.ApplicationName}");

            await bank.DepositToPaperonDePaperoniAsync(10.6m);
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
