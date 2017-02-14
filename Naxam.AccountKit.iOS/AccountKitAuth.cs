using System;
using System.Threading.Tasks;
using INXAccountKitAuth = Naxam.AccountKit.IAccountKitAuth;
using NXLoginType = Naxam.AccountKit.LoginType;
using NXResponseType = Naxam.AccountKit.ResponseType;
using NXLoginAccount = Naxam.AccountKit.LoginAccount;
using NXLoginResult = Naxam.AccountKit.LoginResult;
using UIKit;
using Foundation;
using Acr.Support.iOS;

namespace AccountKit
{
	public class AccountKitAuth : NSObject, INXAccountKitAuth, IAKFViewControllerDelegate
	{
		readonly AKFTheme theme;

		AKFAccountKit accountKit;
		UIViewController pendingLoginViewController;

		public AccountKitAuth(AKFTheme theme)
		{
			this.theme = theme;
		}

		public Task<NXLoginAccount> GetCurrentAccount(NXResponseType responseType)
		{
			var taskCompletionSource = new TaskCompletionSource<NXLoginAccount>();

			InitAK(responseType == NXResponseType.AccessToken 
			       ? AKFResponseType.AccessToken
			       : AKFResponseType.AuthorizationCode);

			accountKit.RequestAccount((obj, error) =>
			{
				if (error != null)
				{
					taskCompletionSource.SetResult(new NXLoginAccount(true));
					return;
				}

				var account = obj as IAKFAccount;

				var phoneNumber = account?.PhoneNumber?.stringRepresentation()?.ToString();
				taskCompletionSource.SetResult(new NXLoginAccount(obj == null, phoneNumber, account?.EmailAddress));
			});

			return taskCompletionSource.Task;
		}

		public Task<NXLoginResult> LoginWithAccountKit(NXLoginType type, NXResponseType responseType)
		{
			InitAK(responseType == NXResponseType.AccessToken
				   ? AKFResponseType.AccessToken
				   : AKFResponseType.AuthorizationCode, true);

			loginTaskCompletionSource?.TrySetCanceled();
			loginTaskCompletionSource = new TaskCompletionSource<NXLoginResult>();
			pendingLoginViewController = type == NXLoginType.Phone
															? accountKit.GetViewControllerForPhoneLogin()
															: accountKit.GetViewControllerForEmailLogin();

			var loginViewController = (pendingLoginViewController as IAKFViewController);

			if (loginViewController != null)
			{
				loginViewController.Delegate = this;
				loginViewController.EnableSendToFacebook = true;

				loginViewController.Theme = theme;
			}

			NSOperationQueue.MainQueue.BeginInvokeOnMainThread(() =>
			{
				var vcInContext = GetTopMostController();

				vcInContext.PresentViewController(pendingLoginViewController, true, null);
			});

			return loginTaskCompletionSource.Task;
		}

		UIViewController GetTopMostController()
		{
			var vc = UIApplication.SharedApplication.GetTopViewController();

			if (vc is AKFNavigationController) {
				vc = vc.PresentingViewController;
			}

			return vc;
		}

		void InitAK(AKFResponseType responseType, bool forced = false) {
			if (accountKit == null || forced) { 
				accountKit = new AKFAccountKit(responseType);
			}
		}

		TaskCompletionSource<NXLoginResult> loginTaskCompletionSource;

		[Export("viewControllerDidCancel:")]
		public void DidCancel(UIViewController viewController)
		{
			loginTaskCompletionSource?.SetResult(new NXLoginResult(false, true));
			loginTaskCompletionSource = null;
		}

		[Export("viewController:didCompleteLoginWithAuthorizationCode:state:")]
		public void DidCompleteLoginWithAuthorizationCode(UIViewController viewController, string code, string state)
		{
			loginTaskCompletionSource?.SetResult(new NXLoginResult(true, false, code));
			loginTaskCompletionSource = null;
		}

		[Export("viewController:didCompleteLoginWithAccessToken:state:")]
		public void DidCompleteLoginWithAccessToken(UIViewController viewController, IAKFAccessToken accessToken, string state)
		{
			loginTaskCompletionSource?.SetResult(new NXLoginResult(true, false, accessToken.TokenString));
			loginTaskCompletionSource = null;
		}

		[Export("viewController:didFailWithError:")]
		public void DidFailWithError(UIViewController viewController, NSError error)
		{
			loginTaskCompletionSource?.SetResult(new NXLoginResult(false));
			loginTaskCompletionSource = null;
		}
	}
}