using BlazorApp1.Models;
using System.Text.Json;

namespace BlazorApp1.Services
{
	public class CategoryServices : ICategoryService
	{
		private readonly HttpClient client;
		private readonly JsonSerializerOptions options;

		public CategoryServices(HttpClient httpClient)
		{
			this.client = httpClient;
			options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		}

		public async Task<List<Category>?> GetCategorias()
		{
			var response = await client.GetAsync("/v1/categories");
			var content = await response.Content.ReadAsStringAsync();

			if(!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}

			return JsonSerializer.Deserialize<List<Category>>(content,options);
		}
	}

	public interface ICategoryService
	{
		Task<List<Category>> GetCategorias();
	}
}
