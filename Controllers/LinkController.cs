using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using webdev.Models;
using webdev.Repository;
using webdev.Services;

namespace webdev.Controllers
{
	public class LinkController : Controller
	{
		private readonly ILinkRepository _repository;
		private readonly IHashService _hashService;

		public LinkController(ILinkRepository repository, IHashService hashService)
		{
			_repository = repository;
			_hashService = hashService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var links = _repository.GetLinks();
			return View(links);
		}

		[HttpPost]
		public IActionResult Create(Link link)
		{
			if (IsUriValid(link.LongLink))
			{
				link.Hash = _hashService.CreateNextHash();
				_repository.AddLink(link);
			}

			return Redirect(nameof(Index));
		}

		[HttpGet]
		public IActionResult Delete(Link link)
		{
			_repository.DeleteLink(link);
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
			_repository.UpdateLink(link);
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