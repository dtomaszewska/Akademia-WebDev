using System.Collections.Generic;

namespace webdev.Models
{
	public class SearchResult
	{
		public IEnumerable<LinkResult> Items { get; set; }
		public PageInfo PageInfo { get; set; }
	}

	public class PageInfo
	{
		public int CurrentPage { get; set; }

		public int MaxPage { get; set; }
	}

	public class LinkResult
	{
		public LinkResult(Link link)
		{
			Id = link.Id;
			LongLink = link.LongLink;
		}
		public int Id { get; set; }
		public string LongLink { get; set; }

	}
}