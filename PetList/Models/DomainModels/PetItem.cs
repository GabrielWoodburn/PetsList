using Newtonsoft.Json;

namespace PetList.Models
{
    public class PetItem
    {
        public PetDTO Pet { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public double TotalWeight => Pet.Weight * Quantity;
    }
}
