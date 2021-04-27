using Microsoft.AspNetCore.Mvc;
using PetList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    [Area("Admin")]
    public class ClassificationController : Controller
    {
        private Repository<Classification> data { get; set; }
        public ClassificationController(PetListContext ctx) => data = new Repository<Classification>(ctx);

        public ViewResult Index()
        {
            var search = new SearchData(TempData);
            search.Clear();

            var classifications = data.List(new QueryOptions<Classification>
            {
                OrderBy = g => g.Name
            });
            return View(classifications);
        }

        [HttpGet]
        public ViewResult Add() => View("Classification", new Classification());

        [HttpPost]
        public IActionResult Add(Classification classification)
        {
            var validate = new Validate(TempData);
            if (!validate.IsClassificationChecked)
            {
                validate.CheckClassification(classification.ClassificationId, data);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(classification.ClassificationId), validate.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                data.Insert(classification);
                data.Save();
                validate.ClearClassification();
                TempData["message"] = $"{classification.Name} added to Classifications.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Classification", classification);
            }
        }

        [HttpGet]
        public ViewResult Edit(string id) => View("Classification", data.Get(id));

        [HttpPost]
        public IActionResult Edit(Classification classification)
        {
            if (ModelState.IsValid)
            {
                data.Update(classification);
                data.Save();
                TempData["message"] = $"{classification.Name} updated.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Classification", classification);
            }
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var classification = data.Get(new QueryOptions<Classification>
            {
                Includes = "Pets",
                Where = g => g.ClassificationId == id
            });

            if (classification.Pets.Count > 0)
            {
                TempData["message"] = $"Can't delete classification {classification.Name} "
                                    + "because it's associated with these pets.";
                return GoToPetSearchResults(id);
            }
            else
            {
                return View("Classification", classification);
            }
        }

        [HttpPost]
        public IActionResult Delete(Classification classification)
        {
            data.Delete(classification);
            data.Save();
            TempData["message"] = $"{classification.Name} removed from Classifications.";
            return RedirectToAction("Index");  // PRG pattern
        }

        public RedirectToActionResult ViewBooks(string id)
        {
            RedirectToActionResult result = GoToPetSearchResults(id);
            return result;
        }

        private RedirectToActionResult GoToPetSearchResults(string id)
        {
            var search = new SearchData(TempData)
            {
                SearchTerm = id,
                Type = "classification"
            };
            return RedirectToAction("Search", "Pet");
        }

    }
}
