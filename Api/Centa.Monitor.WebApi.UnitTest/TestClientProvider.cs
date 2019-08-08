using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;

namespace Centa.Monitor.WebApi.UnitTest
{
    public class TestClientProvider : IDisposable
    {
        public TestServer _server;
        public HttpClient Client { get; }
        public void Dispose()
        {
            _server?.Dispose();
        }
    }
}
