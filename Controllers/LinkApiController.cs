using System.Linq;
using Microsoft.AspNetCore.Mvc;
using webdev.Models;
using webdev.Repository;

namespace webdev.Controllers
{
	[Route("api/links")]
	public class LinkApiController : Controller
	{
		private readonly ILinkDBRepository _repository;

		public LinkApiController(ILinkDBRepository repository)
		{
			_repository = repository;
		}

		[HttpGet("{id}")]
		// GET api/links/{id}
		public IActionResult Get(int id)
		{
			return Ok(_repository.Get(id));
		}

		//GET api/links/?search={string}&page={int}
		[HttpGet]
		public IActionResult Get([FromQuery]GetLinkRequest request)
		{
			var (links, count) = _repository
				.Get(request.Search, request.Page.Value);
			var result = new SearchResult
			{
				PageInfo = new PageInfo
				{
					CurrentPage = request.Page.Value,
					MaxPage = count
				},
				Items = links.Select(x => new LinkResult(x))
			};
			return Ok(result);
		}

		// DELETE api/links/{id}
		[HttpDelete]
		public IActionResult Delete(int id)
		{
			_repository.Delete(id);
			return Ok();
		}

		//POST api/links
		[HttpPost]
		public IActionResult Post([FromBody]CreateLinkRequest createLink)
		{
			return Ok(_repository.Create(createLink.GetLink()));
		}

		//POST api/links
		[HttpPut]
		public IActionResult Put([FromBody]Link link)
		{
			return Ok(_repository.Update(link));
		}
	}
}