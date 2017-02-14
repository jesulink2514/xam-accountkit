using AKAccountKitAuth = AccountKit.AccountKitAuth;

namespace Naxam.AccountKit.Demo.Droid
{
	public class AccountKitGetter : IAccountKitGetter
	{
		public Naxam.AccountKit.IAccountKitAuth AccountKitAuth
		{
			get
			{
				return new AKAccountKitAuth(() => Xamarin.Forms.Forms.Context as Android.App.Activity);
			}
		}
	}
}
