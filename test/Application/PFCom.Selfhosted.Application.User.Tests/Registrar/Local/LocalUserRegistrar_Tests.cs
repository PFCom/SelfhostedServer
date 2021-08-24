using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using PFCom.Selfhosted.Application.Hash.Hashing;
using PFCom.Selfhosted.Application.Hash.Hashing.Impl;
using PFCom.Selfhosted.Application.Hash.Password;
using PFCom.Selfhosted.Application.Hash.Password.Impl;
using PFCom.Selfhosted.Application.User.Registrar.Local;
using PFCom.Selfhosted.Application.User.Registrar.Local.Impl;
using PFCom.Selfhosted.Core.Users;
using PFCom.Selfhosted.DataAccess;
using PFCom.Selfhosted.DataAccess.Repositories.Users;
using Xunit;

namespace PFCom.Selfhosted.Application.User.Tests.Registrar.Local
{
    public class LocalUserRegistrar_Tests
    {
        [Fact]
        public void Test1()
        {
            var uow = this.mockUoW();
            var users = this.mockUserRepository();
            var localUsers = this.mockLocalUserRepository();

            IHashService hashService = new HashService();

            IUserRegisterPasswordHashService passwordHashService = new UserRegisterPasswordHashService(hashService);
            
            ILocalUserRegistrar registrar = new LocalUserRegistrar(uow, users, localUsers, passwordHashService, this.mockLogger<LocalUserRegistrar>());

            registrar.Register("a", "b");
            
            Assert.Equal(1, localUsers.GetAll().Count());
            Assert.Equal(1, users.GetAll().Count());
            Assert.Equal(1, users.GetAll().Count(x => x.Nickname == "a"));
        }

        [Fact]
        public void Test2()
        {
            var uow = this.mockUoW();
            var users = this.mockUserRepository();
            var localUsers = this.mockLocalUserRepository();

            IHashService hashService = new HashService();

            IUserRegisterPasswordHashService passwordHashService = new UserRegisterPasswordHashService(hashService);
            
            ILocalUserRegistrar registrar = new LocalUserRegistrar(uow, users, localUsers, passwordHashService, this.mockLogger<LocalUserRegistrar>());

            const int count = 100;
            const int nicknameLength = 7;
            const int passwordLength = 13;

            string[] nicknames = new string[count];

            for (int i = 0; i < count; i++)
            {
                string nickname = this.generateRandom(nicknameLength);
                var id = registrar.Register(nickname, this.generateRandom(passwordLength)).Id;
                nicknames[i] = nickname;
            }

            foreach (var nickname in nicknames)
            {
                Assert.Equal(1, users.GetAll().Count(x => x.Nickname == nickname));
                Guid id = users.GetAll().First(x => x.Nickname == nickname).Id;
                Assert.NotEqual(Guid.Empty, id);
                Assert.Equal(1, localUsers.GetAll().Count(x => x.Id == id));
            }
            
            Assert.Equal(count, users.GetAll().Count());
            Assert.Equal(count, localUsers.GetAll().Count());
        }
        
        [Fact]
        public void Test3()
        {
            var uow = this.mockUoW();
            var users = this.mockUserRepository();
            var localUsers = this.mockLocalUserRepository();
            
            IHashService hashService = new HashService();

            IUserRegisterPasswordHashService passwordHashService = new UserRegisterPasswordHashService(hashService);
            
            ILocalUserRegistrar registrar = new LocalUserRegistrar(uow, users, localUsers, passwordHashService, this.mockLogger<LocalUserRegistrar>());

            const int count = 100;
            const int nicknameLength = 7;
            const int passwordLength = 13;
            
            for (var i = 0; i < count; i++)
            {
                var id = registrar.Register(this.generateRandom(nicknameLength), this.generateRandom(passwordLength)).Id;
                var localUser = localUsers.GetAll().First(x => x.Id == id);
                Assert.Equal(128, localUser.Password.Length);
                Assert.True(localUser.PasswordSalt.Length > 0);
            }
            
            Assert.Equal(count, users.GetAll().Count());
            Assert.Equal(count, localUsers.GetAll().Count());
        }

        private ILogger<T> mockLogger<T>()
        {
            Mock<ILogger<T>> mock = new Mock<ILogger<T>>();
            
            return mock.Object;
        }

        private IUnitOfWork mockUoW()
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();

            mock.Setup(x => x.BeginTransaction()).Returns(this.mockTransaction);

            return mock.Object;
        }

        private ITransaction mockTransaction()
        {
            Mock<ITransaction> mock = new Mock<ITransaction>();

            mock.Setup(x => x.Commit()).Callback(() => { });

            return mock.Object;
        }

        private IUserRepository mockUserRepository()
        {
            IList<Core.Users.User> users = new List<Core.Users.User>();

            Mock<IUserRepository> mock = new Mock<IUserRepository>();

            mock.Setup(x => x.Add(It.IsAny<Core.Users.User>())).Callback<Core.Users.User>(x => users.Add(x));
            mock.Setup(x => x.GetAll()).Returns(() => users.AsQueryable());

            return mock.Object;
        }

        private ILocalUserRepository mockLocalUserRepository()
        {
            IList<LocalUser> localUsers = new List<LocalUser>();

            Mock<ILocalUserRepository> mock = new Mock<ILocalUserRepository>();

            mock.Setup(x => x.Add(It.IsAny<LocalUser>())).Callback<LocalUser>(x => localUsers.Add(x));
            mock.Setup(x => x.GetAll()).Returns(() => localUsers.AsQueryable());

            return mock.Object;
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
