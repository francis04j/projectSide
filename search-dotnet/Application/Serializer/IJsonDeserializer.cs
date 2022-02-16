using System.Net.Http;
using System.Threading.Tasks;

namespace Application
{
    public interface IJsonDeserializer
    {
        Task<SearchResult> DeserializeAsync(HttpResponseMessage response);
    }
}
