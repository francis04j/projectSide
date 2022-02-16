using Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IApiClient _apiClient;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IndexModel(IApiClient apiClient, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                _apiClient.SearchRestaurantByPostCode(SearchString);
            }


        }
    }
}
