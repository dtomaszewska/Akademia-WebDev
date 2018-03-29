using System.Collections.Generic;
using System.Linq;
using webdev.Models;
using webdev.Services;

namespace webdev.Repository.Implementation
{
	public class LinkRepository : ILinkDBRepository
	{
		private readonly IHashService _hashService;
		private readonly List<Link> _links = new List<Link>();
		private const int _itemsPerPage = 10;

		public LinkRepository(IHashService hashService)
		{
			_hashService = hashService;
		}

		public IEnumerable<Link> Get()
		{
			return _links;
		}

		public (IEnumerable<Link>, int) Get(string search, int page)
		{
			var linksFilteredByName = search != null ? _links
				.Where(x => x.LongLink.ToLower()
					.Contains(search)) : _links;
			var count = linksFilteredByName.Count();

			var paginatedLink = linksFilteredByName
				.OrderBy(x => x.Id)
				.Skip((page - 1) * _itemsPerPage)
				.Take(_itemsPerPage);

			return (paginatedLink, count % _itemsPerPage == 0 ? count / _itemsPerPage : count / _itemsPerPage + 1);
		}

		public Link Get(int id)
		{
			return _links.FirstOrDefault(x => x.Id == id);
		}

		public Link Create(Link link)
		{
			link.Hash = _hashService.CreateNextHash(_links.LastOrDefault()?.Id);
			link.Id = _links.LastOrDefault()?.Id + 1 ?? 1;
			_links.Add(link);

			return link;
		}

		public Link Update(Link link)
		{
			var index = _links.FindIndex(x => x.Id == link.Id);
			_links.RemoveAt(index);
			_links.Insert(index, link);

			return link;
		}

		public void Delete(int id)
		{
			var index = _links.FindIndex(x => x.Id == id);
			_links.RemoveAt(index);
		}
	}
}
