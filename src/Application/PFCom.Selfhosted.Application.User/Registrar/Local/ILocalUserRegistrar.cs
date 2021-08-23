using PFCom.Selfhosted.Application.User.Registrar.Local.Dto;

namespace PFCom.Selfhosted.Application.User.Registrar.Local
{
    public interface ILocalUserRegistrar
    {
        public RegisterUserRes_Dto Register(string nickname, string password);
    }
}
