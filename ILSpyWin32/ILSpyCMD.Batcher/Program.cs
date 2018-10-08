using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using ILSpyCMD.API;

namespace ILSpyCMD.Batcher
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			string dir = "";

			if (!Extensions.Main(args, out dir))
				return;

			string[] folders = Directory.GetDirectories(dir, "*Group*", SearchOption.TopDirectoryOnly);

			folders.ForEach((x) =>
			{
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.FileName = "../ILSpyCMD.exe";
				startInfo.Arguments = $@"-n Presentacion -l ""C: \Users\arg10003\Downloads\Librerias ERP"" {x}";
				startInfo.UseShellExecute = false;

				using (Process p = new Process())
				{
					p.StartInfo = startInfo;
					p.Start();

					p.WaitForExit();
					p.Close();
				}
			});
		}
	}
}