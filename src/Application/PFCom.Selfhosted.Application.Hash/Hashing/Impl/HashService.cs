using System.Security.Cryptography;
using System.Text;

namespace PFCom.Selfhosted.Application.Hash.Hashing.Impl
{
    public class HashService : IHashService
    {
        private SHA512 _hash { get; }

        public HashService()
        {
            this._hash = SHA512.Create();
        }
        
        public string Hash(string input)
        {
            byte[] bytes = this._hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder stringB = new StringBuilder();

            foreach (byte b in bytes)
            {
                stringB.Append(b.ToString("x2"));
            }

            return stringB.ToString();
        }
    }
}
