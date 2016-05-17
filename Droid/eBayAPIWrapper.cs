using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;
using Android.Content;

namespace NovarumAPIDemonstration.Droid
{
	/// <summary>
	/// Wrapper for eBays API
	/// </summary>
	public class eBayAPIWrapper
	{
		public Context  con { get; set; }
		private static eBayAPIWrapper eBayService;
		public SearchResult results {get;set;}

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		private eBayAPIWrapper ()
		{
		}


		/// <summary>
		/// Gets instance of the service.
		/// </summary>
		/// <returns>eBay API wrapper</returns>
		public static eBayAPIWrapper getService()
		{
			if (eBayService == null) 
			{
				eBayService = new eBayAPIWrapper ();
			}

			return eBayService;
		}

		/// <summary>
		/// Searches eBay for any items matching query
		/// </summary>
		/// <param name="searchQuery">Search query.</param>
		public void findItem(string searchQuery,Context con)
		{
			this.con = con;
			string call = String.Format("http://svcs.ebay.com/services/search/FindingService/v1?SECURITY-APPNAME=RyanMcBr-NovarumT-PRD-34d8cb02c-29785617&OPERATION-NAME=findItemsByKeywords&SERVICE-VERSION=1.0.0&RESPONSE-DATA-FORMAT=JSON&REST-PAYLOAD&keywords={0}%203g",searchQuery);
			HttpWebRequest request = WebRequest.Create(call) as HttpWebRequest;
			int attempts = 0;
			while (attempts < 4) 
			{
				try 
				{
					using (HttpWebResponse responce = request.GetResponse () as HttpWebResponse) 
					{ // Make API Call
						//Get Data
						DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer (typeof(RootObject));
						object data = jsonSerializer.ReadObject (responce.GetResponseStream ());
						RootObject ro = (RootObject)data;
						this.results = ro.findItemsByKeywordsResponse [0].searchResult [0];
						attempts = 4;
					}	
				}
				catch (Exception e) 
				{
					attempts++;
					Toast.MakeText (this.con,"Request Failed: Retrying. Attempt Number: " + attempts.ToString(), ToastLength.Short).Show ();
				}
			}
			validate ();
		}

		/// <summary>
		/// Validates the data recived
		/// </summary>
		public void validate()
		{
			foreach (Item i in this.results.item) 
			{
				if (i.sellingStatus [0].bidCount == null)  //Bid Count
				{
					i.sellingStatus [0].bidCount = new List<string>();
					i.sellingStatus [0].bidCount.Add ("0");
				}
			}
		}

		public List<Item> sortPriceHighest()
		{
			return (List<Item>)this.results.item.OrderBy (Item => Item.sellingStatus [0].currentPrice [0]).ToList();
		}

		public List<Item> sortPriceLowest()
		{
			return this.results.item.OrderByDescending (Item => Item.sellingStatus [0].currentPrice [0]).ToList();
		}

		public List<Item> sortNumberOfBids()
		{
			return (List<Item>)this.results.item.OrderBy (Item => Item.sellingStatus [0].bidCount [0]).ToList();
		}

		public List<Item> sortEnding()
		{
			return (List<Item>)this.results.item.OrderBy (Item => Item.sellingStatus [0].timeLeft [0]).ToList();
		}

	}
}

