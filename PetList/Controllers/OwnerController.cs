using Microsoft.AspNetCore.Mvc;
using PetList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Controllers
{
    public class OwnerController : Controller
    {
        private IRepository<Owner> data { get; set; }
        public OwnerController(IRepository<Owner> rep) => data = rep;

        public IActionResult Index() => RedirectToAction("List");

        public ViewResult List(GridDTO vals)
        {
            string defaultSort = nameof(Owner.FirstName);
            var builder = new GridBuilder(HttpContext.Session, vals, defaultSort);
            builder.SaveRouteSegments();

            var options = new QueryOptions<Owner>
            {
                Includes = "PetOwners.Pet",
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                OrderByDirection = builder.CurrentRoute.SortDirection
            };
            if (builder.CurrentRoute.SortField.EqualsNoCase(defaultSort))
                options.OrderBy = a => a.FirstName;
            else
                options.OrderBy = a => a.LastName;

            var vm = new GridViewModel<Owner>
            {
                Items = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var author = data.Get(new QueryOptions<Owner>
            {
                Includes = "PetOwners.Pet",
                Where = a => a.OwnerId == id
            });
            return View(author);
        }
    }
}
