using System.ComponentModel.DataAnnotations;

namespace PFCom.Selfhosted.Host.Web.Auth.v000_001.Register.Local.ReqModels
{
    public class RegisterLocalReqModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string Nickname { get; set; }
        
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
