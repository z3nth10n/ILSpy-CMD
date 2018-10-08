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
			string dir = "";

			if (!Extensions.Main(args, out dir))
				return;

			var groups = Extensions.GetFilesByExtension(dir, ".dll").InGroupsOf(20);

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