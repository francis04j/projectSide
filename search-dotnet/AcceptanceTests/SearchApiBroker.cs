using AcceptanceTests.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AcceptanceTests
{
    public partial class SearchApiBroker : RestApiBroker
    {
        private const string searchRelativeUrl = "api/search";

        public async ValueTask<Restaurant> FindRestaurant(Query query) =>
            await apiFactoryClient.GetContentAsync<Restaurant>($"{searchRelativeUrl}/{query.Term}");

      /*  public async ValueTask<Student> DeleteStudentByIdAsync(Guid studentId) =>
            await this.apiFactoryClient.DeleteContentAsync<Student>($"{searchRelativeUrl}/{studentId}");
      */
    }
}
