
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
		private ListView list;
		private Spinner sortMenu;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.winResults);

			//Create Sort Menu
			string[] menuItems = new string[4];
			menuItems [0] = "Sort: price desending";
			menuItems [1] = "Sort: price assending";
			menuItems [2] = "Sort: most bids";
			menuItems [3] = "Sort: Least time remaining";
			this.sortMenu = FindViewById<Spinner>(Resource.Id.Query);
			this.sortMenu.Adapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleSpinnerItem, menuItems);
			this.sortMenu.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (Spinner_OnClick);


			//Create List
			this.list = FindViewById<ListView> (Resource.Id.listCustom);
			this.list.Adapter = new CustomListAdapter (this,eBayAPIWrapper.getService().results.item);
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
			case 0: //Price Desending
				this.list.Adapter = new CustomListAdapter(this,eBayAPIWrapper.getService().sortPriceHighest());
				break;
			case 1: //Price Assending
				this.list.Adapter = new CustomListAdapter (this, eBayAPIWrapper.getService ().sortPriceLowest ());
				break;
			case 2: //Most Bids
				this.list.Adapter = new CustomListAdapter (this, eBayAPIWrapper.getService ().sortNumberOfBids ());
				break;
			case 3: //Least time remaining
				this.list.Adapter = new CustomListAdapter(this,eBayAPIWrapper.getService().sortEnding());
				break;
			}
		}


	}
}

