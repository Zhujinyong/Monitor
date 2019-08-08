using Centa.Monitor.ViewModel.Request.System.User;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Centa.Monitor.WebApi.UnitTest
{
    /// <summary>
    /// 接口测试
    /// </summary>
    public class TestLoginController : IClassFixture<TestClientProvider>
    {
        private LoginRequestViewModel _login;

        private readonly HttpClient _client;

        public TestLoginController(TestClientProvider testClientProvider)
        {
            _client = testClientProvider.Client;
            _login = new LoginRequestViewModel();
        }

        [Fact(DisplayName = "接口测试")]
        [Trait("Monitor WebApi", "Monitor WebApi")]
        public async Task TestApi()
        {
            string token = await GetToken($"/api/monitor/v1.1/account/token");
            Assert.True(!string.IsNullOrEmpty(token));
        }

        private async Task<string> GetToken(string url)
        {
            var response = await _client.PostAsJsonAsync(url, _login);
            var token = string.Empty;
            var result = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(result))
            {
                var obj = JObject.Parse(result);
                token = obj["data"]["token"].ToString();
            }
            return token;
        }

        private async Task<bool> ActionAsync(string url, string token)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("token", token);
            var result = await _client.GetStringAsync(url);
            return result.Contains(@"""status"":200,");
        }
    }
}
