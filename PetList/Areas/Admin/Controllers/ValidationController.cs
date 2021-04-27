using Microsoft.AspNetCore.Mvc;
using PetList.Models;
using System;

namespace PetList.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ValidationController : Controller
    {
        public JsonResult CheckClassification(string classificationId, [FromServices] IRepository<Classification> data)
        {
            var validate = new Validate(TempData);
            validate.CheckClassification(classificationId, data);
            if (validate.IsValid)
            {
                validate.MarkClassificationChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }

        public JsonResult CheckOwner(string firstName, string lastName, string operation, [FromServices] IRepository<Owner> data)
        {
            var validate = new Validate(TempData);
            validate.CheckOwner(firstName, lastName, operation, data);
            if (validate.IsValid)
            {
                validate.MarkOwnerChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }

    }
}
