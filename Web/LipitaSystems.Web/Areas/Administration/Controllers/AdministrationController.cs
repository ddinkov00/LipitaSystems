namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    using LipitaSystems.Common;
    using LipitaSystems.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
