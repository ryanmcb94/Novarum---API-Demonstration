
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
using System.Net;
using System.Runtime.Serialization.Json;
using System.Diagnostics;

namespace NovarumAPIDemonstration.Droid
{
	[Activity (Label = "eBay Search", Icon = "@drawable/icon", MainLauncher = true)]			
	public class winSearch : Activity
	{
		private Button btnSearch;
		private ImageView image;
		private EditText txtSearch;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			//UI setup
			this.Window.RequestFeature (WindowFeatures.NoTitle);
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.winSearch);
			this.txtSearch = FindViewById<EditText> (Resource.Id.txtSearch);
			this.btnSearch = FindViewById<Button>(Resource.Id.btnSearch);

			this.btnSearch.Click += delegate { //Search, then navigate to results window.
				if(txtSearch.Text !="")
				{
					eBayAPIWrapper.getService().findItem(txtSearch.Text);
					StartActivity(typeof(winResults));
				}
			};

				
				//this.image.SetImageResource (Resource.Drawable.ebaylogo);
			
		}
	}
}

