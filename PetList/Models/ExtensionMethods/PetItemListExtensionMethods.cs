using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public static class PetItemListExtensions
    {
        public static List<PetItemDTO> ToDTO(this List<PetItem> list) =>
            list.Select(ci => new PetItemDTO
            {
                PetId = ci.Pet.PetId,
                Quantity = ci.Quantity
            }).ToList();
    }
}
