using System;

namespace PFCom.Selfhosted.Core.Users
{
    public class LocalUser
    {
        public Guid Id { get; set; }
        
        public User User { get; set; }
        
        public string Password { get; set; }
        
        public string PasswordSalt { get; set; }
    }
}
