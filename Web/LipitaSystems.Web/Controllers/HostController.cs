namespace LipitaSystems.Web.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;

    public class HostController : BaseController
    {
        public HostController()
        {
        }

        public IActionResult Others()
        {
            return this.View();
        }

        public IActionResult Maintanance()
        {
            return this.View();
        }

        public IActionResult Hosting()
        {
            return this.View();
        }

        public IActionResult SecurityControle()
        {
            return this.View();
        }

        public IActionResult Websites()
        {
            return this.View();
        }
    }
}
