using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iex
{
	internal class Program
	{
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
