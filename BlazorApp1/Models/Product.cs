namespace BlazorApp1
{
	public class Product
	{
		public int Id { get; set; }
		public string title { get; set; }
		public decimal? price { get; set; }
		public string description { get; set; }
		public int categoryId { get; set; }
		public string[] images { get; set; }
		public string? Image { get; set; }
	}
}
