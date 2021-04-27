using PetList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public static class FilterPrefix
    {
        public const string Classification = "classification-";
        public const string Weight = "weight-";
        public const string Owner = "owner-";
    }

    public class RouteDictionary : Dictionary<string, string>
    {
        public int PageNumber
        {
            get => Get(nameof(GridDTO.PageNumber)).ToInt();
            set => this[nameof(GridDTO.PageNumber)] = value.ToString();
        }

        public int PageSize
        {
            get => Get(nameof(GridDTO.PageSize)).ToInt();
            set => this[nameof(GridDTO.PageSize)] = value.ToString();
        }

        public string SortField
        {
            get => Get(nameof(GridDTO.SortField));
            set => this[nameof(GridDTO.SortField)] = value;
        }

        public string SortDirection
        {
            get => Get(nameof(GridDTO.SortDirection));
            set => this[nameof(GridDTO.SortDirection)] = value;
        }

        public void SetSortAndDirection(string fieldName, RouteDictionary current)
        {
            this[nameof(GridDTO.SortField)] = fieldName;

            if (current.SortField.EqualsNoCase(fieldName) &&
                current.SortDirection == "asc")
                this[nameof(GridDTO.SortDirection)] = "desc";
            else
                this[nameof(GridDTO.SortDirection)] = "asc";
        }

        public string ClassificationFilter
        {
            get => Get(nameof(PetsGridDTO.Classification))?.Replace(FilterPrefix.Classification, "");
            set => this[nameof(PetsGridDTO.Classification)] = value;
        }

        public string WeightFilter
        {
            get => Get(nameof(PetsGridDTO.Weight))?.Replace(FilterPrefix.Weight, "");
            set => this[nameof(PetsGridDTO.Weight)] = value;
        }

        public string OwnerFilter
        {
            get
            {
                string s = Get(nameof(PetsGridDTO.Owner))?.Replace(FilterPrefix.Owner, "");
                int index = s?.IndexOf('-') ?? -1;
                return (index == -1) ? s : s.Substring(0, index);
            }
            set => this[nameof(PetsGridDTO.Owner)] = value;
        }

        public void ClearFilters() =>
            ClassificationFilter = WeightFilter = OwnerFilter = PetsGridDTO.DefaultFilter;

        private string Get(string key) => Keys.Contains(key) ? this[key] : null;

        public RouteDictionary Clone()
        {
            var clone = new RouteDictionary();
            foreach (var key in Keys)
            {
                clone.Add(key, this[key]);
            }
            return clone;
        }
    }
}
