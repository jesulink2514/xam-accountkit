using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AKAccountKit = AccountKit.AccountKit;

namespace Naxam.AccountKit.Demo.Droid
{
	[Activity(Label = "Naxam.AccountKit.Demo.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			AKAccountKit.Initialize(this);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			Xamarin.Forms.DependencyService.Register<IAccountKitGetter, AccountKitGetter>();

			LoadApplication(new App());
		}
	}
}
