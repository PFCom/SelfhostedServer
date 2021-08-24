using System;
using System.ComponentModel.DataAnnotations;

namespace PFCom.Selfhosted.Core.Users
{
    public class User
    {
        public Guid Id { get; set; }
        
        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string Nickname { get; set; }
        
        public UserType Type { get; set; }

        public string Type_str
        {
            get => this.Type.ToString();
            set => this.Type = Enum.Parse<UserType>(value);
        }
    }
}
