namespace PFCom.Selfhosted.Application.Hash.Password.Dto
{
    public class UserRegisterPasswordHashRes_Dto
    {
        public string HashedPassword { get; set; }
        
        public string Salt { get; set; }
    }
}
