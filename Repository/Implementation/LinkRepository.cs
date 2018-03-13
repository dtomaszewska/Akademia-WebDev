using System;
using System.Collections.Generic;
using System.Linq;
using webdev.Models;

namespace webdev.Repository.Implementation
{
	public class LinkRepository : ILinkRepository
	{
		private readonly List<Link> _links = new List<Link>();

		public List<Link> GetLinks()
		{
			return _links;
		}

		public void AddLink(Link link)
		{
			link.Id = _links.LastOrDefault()?.Id + 1 ?? 1;
			_links.Add(link);
		}

		public void UpdateLink(Link link)
		{
			var index = _links.FindIndex(x => x.Id == link.Id);
			_links.RemoveAt(index);
			_links.Insert(index, link);
		}

		public void DeleteLink(Link link)
		{
			var index = _links.FindIndex(x => x.Id == link.Id);
			_links.RemoveAt(index);
		}
	}
}
