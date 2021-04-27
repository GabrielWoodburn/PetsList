using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public class PetQueryOptions : QueryOptions<Pet>
    {
        public void SortFilter(PetsGridBuilder builder)
        {
            if (builder.IsFilterByClassification)
            {
                Where = b => b.ClassificationId == builder.CurrentRoute.ClassificationFilter;
            }
            if (builder.IsFilterByWeight)
            {
                if (builder.CurrentRoute.WeightFilter == "under7")
                    Where = b => b.Weight < 7;
                else if (builder.CurrentRoute.WeightFilter == "7to14")
                    Where = b => b.Weight >= 7 && b.Weight <= 14;
                else
                    Where = b => b.Weight > 14;
            }
            if (builder.IsFilterByOwner)
            {
                int id = builder.CurrentRoute.OwnerFilter.ToInt();
                if (id > 0)
                    Where = b => b.PetOwners.Any(ba => ba.Owner.OwnerId == id);
            }

            if (builder.IsSortByClassification)
            {
                OrderBy = b => b.Classification.Name;
            }
            else if (builder.IsSortByWeight)
            {
                OrderBy = b => b.Weight;
            }
            else
            {
                OrderBy = b => b.Name;
            }
        }
    }
}
