using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetList.Models;
using System;

namespace PetList.Controllers
{
    public class HomeController : Controller
    {
        private Repository<Pet> data { get; set; }
        public HomeController(PetListContext ctx) => data = new Repository<Pet>(ctx);

        public ViewResult Index()
        {
            var random = data.Get(new QueryOptions<Pet>
            {
                OrderBy = b => Guid.NewGuid()
            });

            return View(random);
        }
    }
}
