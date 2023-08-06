using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iex
{
	/// <summary>
	/// Represents COM object.
	/// </summary>
	internal class ComObject
	{
		/// <summary>
		/// Instance of COM object.
		/// </summary>
		private object unknown = null;

		/// <summary>
		/// Constructor. 
		/// </summary>
		public ComObject()
		{
			this.unknown = null;
		}

		/// <summary>
		/// Create COM object.
		/// </summary>
		/// <param name="progID"></param>
		/// <returns></returns>
		public bool CreateObject(string progID)
		{
			this.unknown = null;
			Type t = Type.GetTypeFromProgID(progID);
			if (t == null)
			{
				return false;
			}
			this.unknown = Activator.CreateInstance(t);
			return true;
		}

		/// <summary>
		/// Get property as bool.
		/// </summary>
		/// <param name="name">Name of property</param>
		/// <returns></returns>
		public bool GetPropertyAsBoolean(string name)
		{
			if (this.unknown == null)
				return false;
			var t = this.unknown.GetType();
			var property = t.InvokeMember(name, System.Reflection.BindingFlags.GetProperty, null, this.unknown, null);
			return ParseBool(property);
		}

		/// <summary>
		/// Set bool property.
		/// </summary>
		/// <param name="name">Name of property</param>
		/// <param name="value">Value</param>
		/// <returns></returns>
		public bool SetPropertyAsBoolean(string name, bool value)
		{
			if (this.unknown == null)
				return false;
			var t = this.unknown.GetType();
			var result = t.InvokeMember(name, System.Reflection.BindingFlags.SetProperty, null, this.unknown, new object[] { value });
			return ParseBool(result);
		}

		/// <summary>
		/// Invoke method.
		/// </summary>
		/// <param name="name">Name of method</param>
		/// <param name="value">Argument</param>
		/// <returns></returns>
		public bool InvokeMethod(string name, string value)
		{
			if (this.unknown == null)
				return false;
			var t = this.unknown.GetType();
			var result = t.InvokeMember(
				name,
				System.Reflection.BindingFlags.InvokeMethod,
				null,
				this.unknown,
				new object[] { value });
			return ParseBool(result);
		}

		/// <summary>
		///Parse boolean value.
		/// </summary>
		/// <param name="unknown"></param>
		/// <returns></returns>
		private static bool ParseBool(object unknown)
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
	}
}
