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
		/// <summary>
		/// アプリケーションのエントリーポイント
		/// </summary>
		/// <param name="args">コマンドライン引数</param>
		static void Main(string[] args)
		{
			try
			{
				var url = 0 < args.Length ? args[0] : "";
				LaunchInternetExplorer(url);
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

			// Navigate to URL.
			if (url != "")
			{
				ie.InvokeMethod("Navigate", url);
			}
		}
	}
}
