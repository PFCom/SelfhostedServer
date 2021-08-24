using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFCom.Selfhosted.Application.User.Registrar.Local;
using PFCom.Selfhosted.Host.Web.Auth.v000_001.Register.Local.ReqModels;
using PFCom.Selfhosted.Host.Web.Auth.v000_001.Register.Local.ResModels;
using Swashbuckle.AspNetCore.Annotations;

namespace PFCom.Selfhosted.Host.Web.Auth.v000_001.Register.Local.Controllers
{
    [ApiController]
    [ApiVersion("0.1")]
    [Area(Routes.Register.AREA)]
    [Route(Routes.Register.Local.POST)]
    public class RegisterLocalController : ControllerBase
    {
        private ILocalUserRegistrar _registrar { get; }

        public RegisterLocalController(ILocalUserRegistrar registrar)
        {
            this._registrar = registrar;
        }
        
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully registered the user.", typeof(RegisterLocalResModel))]
        [SwaggerResponse(StatusCodes.Status501NotImplemented, "Failed to register the user.")]
        public IActionResult Post([FromBody] RegisterLocalReqModel req)
        {
            var regData = this._registrar.Register(req.Nickname, req.Password);

            if (!regData.Successful)
            {
                return this.StatusCode(501);
            }

            return this.Ok(new RegisterLocalResModel()
            {
                Id = regData.Id
            });
        }
    }
}
