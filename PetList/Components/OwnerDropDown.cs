using Microsoft.AspNetCore.Mvc;
using PetList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Components
{
    public class OwnerDropDown : ViewComponent
    {
        private IRepository<Owner> data { get; set; }
        public OwnerDropDown(IRepository<Owner> rep) => data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var authors = data.List(new QueryOptions<Owner>
            {
                OrderBy = a => a.FirstName
            });

            var vm = new DropDownViewModel
            {
                SelectedValue = selectedValue,
                DefaultValue = PetsGridDTO.DefaultFilter,
                Items = authors.ToDictionary(
                    a => a.OwnerId.ToString(), a => a.FullName)
            };

            return View(SharedPath.Select, vm);
        }
    }
}
