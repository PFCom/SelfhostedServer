using PFCom.Selfhosted.Application.Hash.Password.Dto;

namespace PFCom.Selfhosted.Application.Hash.Password
{
    public interface IUserRegisterPasswordHashService
    {
        public UserRegisterPasswordHashRes_Dto HashPassword(string password);
    }
}
