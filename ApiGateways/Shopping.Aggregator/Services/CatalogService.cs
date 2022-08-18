using Shopping.Aggregator.Models;
//using Shopping.Aggregator.Extensions;

namespace Shopping.Aggregator.Services
{
    public class CatalogService: ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var response = await _client.GetAsync("/api/v1/Catalog");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<CatalogModel>>();
                //return await response.ReadContentAs<List<CatalogModel>>();
            else
                return new List<CatalogModel>();
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var response = await _client.GetAsync($"/api/v1/Catalog/{id}");
            
            if (response.IsSuccessStatusCode)
               return await response.Content.ReadFromJsonAsync<CatalogModel>();
               //return await response.ReadContentAs<CatalogModel>();
            else
                return new CatalogModel();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var response = await _client.GetAsync($"/api/v1/Catalog/GetProductByCategory/{category}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<CatalogModel>>();
                //return await response.ReadContentAs<List<CatalogModel>>();
            else
                return new List<CatalogModel>();
        }  
    }
}