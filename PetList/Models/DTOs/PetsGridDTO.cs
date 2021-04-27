using Newtonsoft.Json;

namespace PetList.Models
{
    public class PetsGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";

        public string Owner { get; set; } = DefaultFilter;
        public string Classification { get; set; } = DefaultFilter;
        public string Weight { get; set; } = DefaultFilter;
    }
}
