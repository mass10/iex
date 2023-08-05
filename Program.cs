using System.Runtime.InteropServices;

namespace iex
{
	internal static class Program
	{
		/// <summary>
		/// �A�v���P�[�V�����̃G���g���[�|�C���g�ł��B
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				if (args.Length > 0 )
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

		private static bool GetPropertyAsBoolean(object instance, string name)
		{
			var t = instance.GetType();
			var property = t.InvokeMember(name, System.Reflection.BindingFlags.GetProperty, null, instance, null);
			return ParseBool(property);
		}

		private static bool SetPropertyAsBoolean(object instance, string name, bool value)
		{
			var t = instance.GetType();
			var result = t.InvokeMember(name, System.Reflection.BindingFlags.SetProperty, null, instance, new object[] { value });
			return ParseBool(result);
		}

		private static bool InvokeMethod(object instance, string name, string value)
		{
			var t = instance.GetType();
			var result = t.InvokeMember(
				name,
				System.Reflection.BindingFlags.InvokeMethod,
				null,
				instance,
				new object[] { value });
			return ParseBool(result);
		}

		private static bool ParseBool(object? unknown)
		{
			if (unknown == null)
				return false;

			if (unknown is bool)
				return (bool)unknown;

			if (unknown.Equals("1"))
				return true;

			if (unknown.Equals("yes"))
				return true;

			try
			{
				return bool.Parse("" + unknown);
			}
			catch
			{
				return false;
			}
		}

		public static object? CreateObject(string progID)
		{
			Type? t = Type.GetTypeFromProgID(progID);
			if (t == null)
			{
				return null;
			}
			return Activator.CreateInstance(t);
		}

		private static void LaunchInternetExplorer(string url)
		{
			// Internet Explorer �I�u�W�F�N�g�̃C���X�^���X�𐶐�
			var unknown = CreateObject("InternetExplorer.Application");
			if (unknown == null)
			{
				return;
			}

			// Visible �� true �ɂ���܂ŃE�B���h�E�������܂���B
			if (!GetPropertyAsBoolean(unknown, "Visible"))
			{
				SetPropertyAsBoolean(unknown, "Visible", true);
			}

			if (string.IsNullOrEmpty(url))
			{
				return;
			}

			// URL ���J���܂��B
			InvokeMethod(unknown, "Navigate", url);
		}
	}
}
