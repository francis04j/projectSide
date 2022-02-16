using System.Net.Http;
using System.Threading.Tasks;

namespace Application.HttpClients
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string uri);
    }
}
