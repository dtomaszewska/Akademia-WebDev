using System.Linq;
using Microsoft.AspNetCore.Mvc;
using webdev.Models;
using webdev.Repository;

namespace webdev.Controllers
{
	public class RedirectController : Controller
	{
		private readonly ILinkRepository _repository;

		public RedirectController(ILinkRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		[Route("/{hash}")]
		public IActionResult Open(string hash)
		{
			var link = _repository.GetLinks().FirstOrDefault(x => x.Hash == hash);
			return link.LongLink.StartsWith("http") || link.LongLink.StartsWith("ftp")
				? Redirect(link.LongLink)
				: Redirect("http://" + link.LongLink);
		}
	}
}