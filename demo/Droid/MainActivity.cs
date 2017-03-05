using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using INXAccountKitAuth = Naxam.AccountKit.IAccountKitAuth;
using NXLoginType = Naxam.AccountKit.LoginType;
using NXResponseType = Naxam.AccountKit.ResponseType;
using NXLoginAccount = Naxam.AccountKit.LoginAccount;
using NXLoginResult = Naxam.AccountKit.LoginResult;
using AKAccountKit = AccountKit.AccountKit;
using IAKAccountKitCallback = AccountKit.IAccountKitCallback;
using AKAccountKitLoginResult = AccountKit.AccountKitLoginResult;
using AKAccountKitActivity = AccountKit.UI.AccountKitActivity;
using AKLoginType = AccountKit.UI.LoginType;
using AKAccountKitConfiguration = AccountKit.UI.AccountKitConfiguration;
using AKAccount = AccountKit.Account;
using AKAccountKitError = AccountKit.AccountKitError;
using IAKAccountKitLoginResult = AccountKit.IAccountKitLoginResult;

namespace Naxam.AccountKit.Demo.Droid
{
	[Activity(Label = "Naxam.AccountKit.Demo.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		const int APP_REQUEST_CODE = 9999;

		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			AKAccountKit.Initialize(this);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			Xamarin.Forms.DependencyService.Register<IAccountKitGetter, AccountKitGetter>();

			LoadApplication(new App());

			//var responseType = NXResponseType.AuthorizationCode;//(NXResponseType)Intent.Extras.GetInt(nameof(NXResponseType));
			//var loginType = NXLoginType.Phone;//(NXLoginType)Intent.Extras.GetInt(nameof(NXLoginType));

			//var intent = new Intent(this, typeof(AKAccountKitActivity));
			//var configurationBuilder =
			//	new AKAccountKitConfiguration.AccountKitConfigurationBuilder(
			//		loginType == NXLoginType.Phone ? AKLoginType.Phone : AKLoginType.Email,
			//		responseType == NXResponseType.AuthorizationCode ? AKAccountKitActivity.ResponseType.Code : AKAccountKitActivity.ResponseType.Token);

			//intent.PutExtra(
			//			AKAccountKitActivity.AccountKitActivityConfiguration,
			//			configurationBuilder.Build());

			//StartActivityForResult(intent, APP_REQUEST_CODE);
		}
	}
}
