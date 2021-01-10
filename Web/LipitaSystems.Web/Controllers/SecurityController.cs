namespace LipitaSystems.Web.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;

    public class SecurityController : BaseController
    {
        public SecurityController()
        {
        }

        public IActionResult Control()
        {
            return this.View();
        }
    }
}
