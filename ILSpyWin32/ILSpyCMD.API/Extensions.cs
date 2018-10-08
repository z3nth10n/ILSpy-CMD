using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ILSpyCMD.API
{
	public static class Extensions
	{
		public static IEnumerable<IEnumerable<T>> InGroupsOf<T>(this IEnumerable<T> ts, int num)
		{
			return ts.Select((str, index) => new { str, index })
					.GroupBy(x => x.index / num)
					.Select(g => g.Select(x => x.str));
		}

		public static IEnumerable<string> GetFilesByExtension(string directoryPath, string extension, SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			return
				Directory.EnumerateFiles(directoryPath, "*" + extension, searchOption)
					.Where(x => string.Equals(Path.GetExtension(x), extension, StringComparison.InvariantCultureIgnoreCase));
		}

		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (T element in source)
				action(element);
		}

		public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
		{
			int i = 0;

			foreach (T element in source)
			{
				action(element, i);
				++i;
			}
		}

		public static string DebugGroups(this IEnumerable<IEnumerable<string>> groups)
		{
			StringBuilder sb = new StringBuilder();

			groups.ForEach((x, i) =>
			{
				sb.AppendFormat($"Group${i}: {0}", string.Join(", ", x));
				sb.AppendLine();
			});

			return sb.ToString();
		}

		public static void WriteRead(string str)
		{
			Console.WriteLine(str);
			Console.Read();
		}

		public static bool Main(string[] args, out string dir)
		{
			if (args == null || args != null && args.Length == 0)
			{
				WriteRead("Args are null!");
				dir = "";
				return false;
			}

			dir = args[0];

			if (!Directory.Exists(dir))
			{
				WriteRead("Specified path isn't a directory!");
				return false;
			}

			return true;
		}
	}
}