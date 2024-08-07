using BlazorApp1.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp1.Services
{
	public class ProductServices
	{
		private readonly HttpClient client;
		private readonly JsonSerializerOptions options;

		public ProductServices(HttpClient httpClient, JsonSerializerOptions optionsJson)
		{
			client = httpClient;
			options = optionsJson;
		}

		public async Task<List<Product>?> GetProductos()
		{
			var response = await client.GetAsync("/v1/products");
			return await JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync());
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
