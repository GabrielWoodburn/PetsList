using Microsoft.AspNetCore.Mvc;
using PetList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Components
{
    public class ClassificationDropDown : ViewComponent
    {
        private IRepository<Classification> data { get; set; }
        public ClassificationDropDown(IRepository<Classification> rep) => data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var genres = data.List(new QueryOptions<Classification>
            {
                OrderBy = g => g.Name
            });

            var vm = new DropDownViewModel
            {
                SelectedValue = selectedValue,
                DefaultValue = PetsGridDTO.DefaultFilter,
                Items = genres.ToDictionary(g => g.ClassificationId.ToString(), g => g.Name)
            };

            return View(SharedPath.Select, vm);
        }
    }
}
