using System;
using System.Net;
using System.Runtime.Versioning;
using Microsoft.Extensions.Logging;
using PFCom.Selfhosted.Application.Hash.Password;
using PFCom.Selfhosted.Application.User.Registrar.Local.Dto;
using PFCom.Selfhosted.Core.Users;
using PFCom.Selfhosted.DataAccess;
using PFCom.Selfhosted.DataAccess.Repositories.Users;

namespace PFCom.Selfhosted.Application.User.Registrar.Local.Impl
{
    public class LocalUserRegistrar : ILocalUserRegistrar
    {
        private IUnitOfWork _uow { get; }
        
        private IUserRepository _userRepo { get; }
        
        private ILocalUserRepository _localUserRepo { get; }
        
        private IUserRegisterPasswordHashService _hash { get; }
        
        private ILogger<LocalUserRegistrar> _logger { get; }
        
        public LocalUserRegistrar(IUnitOfWork uow, IUserRepository userRepo, ILocalUserRepository localUserRepo, IUserRegisterPasswordHashService hash, ILogger<LocalUserRegistrar> logger)
        {
            this._uow = uow;
            this._userRepo = userRepo;
            this._localUserRepo = localUserRepo;
            this._hash = hash;
            this._logger = logger;
        }
        
        public RegisterUserRes_Dto Register(string nickname, string password)
        {
            bool successful = false;
            
            var id = Guid.NewGuid();
            
            try
            {
                var user = new Core.Users.User()
                {
                    Id = id,
                    Nickname = nickname,
                    Type = UserType.Local
                };

                var hash = this._hash.HashPassword(password);

                var localUser = new LocalUser()
                {
                    Id = id,
                    Password = hash.HashedPassword,
                    PasswordSalt = hash.Salt,
                    User = user
                };

                var transaction = this._uow.BeginTransaction();

                this._userRepo.Add(user);
                this._localUserRepo.Add(localUser);

                this._uow.SaveChanges();
            
                transaction.Commit();

                successful = true;
            }
            catch (Exception e)
            {
                this._logger.LogError(e.ToString());
                successful = false;
            }
            
            if (successful)
            {
                return new RegisterUserRes_Dto()
                {
                    Successful = true,
                    Id = id
                };
            }
            else
            {
                return new RegisterUserRes_Dto()
                {
                    Successful = false,
                    Id = Guid.Empty
                };
            }
        }
    }
}
