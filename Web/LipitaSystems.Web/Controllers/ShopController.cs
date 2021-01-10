namespace LipitaSystems.Web.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    public class ShopController : BaseController
    {
        public ShopController()
        {
        }

        public IActionResult All()
        {
            return this.View();
        }
    }
}
