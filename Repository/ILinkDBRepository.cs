using System.Collections.Generic;
using webdev.Models;

namespace webdev.Repository
{
	public interface ILinkDBRepository
	{
		IEnumerable<Link> Get();
		(IEnumerable<Link>, int) Get(string search, int page);
		Link Get(int id);
		Link Create(Link link);
		Link Update(Link link);
		void Delete(int id);
	}
}
