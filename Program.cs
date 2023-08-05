using System.Runtime.InteropServices;

namespace iex
{
	internal static class Program
	{
		/// <summary>
		/// Entrypoint of application.
		/// </summary>
		/// <param name="args">Command line arguments</param>
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				if (args.Length > 0)
				{
					var url = args[0];
					LaunchInternetExplorer(url);
				}
				else
				{
					LaunchInternetExplorer("");
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "iex.exe", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Launch Internet Explorer.
		/// </summary>
		/// <param name="url">URL</param>
		private static void LaunchInternetExplorer(string url)
		{
			// Create COM object.
			var ie = new ComObject();
			if (!ie.CreateObject("InternetExplorer.Application"))
			{
				return;
			}

			// Show window. 
			if (!ie.GetPropertyAsBoolean("Visible"))
			{
				ie.SetPropertyAsBoolean("Visible", true);
			}

			if (string.IsNullOrEmpty(url))
			{
				return;
			}

			// Navigate to URL.
			ie.InvokeMethod("Navigate", url);
		}
	}
}
