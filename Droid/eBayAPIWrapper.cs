using System;
using System.IO;
using System.Net;
using System.Net;

namespace NovarumAPIDemonstration
{
	/// <summary>
	/// Wrapper for eBays API
	/// </summary>
	public class eBayAPIWrapper
	{
		private static eBayAPIWrapper eBayService;

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		private eBayAPIWrapper ()
		{
			eBayService = new eBayAPIWrapper ();
		}


		/// <summary>
		/// Gets instance of the service.
		/// </summary>
		/// <returns>eBay API wrapper</returns>
		public eBayAPIWrapper getService()
		{
			if (eBayService == null) 
			{
				eBayService = new eBayAPIWrapper ();
			}
			return eBayService;
		}



	}
}

