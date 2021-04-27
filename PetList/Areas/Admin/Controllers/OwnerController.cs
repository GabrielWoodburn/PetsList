using Microsoft.AspNetCore.Mvc;
using PetList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSupplies.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OwnerController : Controller
    {
        private Repository<Owner> data { get; set; }
        public OwnerController(PetListContext ctx) => data = new Repository<Owner>(ctx);

        public ViewResult Index()
        {
            var authors = data.List(new QueryOptions<Owner>
            {
                OrderBy = a => a.FirstName
            });
            return View(authors);
        }

        public RedirectToActionResult Select(int id, string operation)
        {
            switch (operation.ToLower())
            {
                case "view pets":
                    return RedirectToAction("ViewPets", new { id });
                case "edit":
                    return RedirectToAction("Edit", new { id });
                case "delete":
                    return RedirectToAction("Delete", new { id });
                default:
                    return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ViewResult Add() => View("Owner", new Owner());

        [HttpPost]
        public IActionResult Add(Owner owner, string operation)
        {
            var validate = new Validate(TempData);
            if (!validate.IsOwnerChecked)
            {
                validate.CheckOwner(owner.FirstName, owner.LastName, operation, data);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(owner.LastName), validate.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                data.Insert(owner);
                data.Save();
                validate.ClearOwner();
                TempData["message"] = $"{owner.FullName} added to Owners.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Owner", owner);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => View("Owner", data.Get(id));

        [HttpPost]
        public IActionResult Edit(Owner owner)
        {
            if (ModelState.IsValid)
            {
                data.Update(owner);
                data.Save();
                TempData["message"] = $"{owner.FullName} updated.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Owner", owner);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var owner = data.Get(new QueryOptions<Owner>
            {
                Includes = "PetOwners",
                Where = a => a.OwnerId == id
            });

            if (owner.PetOwners.Count > 0)
            {
                TempData["message"] = $"Can't delete owner {owner.FullName} because they are associated with these pets.";
                return GoToOwnerSearch(owner);
            }
            else
            {
                return View("Owner", owner);
            }
        }

        [HttpPost]
        public RedirectToActionResult Delete(Owner owner)
        {
            data.Delete(owner);
            data.Save();
            TempData["message"] = $"{owner.FullName} removed from Owners.";
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ViewPets(int id)
        {
            var owner = data.Get(id);
            return GoToOwnerSearch(owner);
        }

        private RedirectToActionResult GoToOwnerSearch(Owner owner)
        {
            var search = new SearchData(TempData)
            {
                SearchTerm = owner.FullName,
                Type = "owner"
            };
            return RedirectToAction("Search", "Pet");
        }
    }
}
