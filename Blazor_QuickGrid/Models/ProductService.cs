using System.Net.Http.Json;
namespace Blazor_QuickGrid.Models
{
    /// <summary>
    /// Class that contains method to perform HTTP Operations
    /// to Access API and Perform CRUD Operations
    /// </summary>
    public class ProductService(HttpClient client)
    {
        private string url = "https://localhost:7273/products";

        public async Task<IQueryable<ProductInfo>> GetAsync()
        {
            var result = await client.GetFromJsonAsync<List<ProductInfo>>(url);
            return result?.AsQueryable() ?? Enumerable.Empty<ProductInfo>().AsQueryable();
        }

        public async Task<ProductInfo> GetAsync(string id)
        {
            var result = await client.GetFromJsonAsync<ProductInfo>($"{url}/{id}");
            return result ?? new ProductInfo();
        }

        public async Task<ProductInfo> PostAsync(ProductInfo product)
        { 
            var response = await client.PostAsJsonAsync <ProductInfo>(url, product);
            if(response.IsSuccessStatusCode)
            { 
               var result = await response.Content.ReadFromJsonAsync<ProductInfo>();
                    return result ?? new ProductInfo();
            }
            return new ProductInfo();
        }

        public async Task<ProductInfo> PutAsync(string id,ProductInfo product)
        { 
            var response = await client.PutAsJsonAsync <ProductInfo>($"{url}/{id}", product);
            if(response.IsSuccessStatusCode)
            { 
               var result = await response.Content.ReadFromJsonAsync<ProductInfo>();
                    return result ?? new ProductInfo();
            }
            return new ProductInfo();
        }

        public async Task<ProductInfo> DeleteAsync(string id)
        {
          var result =  await client.DeleteFromJsonAsync<ProductInfo>($"{url}/{id}");
            return result ?? new ProductInfo();
        }
    }
}
