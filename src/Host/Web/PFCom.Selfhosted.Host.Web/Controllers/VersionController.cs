using Microsoft.AspNetCore.Mvc;

namespace PFCom.Selfhosted.Host.Web.Controllers
{
    [ApiController]
    [ApiVersionNeutral]
    [Area("core")]
    [Route("[area]/version")]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "0.1";
        }
    }
}
