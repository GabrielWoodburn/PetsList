using Microsoft.AspNetCore.Mvc;
using PetList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Components
{
    public class CartBadge : ViewComponent
    {
        private IPetList petList { get; set; }
        public CartBadge(IPetList c) => petList = c;

        public IViewComponentResult Invoke()
        {
            return View(petList.Count);
        }
    }
}
