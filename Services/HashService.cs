using System.Linq;
using HashidsNet;
using webdev.Repository;

namespace webdev.Services
{
	public class HashService : IHashService
	{
		private const string Key = "0123456789ABCDEF";
		private const string Salt = "Bardzo tajny klucz";
		private const int MinHashL = 5;

		private readonly ILinkRepository _repository;
		private readonly Hashids _hash = new Hashids(Salt, MinHashL, Key);

		public HashService(ILinkRepository repository)
		{
			_repository = repository;
		}

		public string CreateNextHash()
		{
			var nextHash = _hash.Encode(_repository.GetLinks().LastOrDefault()?.Id ?? 0);
			return nextHash;
		}
	}
}
