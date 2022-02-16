using Api;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RESTFulSense.Clients;

namespace AcceptanceTests
{
    public class RestApiBroker
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly HttpClient baseClient;
        public IRESTFulApiFactoryClient apiFactoryClient { get; }

        public RestApiBroker()
        {
            this.webApplicationFactory = new WebApplicationFactory<Startup>();
            this.baseClient = this.webApplicationFactory.CreateClient();
            this.apiFactoryClient = new RESTFulApiFactoryClient(this.baseClient);
        }
    }
}
