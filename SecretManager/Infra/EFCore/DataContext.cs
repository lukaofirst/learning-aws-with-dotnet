using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.EFCore
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions) { }

		public DbSet<Person> Persons { get; set; }
	}
}