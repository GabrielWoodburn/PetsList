using Microsoft.AspNetCore.Mvc;
using PetList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PetController : Controller
    {
        private PetItemUnit data { get; set; }
        public PetController(PetListContext ctx) => data = new PetItemUnit(ctx);

        public ViewResult Index()
        {
            var search = new SearchData(TempData);
            search.Clear();

            return View();
        }

        [HttpPost]
        public RedirectToActionResult Search(SearchViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var search = new SearchData(TempData)
                {
                    SearchTerm = vm.SearchTerm,
                    Type = vm.Type
                };
                return RedirectToAction("Search");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ViewResult Search()
        {
            var search = new SearchData(TempData);

            if (search.HasSearchTerm)
            {
                var vm = new SearchViewModel
                {
                    SearchTerm = search.SearchTerm
                };

                var options = new QueryOptions<Pet>
                {
                    Includes = "Classification, PetOwners.Owner"
                };
                if (search.IsPet)
                {
                    options.Where = b => b.Name.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for pet named: '{vm.SearchTerm}'";
                }
                if (search.IsOwner)
                {
                    int index = vm.SearchTerm.LastIndexOf(' ');
                    if (index == -1)
                    {
                        options.Where = b => b.PetOwners.Any(
                            ba => ba.Owner.FirstName.Contains(vm.SearchTerm) ||
                            ba.Owner.LastName.Contains(vm.SearchTerm));
                    }
                    else
                    {
                        string first = vm.SearchTerm.Substring(0, index);
                        string last = vm.SearchTerm.Substring(index + 1);
                        options.Where = b => b.PetOwners.Any(
                            ba => ba.Owner.FirstName.Contains(first) &&
                            ba.Owner.LastName.Contains(last));
                    }
                    vm.Header = $"Search results for owner named: '{vm.SearchTerm}'";
                }
                if (search.IsClassification)
                {
                    options.Where = b => b.ClassificationId.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for classification ID '{vm.SearchTerm}'";
                }
                vm.Pets = data.Pets.List(options);
                return View("SearchResults", vm);
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet]
        public ViewResult Add(int id) => GetPet(id, "Add");

        [HttpPost]
        public IActionResult Add(PetViewModel vm)
        {
            if (ModelState.IsValid)
            {
                data.LoadNewPetOwners(vm.Pet, vm.SelectedOwners);
                data.Pets.Insert(vm.Pet);
                data.Save();

                TempData["message"] = $"{vm.Pet.Name} added to Pets.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Add");
                return View("Pet", vm);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => GetPet(id, "Edit");

        [HttpPost]
        public IActionResult Edit(PetViewModel vm)
        {
            if (ModelState.IsValid)
            {
                data.DeleteCurrentPetOwners(vm.Pet);
                data.LoadNewPetOwners(vm.Pet, vm.SelectedOwners);

                data.Pets.Update(vm.Pet);
                data.Save();

                TempData["message"] = $"{vm.Pet.Name} updated.";
                return RedirectToAction("Search");
            }
            else
            {
                Load(vm, "Edit");
                return View("Pet", vm);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id) => GetPet(id, "Delete");

        [HttpPost]
        public IActionResult Delete(PetViewModel vm)
        {
            data.Pets.Delete(vm.Pet);
            data.Save();
            TempData["message"] = $"{vm.Pet.Name} removed from Pets.";
            return RedirectToAction("Search");
        }

        private ViewResult GetPet(int id, string operation)
        {
            var pet = new PetViewModel();
            Load(pet, operation, id);
            return View("Pet", pet);
        }
        private void Load(PetViewModel vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm.Pet = new Pet();
            }
            else
            {
                vm.Pet = data.Pets.Get(new QueryOptions<Pet>
                {
                    Includes = "PetOwners.Owner, Classification",
                    Where = b => b.PetId == (id ?? vm.Pet.PetId)
                });
            }

            vm.SelectedOwners = vm.Pet.PetOwners?.Select(
                ba => ba.Owner.OwnerId).ToArray();
            vm.Owners = data.Owners.List(new QueryOptions<Owner>
            {
                OrderBy = a => a.FirstName
            });
            vm.Classifications = data.Classifications.List(new QueryOptions<Classification>
            {
                OrderBy = g => g.Name
            });
        }
    }
}