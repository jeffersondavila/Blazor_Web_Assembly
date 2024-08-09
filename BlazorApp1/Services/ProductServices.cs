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
				throw new ApplicationException(content);
			}

			return JsonSerializer.Deserialize<List<Product>>(content, options);
		}

		public async Task PostProductos(Product productos)
		{
			var response = await client.PostAsync("/v1/products", JsonContent.Create(productos));
			var content = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception("No se pudo llevar a cabo el insert de los productos");
			}
		}

		public async Task DeleteProductos(int codigoProducto)
		{
			var response = await client.DeleteAsync($"/v1/products/{codigoProducto}");
			var content = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception("No se pudo llevar a cabo la eliminacion de los productos");
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
