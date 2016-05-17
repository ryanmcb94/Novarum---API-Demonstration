using System;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using Xamarin.Forms;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Graphics;
using Android.Views;
using System.Net;

namespace NovarumAPIDemonstration.Droid
{
	public class CustomListAdapter: BaseAdapter<Item>
	{
		List<Item> items;
		Context context;

		/// <summary>
		/// Initializes a new instance of the ListAdapter class. this is a custom list adapter for the eBay items
		/// and displays title, price, condition and an image for each item.
		/// </summary>
		/// <param name="con">Context</param>
		/// <param name="items">List of items being displayed.</param>
		public CustomListAdapter (Context con, List<Item> items)
		{
			this.items = items;
			this.context = con;
		}

		#region implemented abstract members of BaseAdapter
		/// <param name="position">The position of the item within the adapter's data set whose row id we want.</param>
		/// <summary>
		/// Get the row id associated with the specified position in the list.
		/// </summary>
		/// <returns>To be added.</returns>
		public override long GetItemId (int position)
		{
			return position;
		}

		/// <param name="position">The position of the item within the adapter's data set of the item whose view
		///  we want.</param>
		/// <summary>
		/// Gets the view.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="convertView">Convert view.</param>
		/// <param name="parent">Parent.</param>
		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			Item item = items [position];
			Android.Views.View v = convertView;

			if (v == null) { //Initalize Row.
				LayoutInflater lf = (LayoutInflater)this.context.GetSystemService (Context.LayoutInflaterService);
				v = lf.Inflate (Resource.Layout.CustomRowLayout, null);
			}
			if(item !=null) //Display Items
			{
				v.FindViewById<TextView> (Resource.Id.txtTitle).Text = item.title[0];
				v.FindViewById<TextView> (Resource.Id.txtCondition).Text = item.condition[0].conditionDisplayName[0];
				v.FindViewById<TextView> (Resource.Id.txtPrice).Text = item.sellingStatus [0].currentPrice [0].__value__;
				v.FindViewById<ImageView> (Resource.Id.imgIcon).SetImageBitmap(this.getOnlineImage(item.galleryURL[0]));
			}
			return v;
		}

		/// <summary>
		/// How many items are in the data set represented by this Adapter.
		/// </summary>
		/// <value>To be added.</value>
		public override int Count {
			get {
				return items.Count;
			}
		}
		#endregion
		#region implemented abstract members of BaseAdapter
		/// <summary>
		/// Gets the Item at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public override Item this [int index] {
			get {
				return items [index];
			}
		}
		#endregion

		/// <summary>
		/// Download Image from website
		/// </summary>
		/// <returns>The online image.</returns>
		/// <param name="url">URL.</param>
		private Bitmap getOnlineImage(string url)
		{
			using(WebClient client = new WebClient())
			{
				var imageBytes = client.DownloadData(url);
				if(imageBytes !=null && imageBytes.Length>0)
				{
					return BitmapFactory.DecodeByteArray(imageBytes,0,imageBytes.Length);
				}
			}
			return null;
		}
	}
}