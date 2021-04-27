using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PetList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public class Validate
    {
        private const string ClassificationKey = "validClassification";
        private const string OwnerKey = "validOwner";

        private ITempDataDictionary tempData { get; set; }
        public Validate(ITempDataDictionary temp) => tempData = temp;

        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }

        public void CheckClassification(string classificationId, IRepository<Classification> data)
        {
            Classification entity = data.Get(classificationId);
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"Classification id {classificationId} is already in the database.";
        }
        public void MarkClassificationChecked() => tempData[ClassificationKey] = true;
        public void ClearClassification() => tempData.Remove(ClassificationKey);
        public bool IsClassificationChecked => tempData.Keys.Contains(ClassificationKey);

        public void CheckOwner(string firstName, string lastName, string operation, IRepository<Owner> data)
        {
            Owner entity = null;
            if (Operation.IsAdd(operation))
            {
                entity = data.Get(new QueryOptions<Owner>
                {
                    Where = a => a.FirstName == firstName && a.LastName == lastName
                });
            }
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"Owner {entity.FullName} is already in the database.";
        }
        public void MarkOwnerChecked() => tempData[OwnerKey] = true;
        public void ClearOwner() => tempData.Remove(OwnerKey);
        public bool IsOwnerChecked => tempData.Keys.Contains(OwnerKey);

    }
}
