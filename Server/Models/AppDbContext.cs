using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
	public class AppDbContext : DbContext
	{
		public DbSet<Db.User> Users { get; set; }

		public DbSet<Db.Message> Messages { get; set; }

		public DbSet<Db.FileDb> Files { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			string executablePath = Assembly.GetExecutingAssembly().Location;
			string directoryPath = Path.GetDirectoryName(executablePath);
			string databasePath = Path.Combine(directoryPath, "ChatDb.db");
			optionsBuilder.UseSqlite($"Data Source={databasePath}");
		}
	}
}
