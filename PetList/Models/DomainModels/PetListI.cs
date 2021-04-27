using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using PetList.Models;

namespace PetList.Models
{
    public class PetListI : IPetList
    {
        private const string CartKey = "mycart";
        private const string CountKey = "mycount";

        private List<PetItem> items { get; set; }
        private List<PetItemDTO> storedItems { get; set; }

        private ISession session { get; set; }
        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }

        public PetListI(IHttpContextAccessor ctx)
        {
            session = ctx.HttpContext.Session;
            requestCookies = ctx.HttpContext.Request.Cookies;
            responseCookies = ctx.HttpContext.Response.Cookies;
            items = new List<PetItem>();
        }

        public void Load(IRepository<Pet> data)
        {
            items = session.GetObject<List<PetItem>>(CartKey);
            if (items == null)
            {
                items = new List<PetItem>();
                storedItems = requestCookies.GetObject<List<PetItemDTO>>(CartKey);
            }
            if (storedItems?.Count > items?.Count)
            {
                foreach (PetItemDTO storedItem in storedItems)
                {
                    var pet = data.Get(new QueryOptions<Pet>
                    {
                        Includes = "PetOwners.Owner, Classification",
                        Where = b => b.PetId == storedItem.PetId
                    });
                    if (pet != null)
                    {
                        var dto = new PetDTO();
                        dto.Load(pet);

                        PetItem item = new PetItem
                        {
                            Pet = dto,
                            Quantity = storedItem.Quantity
                        };
                        items.Add(item);
                    }
                }
                Save();
            }
        }

        public double TotalWeight => items.Sum(c => c.TotalWeight);
        public int? Count => session.GetInt32(CountKey) ?? requestCookies.GetInt32(CountKey);
        public IEnumerable<PetItem> List => items;

        public PetItem GetById(int id) =>
            items.FirstOrDefault(ci => ci.Pet.PetId == id);

        public void Add(PetItem item)
        {
            var exists = GetById(item.Pet.PetId);
            if (exists == null)
                items.Add(item);
            else
                exists.Quantity += item.Quantity;
        }

        public void Edit(PetItem item)
        {
            var exists = GetById(item.Pet.PetId);
            if (exists != null)
            {
                exists.Quantity = item.Quantity;
            }
        }

        public void Remove(PetItem item) => items.Remove(item);
        public void Clear() => items.Clear();

        public void Save()
        {
            if (items.Count == 0)
            {
                session.Remove(CartKey);
                session.Remove(CountKey);
                responseCookies.Delete(CartKey);
                responseCookies.Delete(CountKey);
            }
            else
            {
                session.SetObject<List<PetItem>>(CartKey, items);
                session.SetInt32(CountKey, items.Count);
                responseCookies.SetObject<List<PetItemDTO>>(CartKey, items.ToDTO());
                responseCookies.SetInt32(CountKey, items.Count);
            }
        }
    }
}