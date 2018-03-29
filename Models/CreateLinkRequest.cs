namespace webdev.Models
{
	public class CreateLinkRequest
	{
		public string LongLink { get; set; }

		public Link GetLink()
		{
			var link = new Link
			{
				LongLink = this.LongLink
			};

			return link;
		}
	}
}