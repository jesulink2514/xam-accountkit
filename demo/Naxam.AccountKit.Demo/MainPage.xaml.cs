using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Naxam.AccountKit.Demo
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

			var akAuth = DependencyService.Get<IAccountKitGetter>().AccountKitAuth;

			var selectedLoginTypeIndex = pkrLoginType.SelectedIndex = 0;
			var selectedResponseTypeIndex = pkrResponseType.SelectedIndex = 0;

			pkrLoginType.SelectedIndexChanged += (sender, e) =>
			{
				selectedLoginTypeIndex = pkrLoginType.SelectedIndex;
			};

			pkrResponseType.SelectedIndexChanged += (sender, e) =>
			{
				selectedResponseTypeIndex = pkrResponseType.SelectedIndex;
			};

			btnLogin.Clicked += async delegate
			{
				var responseType = selectedResponseTypeIndex == 0
												   ? ResponseType.AuthorizationCode
												 : ResponseType.AccessToken;
				
				var result = await akAuth.LoginWithAccountKit(selectedLoginTypeIndex == 0
										   ? LoginType.Phone
										   : LoginType.Email,
											  responseType);

				System.Diagnostics.Debug.WriteLine(result.TokenOrCode);

				lblResult.Text = $"{responseType.ToString()}:{result.TokenOrCode}";
			};
		}
	}
}
