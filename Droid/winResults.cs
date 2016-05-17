
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EggsToGo;

namespace NovarumAPIDemonstration.Droid
{
	[Activity (Label = "Results")]	
	/// <summary>
	/// Displays list of results.
	/// </summary>
	public class winResults : Activity
	{
		private ListView lstItem;
		private Spinner sortMenu;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			this.Window.RequestFeature (WindowFeatures.NoTitle);
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.winResults);
			eBayAPIWrapper.getService ().con = this;

			//Create Sort Menu
			string[] menuItems = new string[5];
			menuItems[0] = "No Sort";
			menuItems [1] = "Sort: Price assending";
			menuItems [2] = "Sort: Price desending";
			menuItems [3] = "Sort: Most bids";
			menuItems [4] = "Sort: Least time remaining";
			this.sortMenu = FindViewById<Spinner>(Resource.Id.Query);
			this.sortMenu.Adapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleSpinnerItem, menuItems);
			this.sortMenu.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (Spinner_OnClick);


			//Create List
			this.lstItem = FindViewById<ListView> (Resource.Id.listCustom);
			this.lstItem.Adapter = new CustomListAdapter (this,eBayAPIWrapper.getService().results.item);
			this.lstItem.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs> (lstItem_OnClick);
		}

		/// <summary>
		/// Spinners on click.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e"></param>
		public void Spinner_OnClick(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			switch (e.Position)  //Updates the list with sorted data.
			{
			case 0: //No Sort
				this.lstItem.Adapter = new CustomListAdapter (this, eBayAPIWrapper.getService ().results.item);
				break;
			case 1: //Price Assending
				this.lstItem.Adapter = new CustomListAdapter(this,eBayAPIWrapper.getService().sortPriceHighest());
				break;
			case 2: //Price Desending
				this.lstItem.Adapter = new CustomListAdapter (this, eBayAPIWrapper.getService ().sortPriceLowest ());
				break;
			case 3: //Most Bids
				this.lstItem.Adapter = new CustomListAdapter (this, eBayAPIWrapper.getService ().sortNumberOfBids ());
				break;
			case 4: //Least time remaining
				this.lstItem.Adapter = new CustomListAdapter(this,eBayAPIWrapper.getService().sortEnding());
				break;
			}
		}

		public void lstItem_OnClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			try {
			string url = eBayAPIWrapper.getService ().results.item [e.Position].viewItemURL[0];
			Intent web = new Intent (Intent.ActionView, Android.Net.Uri.Parse (url));
			StartActivity (web);
			}
			catch(Exception ex)
			{
				Toast.MakeText(this,"Error opening web page",ToastLength.Short).Show();
			}
		}


	}
}

