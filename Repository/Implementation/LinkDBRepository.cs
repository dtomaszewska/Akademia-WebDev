using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webdev.DB;
using webdev.Models;
using webdev.Services;

namespace webdev.Repository.Implementation
{
	public class LinkDBRepository : ILinkDBRepository
	{
		private readonly LinkDBContext _context;
		private readonly IHashService _hashService;
		private const int _itemsPerPage = 10;

		public LinkDBRepository(LinkDBContext context, IHashService hashService)
		{
			_context = context;
			_hashService = hashService;
		}

		public void Delete(int id)
		{
			var stopEntity = _context.Links.Find(id);
			_context.Links.Remove(stopEntity);
			_context.SaveChanges();
		}

		public Link Create(Link link)
		{
			link.Hash = _hashService.CreateNextHash(_context.Links.LastOrDefault()?.Id);
			_context.Links.Add(link);
			_context.SaveChanges();
			return link;
		}

		public Link Update(Link link)
		{
			_context.Links.Attach(link);
			_context.Entry(link).State = EntityState.Modified;
			_context.SaveChanges();
			return link;
		}

		public (IEnumerable<Link>, int) Get(string search, int page)
		{
			var linksFilteredByName = search != null ? _context.Links
				.Where(x => x.LongLink.ToLower()
					.Contains(search)) : _context.Links;
			var count = linksFilteredByName.Count();

			var paginatedLink = linksFilteredByName
				.OrderBy(x => x.Id)
				.Skip((page - 1) * _itemsPerPage)
				.Take(_itemsPerPage);

			return (paginatedLink, count % _itemsPerPage == 0 ? count / _itemsPerPage : count / _itemsPerPage + 1);
		}


		public Link Get(int id)
		{
			return _context.Links.Find(id);
		}

		public IEnumerable<Link> Get()
		{
			return _context.Links;
		}
	}
}
