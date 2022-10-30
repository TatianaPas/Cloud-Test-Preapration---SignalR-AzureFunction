using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRSamlple.Hubs;
using SignalRSamlple.Models;
using System.Diagnostics;

namespace SignalRSamlple.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //inject hub
        private readonly IHubContext<DeathlyHallowHub> _deathlyHub;

        public HomeController(ILogger<HomeController> logger, IHubContext<DeathlyHallowHub> deathlyHub)
        {
            _logger = logger;
            _deathlyHub=deathlyHub;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public async Task<IActionResult> DeathlyHallows (string type)
        {

            if(StaticData.DealthyHallowRace.ContainsKey(type))
            {
                StaticData.DealthyHallowRace[type]++;
            }

            //send notificaiton with Hub

            await _deathlyHub.Clients.All.SendAsync("updateDeathlyHallowsCount",
                StaticData.DealthyHallowRace[StaticData.Cloak],
                StaticData.DealthyHallowRace[StaticData.Stone], 
                StaticData.DealthyHallowRace[StaticData.Wand]);


            return Accepted();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}