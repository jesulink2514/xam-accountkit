using System;
using System.Threading.Tasks;
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
using Android.App;
using Android.Support.V7.App;
using Android.Content;
using Android.OS;

namespace AccountKit
{
	public class AccountKitAuth : INXAccountKitAuth
	{
		readonly Func<Activity> activityInContext;

		public AccountKitAuth(Func<Activity> activityInContext)
		{
			this.activityInContext = activityInContext;
		}

		public Task<NXLoginAccount> GetCurrentAccount(NXResponseType responseType)
		{
			var taskCompletionSource = new TaskCompletionSource<NXLoginAccount>();

			AKAccountKit.GetCurrentAccount(new InnerAccountKitCallback(taskCompletionSource));

			return taskCompletionSource.Task;
		}

		public Task<NXLoginResult> LoginWithAccountKit(NXLoginType loginType, NXResponseType responseType)
		{
			var taskCompletionSource = new TaskCompletionSource<NXLoginResult>();

			Action<IAKAccountKitLoginResult> onAKResult = (e) =>
			{
				if (e == null)
				{
					taskCompletionSource.SetResult(new NXLoginResult(false));
				}
				else
				{
					var tokenOrCode = responseType == NXResponseType.AccessToken
																	? e.AccessToken?.Token
																	: e.AuthorizationCode;
					var result = new NXLoginResult(true, false, tokenOrCode);
					taskCompletionSource.SetResult(result);
				}

				HiddenAccountKitActivity.Completed = null;
			};

			HiddenAccountKitActivity.Completed = onAKResult;

			var context = activityInContext?.Invoke();
			var intent = new Intent(context, typeof(HiddenAccountKitActivity));

			intent.PutExtra(nameof(NXResponseType), (int)responseType);
			intent.PutExtra(nameof(NXLoginType), (int)loginType);

			context?.StartActivity(intent);

			return taskCompletionSource.Task;
		}

		class InnerAccountKitCallback : Java.Lang.Object, IAKAccountKitCallback {
			readonly TaskCompletionSource<NXLoginAccount> taskCompleteionSource;

			public InnerAccountKitCallback(TaskCompletionSource<NXLoginAccount> taskCompleteionSource)
			{
				this.taskCompleteionSource = taskCompleteionSource;
			}

			public void OnError(AKAccountKitError p0) {
				//TODO return exception
				var result = new NXLoginAccount(true);

				taskCompleteionSource.SetResult(result);
			}

			public void OnSuccess(Java.Lang.Object p0) {
				var account = p0 as AKAccount;

				var result = new NXLoginAccount(phoneNumber: account.PhoneNumber.ToString(), email: account.Email);

				taskCompleteionSource.SetResult(result);
			}
		}
	}

	[Activity(Theme = "@style/EmptyTheme")]
	public class HiddenAccountKitActivity : AppCompatActivity
	{
		const int APP_REQUEST_CODE = 9999;
		internal static Action<IAKAccountKitLoginResult> Completed;

		protected override void OnCreate(Android.OS.Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			var responseType = (NXResponseType) Intent.Extras.GetInt(nameof(NXResponseType));
			var loginType = (NXLoginType)Intent.Extras.GetInt(nameof(NXLoginType));

			var intent = new Intent(this, typeof(AKAccountKitActivity));
			var configurationBuilder =
				new AKAccountKitConfiguration.AccountKitConfigurationBuilder(
					loginType == NXLoginType.Phone ? AKLoginType.Phone : AKLoginType.Email,
					responseType == NXResponseType.AuthorizationCode ? AKAccountKitActivity.ResponseType.Code : AKAccountKitActivity.ResponseType.Token);

			intent.PutExtra(
						AKAccountKitActivity.AccountKitActivityConfiguration,
						configurationBuilder.Build());

			StartActivityForResult(intent, APP_REQUEST_CODE);

		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
		{
			if (requestCode != APP_REQUEST_CODE)
			{
				base.OnActivityResult(requestCode, resultCode, data);

				Finish();
				return;
			}

			var result = (IAKAccountKitLoginResult)data?.GetParcelableExtra(AKAccountKitLoginResult.ResultKey);

			Completed?.Invoke(result);

			Finish();
		}
	}
}
