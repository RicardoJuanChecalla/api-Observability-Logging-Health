using Shopping.Aggregator.Models;
//using Shopping.Aggregator.Extensions;

namespace Shopping.Aggregator.Services
{
    public class BasketService: IBasketService
    {
        private readonly HttpClient _client;

        public BasketService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<BasketModel> GetBasket(string userName)
        {
            var response = await _client.GetAsync($"/api/v1/Basket/{userName}");
            if (response.IsSuccessStatusCode)
                //return await response.ReadContentAs<BasketModel>();
                return await response.Content.ReadFromJsonAsync<BasketModel>();
            else
                return new BasketModel();
        }
    }
}