using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models.Db
{
	public class FileDb
	{
		public int Id { get; set; }
		public string FileName { get; set; } = null!;
		public string FilePath { get; set; } = null!;
		public string GuidName { get; set; } = null!;
	}
}
