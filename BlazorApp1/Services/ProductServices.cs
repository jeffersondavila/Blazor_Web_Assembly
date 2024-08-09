using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp1
{
	public class ProductServices : IProductService
	{
		private readonly HttpClient client;
		private readonly JsonSerializerOptions options;

		public ProductServices(HttpClient httpClient)
		{
			this.client = httpClient;
			options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		}

		public async Task<List<Product>?> GetProductos()
		{
			var response = await client.GetAsync("/api/v1/products");
			var content = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				//Console.WriteLine($"Error fetching products: {content}");
				throw new ApplicationException($"Failed to fetch products: {response.StatusCode}");
			}

			return JsonSerializer.Deserialize<List<Product>>(content, options);
		}

		public async Task PostProductos(Product productos)
		{
			var response = await client.PostAsJsonAsync("/api/v1/products", productos);
			var content = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				//Console.WriteLine($"Error posting product: {content}");
				throw new Exception($"Failed to post product: {response.StatusCode}");
			}
		}

		public async Task DeleteProductos(int codigoProducto)
		{
			var response = await client.DeleteAsync($"/api/v1/products/{codigoProducto}");
			var content = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				//Console.WriteLine($"Error deleting product: {content}");
				throw new Exception($"Failed to delete product: {response.StatusCode}");
			}
		}
	}

	public interface IProductService
	{
		Task<List<Product>?> GetProductos();
		Task PostProductos(Product product);
		Task DeleteProductos(int productId);
	}
}
