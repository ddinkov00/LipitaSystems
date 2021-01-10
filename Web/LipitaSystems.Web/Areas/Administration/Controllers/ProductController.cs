using LipitaSystems.Web.ViewModels.InputModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    public class ProductController : AdministrationController
    {
        public IActionResult Create()
        {
            var inputModel = new ProductInputModel();

            return this.View();
        }
    }
}
