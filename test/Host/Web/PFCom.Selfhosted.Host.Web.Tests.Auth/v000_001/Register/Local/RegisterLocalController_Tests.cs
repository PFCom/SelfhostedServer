using System;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PFCom.Selfhosted.DataAccess;
using PFCom.Selfhosted.DataAccess.Repositories.Users;
using PFCom.Selfhosted.Host.Web.Auth.v000_001.Register.Local.ReqModels;
using PFCom.Selfhosted.Host.Web.Auth.v000_001.Register.Local.ResModels;
using Xunit;

namespace PFCom.Selfhosted.Host.Web.Tests.Auth.v000_001.Register.Local
{
    public class RegisterLocalController_Tests : IntegrationWebAPITestBase
    {
        public RegisterLocalController_Tests(WebApiFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public void Test1()
        {
            var nickname = this.generateRandomString(16);
            var password = this.generateRandomString(16);

            var res = this.PostJson<RegisterLocalReqModel, RegisterLocalResModel>("/v0.1/auth/register/local",
                new RegisterLocalReqModel() {Nickname = nickname, Password = password});

            var id = res.Id;
            
            Assert.NotEqual(Guid.Empty, id);

            using (var scope = this.Services.CreateScope())
            {
                var users = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var localUsers = scope.ServiceProvider.GetRequiredService<ILocalUserRepository>();
                var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                Assert.Equal(1, users.GetAll().Count(x => x.Nickname == nickname && x.Id == id));
                Assert.Equal(1, localUsers.GetAll().Count(x => x.Id == id));

                var localUser = localUsers.GetAll().First(x => x.Id == id);
                
                Assert.NotNull(localUser.User);
                
                users.Delete(localUser.User);
                localUsers.Delete(localUser);
                
                uow.Complete();
                
                Assert.Equal(0, users.GetAll().Count(x => x.Id == id));
                Assert.Equal(0, localUsers.GetAll().Count(x => x.Id == id));
            }
        }

        [Fact]
        public void Test2()
        {
            using (var scope = this.Services.CreateScope())
            {
                var sp = scope.ServiceProvider;

                var users = sp.GetRequiredService<IUserRepository>();
                var localUsers = sp.GetRequiredService<ILocalUserRepository>();
                var uow = sp.GetRequiredService<IUnitOfWork>();

                var transaction = uow.BeginTransaction();
                
                users.DeleteAll();
                localUsers.DeleteAll();
                
                uow.SaveChanges();
                
                transaction.Commit();
                
                Assert.Equal(0, users.GetAll().Count());
                Assert.Equal(0, localUsers.GetAll().Count());
            }
            
            const int COUNT_OF_USERS = 300;
            const int NICKNAME_LENGTH = 16;
            const int PASSWORD_LENGTH = 16;

            Guid[] ids = new Guid[COUNT_OF_USERS];
            string[] nicknames = new string[COUNT_OF_USERS];

            for (var i = 0; i < COUNT_OF_USERS; i++)
            {
                string nickname = this.generateRandomString(NICKNAME_LENGTH);
                string password = this.generateRandomString(PASSWORD_LENGTH);

                var id = this.PostJson<RegisterLocalReqModel, RegisterLocalResModel>("/v0.1/auth/register/local",
                    new RegisterLocalReqModel() {Nickname = nickname, Password = password}).Id;

                ids[i] = id;
                nicknames[i] = nickname;
            }

            using (var scope = this.Services.CreateScope())
            {
                var sp = scope.ServiceProvider;

                var users = sp.GetRequiredService<IUserRepository>();
                var localUsers = sp.GetRequiredService<ILocalUserRepository>();
                
                Assert.Equal(COUNT_OF_USERS, users.GetAll().Count());
                Assert.Equal(COUNT_OF_USERS, localUsers.GetAll().Count());
                
                for (var i = 0; i < COUNT_OF_USERS; i++)
                {
                    var id = ids[i];
                    var nickname = nicknames[i];
                
                    Assert.Equal(1, users.GetAll().Count(x => x.Id == id && x.Nickname == nickname));
                    Assert.Equal(1, localUsers.GetAll().Count(x => x.Id == id));

                    var localUser = localUsers.GetAll().First(x => x.Id == id);
                }
            }
            
            using (var scope = this.Services.CreateScope())
            {
                var sp = scope.ServiceProvider;

                var users = sp.GetRequiredService<IUserRepository>();
                var localUsers = sp.GetRequiredService<ILocalUserRepository>();
                var uow = sp.GetRequiredService<IUnitOfWork>();
                
                Assert.Equal(COUNT_OF_USERS, users.GetAll().Count());
                Assert.Equal(COUNT_OF_USERS, localUsers.GetAll().Count());
                
                for (var i = 0; i < COUNT_OF_USERS; i++)
                {
                    var id = ids[i];
                    
                    users.Delete(users.GetAll().First(x => x.Id == id));
                }
                
                uow.SaveChanges();
                
                Assert.Equal(0, users.GetAll().Count());
                Assert.Equal(0, localUsers.GetAll().Count());
            }
        }

        private string generateRandomString(int length, string format = "x2")
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
