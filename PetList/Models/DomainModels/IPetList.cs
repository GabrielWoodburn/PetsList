using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public interface IPetList
    {
        double TotalWeight { get; }
        int? Count { get; }
        IEnumerable<PetItem> List { get; }

        void Load(IRepository<Pet> data);
        PetItem GetById(int id);

        void Add(PetItem item);
        void Edit(PetItem item);
        void Remove(PetItem item);
        void Clear();
        void Save();
    }
}
