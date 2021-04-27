using Microsoft.AspNetCore.Mvc;
using PetList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSupplies.Controllers
{
    public class CartController : Controller
    {
        private IRepository<Pet> data { get; set; }
        private IPetList list { get; set; }

        public CartController(IRepository<Pet> rep, IPetList c)
        {
            data = rep;
            list = c;
            list.Load(data);
        }

        public ViewResult Index()
        {
            var vm = new CartViewModel
            {
                List = list.List,
                TotalWeight = list.TotalWeight
            };
            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Add(int id)
        {
            var pet = data.Get(new QueryOptions<Pet>
            {
                Includes = "PetOwners.Owner, Classification",
                Where = b => b.PetId == id
            });
            if (pet == null)
            {
                TempData["message"] = "Unable to add pet to list";
            }
            else
            {
                var dto = new PetDTO();
                dto.Load(pet);
                PetItem item = new PetItem
                {
                    Pet = dto,
                    Quantity = 1  // default value
                };
                list.Add(item);
                list.Save();

                TempData["message"] = $"{pet.Name} added to list";
            }
            return RedirectToAction("List", "Book");
        }

        public IActionResult Edit(int id)
        {
            PetItem item = list.GetById(id);
            if (item == null)
            {
                TempData["message"] = "Unable to locate list item";
                return RedirectToAction("List");
            }
            else
            {
                return View(item);
            }
        }
        [HttpPost]
        public RedirectToActionResult Edit(PetItem item)
        {
            list.Edit(item);
            list.Save();

            TempData["message"] = $"{item.Pet.Name} updated";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            PetItem item = list.GetById(id);
            list.Remove(item);
            list.Save();

            TempData["message"] = $"{item.Pet.Name} removed from list.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToActionResult Clear()
        {
            list.Clear();
            list.Save();

            TempData["message"] = "List cleared.";
            return RedirectToAction("Index");
        }

        public ViewResult Checkout() => View();
    }
}
