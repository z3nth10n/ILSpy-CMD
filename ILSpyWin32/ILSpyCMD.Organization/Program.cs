using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ILSpyCMD.API;

namespace ILSpyCMD.Organization
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			if (args == null || args != null && args.Length == 0)
			{
				Console.WriteLine("Args are null!");
				Console.Read();
				return;
			}

			string dir = args[0];
			if (!Directory.Exists(dir))
			{
				Console.WriteLine("Specified path isn't a directory!");
				Console.Read();
				return;
			}

			var groups = Extensions.GetFilesByExtension(dir, ".dll").InGroupsOf(20);

			//Console.WriteLine(groups.DebugGroups());
			//Console.Read();

			groups.ForEach((x, i) =>
			{
				string dir0 = Path.Combine(dir, "Group" + i);

				if (Directory.Exists(dir0))
					return;

				Directory.CreateDirectory(dir0);

				x.ForEach(y => File.Move(y, Path.Combine(dir0, Path.GetFileName(y))));
			});
		}
	}
}