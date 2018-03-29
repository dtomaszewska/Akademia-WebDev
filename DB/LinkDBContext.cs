using Microsoft.EntityFrameworkCore;
using webdev.Models;

namespace webdev.DB
{
	public class LinkDBContext : DbContext
	{
		public LinkDBContext(DbContextOptions<LinkDBContext> options) : base(options) {}

		public DbSet<Link> Links { get; set; }
	}
}
