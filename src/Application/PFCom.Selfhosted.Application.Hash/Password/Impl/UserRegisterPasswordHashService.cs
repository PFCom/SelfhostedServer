using System;
using System.Text;
using PFCom.Selfhosted.Application.Hash.Hashing;
using PFCom.Selfhosted.Application.Hash.Password.Dto;

namespace PFCom.Selfhosted.Application.Hash.Password.Impl
{
    public class UserRegisterPasswordHashService : IUserRegisterPasswordHashService
    {
        private IHashService _hash { get; }

        private const int LengthOfSalt = 64;

        public UserRegisterPasswordHashService(IHashService hash)
        {
            this._hash = hash;
        }
        
        public UserRegisterPasswordHashRes_Dto HashPassword(string password)
        {
            string salt = this.generateRandom(LengthOfSalt);
            string hashedPassword = this._hash.Hash(salt + password);

            return new UserRegisterPasswordHashRes_Dto()
            {
                HashedPassword = hashedPassword,
                Salt = salt
            };
        }
        
        private string generateRandom(int length, string format = "x2")
        {
            Byte[] bytes = new Byte[length];
            Random rand = new Random();
            
            rand.NextBytes(bytes);

            StringBuilder builder = new StringBuilder();

            foreach (byte b in bytes)
            {
                builder.Append(b.ToString(format));
            }

            return builder.ToString();
        }
    }
}
