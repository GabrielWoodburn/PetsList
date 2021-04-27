using Microsoft.AspNetCore.Mvc;
using PetList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Controllers
{
    public class PetController : Controller
    {
        private IRepository<Pet> data { get; set; }
        public PetController(IRepository<Pet> rep) => data = rep;

        public RedirectToActionResult Index() => RedirectToAction("List");

        public ViewResult List(PetsGridDTO values)
        {
            var builder = new PetsGridBuilder(HttpContext.Session, values,
                defaultSortField: nameof(Pet.Name));

            var options = new PetQueryOptions
            {
                Includes = "PetOwners.Owner, Classification",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };
            options.SortFilter(builder);

            var vm = new GridViewModel<Pet>
            {
                Items = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        public ViewResult Details(int id)
        {
            var book = data.Get(new QueryOptions<Pet>
            {
                Includes = "PetOwners.Owner, Classification",
                Where = b => b.PetId == id
            });
            return View(book);
        }

        [HttpPost]
        public RedirectToActionResult Filter([FromServices] IRepository<Owner> data,
            string[] filter, bool clear = false)
        {
            var builder = new PetsGridBuilder(HttpContext.Session);

            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                var owner = data.Get(filter[0].ToInt());
                builder.LoadFilterSegments(filter, owner);
            }

            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }
    }
}
