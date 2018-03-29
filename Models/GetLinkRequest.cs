namespace webdev.Models
{
	public class GetLinkRequest
	{
		private int? _page = 1;

		public int? Page
		{
			get => _page;
			set => _page = value <= 0 ? 1 : value;
		}

		public string Search { get; set; }
	}
}