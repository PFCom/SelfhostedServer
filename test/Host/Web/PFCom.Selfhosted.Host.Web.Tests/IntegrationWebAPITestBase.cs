using System;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json;
using Xunit;

namespace PFCom.Selfhosted.Host.Web.Tests
{
    public class IntegrationWebAPITestBase : IClassFixture<WebApiFactory<Startup>>
    {
        private WebApplicationFactory<Startup> _factory { get; }
        
        protected JsonSerializer Json { get; }

        protected HttpClient Client { get; }
        
        protected IServiceProvider Services { get; }

        public IntegrationWebAPITestBase(WebApiFactory<Startup> factory)
        {
            this._factory = factory;
            this.Client = this._factory.CreateClient();
            this.Services = this._factory.Services;

            this.Json = new JsonSerializer();
        }

        protected TOut PostJson<TIn, TOut>(string uri, TIn body)
        {
            return this.PostJsonAsync<TIn, TOut>(uri, body).GetAwaiter().GetResult();
        }

        protected async Task<TOut> PostJsonAsync<TIn, TOut>(string uri, TIn body)
        {
            var req = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create<TIn>(body)
            };

            var res = await this.Client.SendAsync(req);

            return await res.Content.ReadFromJsonAsync<TOut>();
        }

        protected TOut PostJson<TIn, TOut, TToken>(string uri, TIn body, TToken token)
        {
            return this.PostJsonAsync<TIn, TOut, TToken>(uri, body, token).GetAwaiter().GetResult();
        }

        protected async Task<TOut> PostJsonAsync<TIn, TOut, TToken>(string uri, TIn body, TToken token)
        {
            var req = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create<TIn>(body)
            };
            
            req.Headers.Add("Token", token.ToString());

            var res = await this.Client.SendAsync(req);

            return await res.Content.ReadFromJsonAsync<TOut>();
        }
    }
}
