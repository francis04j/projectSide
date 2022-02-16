using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public interface IJustEatSearchRestaurantClient
    {
        Task<HttpResponseMessage> SearchRestaurantsByCode(string code);
    }
}