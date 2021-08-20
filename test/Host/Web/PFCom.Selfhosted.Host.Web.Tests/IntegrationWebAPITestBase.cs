using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;

namespace PFCom.Selfhosted.Host.Web.Tests
{
    public class IntegrationWebAPITestBase : IClassFixture<WebApplicationFactory<Startup>>
    {
        private WebApplicationFactory<Startup> _factory { get; }

        protected HttpClient Client { get; }

        public IntegrationWebAPITestBase(WebApplicationFactory<Startup> factory)
        {
            this._factory = factory;
            this.Client = this._factory.CreateClient();
        }
    }
}
