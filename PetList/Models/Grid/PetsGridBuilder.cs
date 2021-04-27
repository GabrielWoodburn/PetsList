using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public class PetsGridBuilder : GridBuilder
    {
        public PetsGridBuilder(ISession sess) : base(sess) { }

        public PetsGridBuilder(ISession sess, PetsGridDTO values,
            string defaultSortField) : base(sess, values, defaultSortField)
        {
            bool isInitial = values.Classification.IndexOf(FilterPrefix.Classification) == -1;
            routes.OwnerFilter = (isInitial) ? FilterPrefix.Owner + values.Owner : values.Owner;
            routes.ClassificationFilter = (isInitial) ? FilterPrefix.Classification + values.Classification : values.Classification;
            routes.WeightFilter = (isInitial) ? FilterPrefix.Weight + values.Weight : values.Weight;
        }

        public void LoadFilterSegments(string[] filter, Owner owner)
        {
            if (owner == null)
            {
                routes.OwnerFilter = FilterPrefix.Owner + filter[0];
            }
            else
            {
                routes.OwnerFilter = FilterPrefix.Owner + filter[0]
                    + "-" + owner.FullName.Slug();
            }
            routes.ClassificationFilter = FilterPrefix.Classification + filter[1];
            routes.WeightFilter = FilterPrefix.Weight + filter[2];
        }
        public void ClearFilterSegments() => routes.ClearFilters();

        string def = PetsGridDTO.DefaultFilter;
        public bool IsFilterByOwner => routes.OwnerFilter != def;
        public bool IsFilterByClassification => routes.ClassificationFilter != def;
        public bool IsFilterByWeight => routes.WeightFilter != def;

        public bool IsSortByClassification =>
            routes.SortField.EqualsNoCase(nameof(Classification));
        public bool IsSortByWeight =>
            routes.SortField.EqualsNoCase(nameof(Pet.Weight));
    }
}
