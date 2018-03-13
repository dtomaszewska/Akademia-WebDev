using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webdev.Models;

namespace webdev.Repository
{
    public interface ILinkRepository
	{
		List<Link> GetLinks();

		void AddLink(Link link);

		void UpdateLink(Link link);

		void DeleteLink(Link link);
	}
}
