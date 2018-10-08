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
				sb.AppendFormat("Group{0}: {1}", i, string.Join(", ", x));
				sb.AppendLine();
			});

			return sb.ToString();
		}
	}
}