using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using webdev.Models;
using webdev.Repository;

namespace webdev.Controllers
{
	public class LinkController : Controller
	{
		private readonly ILinkDBRepository _repository;

		public LinkController(ILinkDBRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var links = _repository.Get(string.Empty, 0);
			return View(links.Item1.ToList());
		}

		[HttpPost]
		public IActionResult Create(Link link)
		{
			if (IsUriValid(link.LongLink))
			{
				_repository.Create(link);
			}

			return Redirect(nameof(Index));
		}

		[HttpGet]
		public IActionResult Delete(Link link)
		{
			_repository.Delete(link.Id);
			return Redirect(nameof(Index));
		}

		[HttpGet]
		public IActionResult Edit(Link link)
		{
			return View(link);
		}

		[HttpPost]
		public IActionResult Update(Link link)
		{
			_repository.Update(link);
			return Redirect(nameof(Index));
		}

		private bool IsUriValid(string uri)
		{
			if (!Regex.IsMatch(uri, @"^(https?|ftp)://.*"))
				uri = "http://" + uri;
			return Uri.IsWellFormedUriString(uri, UriKind.Absolute);
		}
	}
}