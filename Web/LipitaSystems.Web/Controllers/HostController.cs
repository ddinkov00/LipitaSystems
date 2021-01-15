namespace LipitaSystems.Web.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;

    public class HostController : BaseController
    {
        public HostController()
        {
        }

        public IActionResult Pricing()
        {
            return this.View();
        }

        public IActionResult Maintanance()
        {
            return this.View();
        }
    }
}
