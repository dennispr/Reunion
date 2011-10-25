using Sifteo;
using System;

namespace Game
{
	/// <summary>
	/// Fast log.
	/// </summary>
	public static class FastLog
	{
		/// <summary>
		/// Debug the specified obj.
		/// </summary>
		/// <param name='obj'>
		/// Object.
		/// </param>
		public static void Debug(System.Object obj)
		{
			Log.Debug(obj.ToString());
		}
		
		/// <summary>
		/// Info the specified obj.
		/// </summary>
		/// <param name='obj'>
		/// Object.
		/// </param>
		public static void Info(System.Object obj)
		{
			Log.Info(obj.ToString());
		}
		
		/// <summary>
		/// Error the specified obj.
		/// </summary>
		/// <param name='obj'>
		/// Object.
		/// </param>
		public static void Error(System.Object obj)
		{
			Log.Error(obj.ToString());
		}
		
		/// <summary>
		/// Warning the specified obj.
		/// </summary>
		/// <param name='obj'>
		/// Object.
		/// </param>
		public static void Warning(System.Object obj)
		{
			Log.Warning(obj.ToString());
		}
	}
}