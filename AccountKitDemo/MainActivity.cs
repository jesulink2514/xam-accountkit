using Android.App;
using Android.Widget;
using Android.OS;

using AccountKit;
using AccountKit.UI;
using Android.Content;
using Android.Support.V7.App;

namespace AccountKitDemo
{
	[Activity(Label = "AccountKitDemo", MainLauncher = true, Icon = "@mipmap/icon", Theme="@style/AppTheme")]
	public class MainActivity : AppCompatActivity
	{
		const int APP_REQUEST_CODE = 999;
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			AccountKit.AccountKit.Initialize(this);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);

			button.Click += delegate { button.Text = $"{count++} clicks!"; };

			var intent = new Intent(this, typeof(AccountKitActivity));
			var configurationBuilder =
				new AccountKitConfiguration.AccountKitConfigurationBuilder(
						LoginType.Phone,
						AccountKitActivity.ResponseType.Code);

			intent.PutExtra(
						AccountKitActivity.AccountKitActivityConfiguration,
						configurationBuilder.Build());

			StartActivityForResult(intent, APP_REQUEST_CODE);

		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
		{
			if (requestCode != APP_REQUEST_CODE)
			{
				base.OnActivityResult(requestCode, resultCode, data);

				//Finish();
				return;
			}

			if (resultCode != Result.Ok)
			{
				//Completed?.Invoke(this, null);

				//Finish();
				return;
			}

			var xdata = (AccountKitLoginResult)data.GetParcelableExtra(AccountKitLoginResult.ResultKey);

			//Completed?.Invoke(this, (AccountKitLoginResult)data.GetParcelableExtra(AccountKitLoginResult.ResultKey));

			//Finish();
		}
	}
}

