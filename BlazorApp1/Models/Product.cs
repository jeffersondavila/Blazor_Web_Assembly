using System.Text.Json;

namespace BlazorApp1
{
    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public decimal? price { get; set; }
        public string description { get; set; }
        public int categoryId { get; set; }
        public string[] images { get; set; }
        public Category category { get; set; }

        public string GetFirstImageUrl()
        {
            if (images != null && images.Length > 0)
            {
                try
                {
                    var actualImages = JsonSerializer.Deserialize<string[]>(images[0]);

                    if (actualImages != null && actualImages.Length > 0)
                    {
                        //Console.WriteLine($"URL P {actualImages[0]}");
                        return actualImages[0];
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error parsing image JSON for product {id}: {ex.Message}");
                }
            }
            //Console.WriteLine($"URL C {category?.image}");
            return category?.image ?? string.Empty;
        }
    }
}
