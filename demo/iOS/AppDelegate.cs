using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using AKAccountKitAuth = AccountKit.AccountKitAuth;
using AKTheme = AccountKit.AKFTheme;

namespace Naxam.AccountKit.Demo.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			Xamarin.Forms.DependencyService.Register<IAccountKitGetter, AccountKitGetter>();

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}

	public class AccountKitGetter : IAccountKitGetter
	{
		public Naxam.AccountKit.IAccountKitAuth AccountKitAuth
		{
			get
			{
				return new AKAccountKitAuth(AKTheme.DefaultTheme);
			}
		}
	}
}
