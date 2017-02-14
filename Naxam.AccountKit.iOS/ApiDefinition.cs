using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;

namespace AccountKit
{
	// The first step to creating a binding is to add your native library ("libNativeLibrary.a")
	// to the project by right-clicking (or Control-clicking) the folder containing this source
	// file and clicking "Add files..." and then simply select the native library (or libraries)
	// that you want to bind.
	//
	// When you do that, you'll notice that MonoDevelop generates a code-behind file for each
	// native library which will contain a [LinkWith] attribute. MonoDevelop auto-detects the
	// architectures that the native library supports and fills in that information for you,
	// however, it cannot auto-detect any Frameworks or other system libraries that the
	// native library may depend on, so you'll need to fill in that information yourself.
	//
	// Once you've done that, you're ready to move on to binding the API...
	//
	//
	// Here is where you'd define your API definition for the native Objective-C library.
	//
	// For example, to bind the following Objective-C class:
	//
	//     @interface Widget : NSObject {
	//     }
	//
	// The C# binding would look like this:
	//
	//     [BaseType (typeof (NSObject))]
	//     interface Widget {
	//     }
	//
	// To bind Objective-C properties, such as:
	//
	//     @property (nonatomic, readwrite, assign) CGPoint center;
	//
	// You would add a property definition in the C# interface like so:
	//
	//     [Export ("center")]
	//     CGPoint Center { get; set; }
	//
	// To bind an Objective-C method, such as:
	//
	//     -(void) doSomething:(NSObject *)object atIndex:(NSInteger)index;
	//
	// You would add a method definition to the C# interface like so:
	//
	//     [Export ("doSomething:atIndex:")]
	//     void DoSomething (NSObject object, int index);
	//
	// Objective-C "constructors" such as:
	//
	//     -(id)initWithElmo:(ElmoMuppet *)elmo;
	//
	// Can be bound as:
	//
	//     [Export ("initWithElmo:")]
	//     IntPtr Constructor (ElmoMuppet elmo);
	//
	// For more information, see http://developer.xamarin.com/guides/ios/advanced_topics/binding_objective-c/
	//

	[BaseType(typeof(NSObject))]
	[Model, Protocol]
	partial interface AKFAccessToken : INSCopying, INSSecureCoding { 
		[Export("accountID"), Abstract]
		string AccountId { get; }

		[Export("applicationID"), Abstract]
		string ApplicationId { get; }

		[Export("lastRefresh"), Abstract]
		NSDate LastRefresh { get; }

		[Export("tokenRefreshInterval"), Abstract]
		nfloat TokenRefreshInterval { get; }

		[Export("tokenString"), Abstract]
		string TokenString { get; }
	}

	[BaseType(typeof(NSObject))]
	[Model, Protocol]
	partial interface AKFAccount : INSCopying, INSSecureCoding
	{
		[Export("accountID"), Abstract]
		string AccountId { get; }

		[Export("emailAddress"), Abstract]
		string EmailAddress { get; }

		[Export("phoneNumber"), Abstract]
		AKFPhoneNumber PhoneNumber { get; }
	}

	//typedef void (^AKFRequestAccountHandler)(id<AKFAccount> _Nullable account, NSError *_Nullable error);
	delegate void AKFRequestAccountHandler(IAKFAccount account, NSError error);

	[BaseType(typeof(NSObject))]
	partial interface AKFAccountKit { 
		[Static, Export("graphVersionString")]
		string GraphVersionString { get; }

		[Static, Export("versionString")]
		string VersionString { get; }

		[Export("currentAccessToken")]
		IAKFAccessToken CurrentAccessToken { get; }

		//- (instancetype)initWithResponseType:(AKFResponseType)responseType NS_DESIGNATED_INITIALIZER;
		[Export("initWithResponseType:")]
		IntPtr Constructor(AKFResponseType responseType);

		/**
		  Returns an instance of account preferences for the current access token.
		 */
		//- (nullable AKFAccountPreferences *)accountPreferences;
		[Export("accountPreferences")]
		AKFAccountPreferences AccountPreferences { get; }

		/**
		  Cancels the pending login request.
		 */
		//- (void)cancelLogin;
		[Export("cancelLogin")]
		void CancelLogin();

		/**
		  Logs out currently logged in account.
		 */
		//- (void)logOut;
		[Export("logOut")]
		void LogOut();

		/**
		  Asynchronously returns Account Kit account information
		 */
		//- (void)requestAccount:(AKFRequestAccountHandler)handler;
		[Export("requestAccount:")]
		void RequestAccount(AKFRequestAccountHandler handler);

		/**
		  Returns a view controller to be presented to initiate an email login.
		 */
		//- (UIViewController<AKFViewController>*)viewControllerForEmailLogin;
		[Export("viewControllerForEmailLogin")]
		UIViewController GetViewControllerForEmailLogin();

		/**
		  Returns a view controller to be presented to initiate an email login.

		 - Parameter email: the email to be used for login.
		 - Parameter state: the state for the login request.
		 */
		//- (UIViewController<AKFViewController>*)viewControllerForEmailLoginWithEmail:(nullable NSString *)email
		//																	   state:(nullable NSString *)state;
		[Export("viewControllerForEmailLoginWithEmail:state:")]
		UIViewController GetViewControllerForEmailLogin(string email, string state);

		/**
		  Returns a view controller to be presented to initiate a phone login.
		 */
		//- (UIViewController<AKFViewController>*)viewControllerForPhoneLogin;
		[Export("viewControllerForPhoneLogin")]
		UIViewController GetViewControllerForPhoneLogin();

		/**
		  Returns a view controller to be presented to initiate a phone login.

		 - Parameter phoneNumber: the phone number to be used for login.
		 - Parameter state: the state for the login request.
		 */
		//- (UIViewController<AKFViewController>*)viewControllerForPhoneLoginWithPhoneNumber:(nullable AKFPhoneNumber *)phoneNumber
		//																			 state:(nullable NSString *)state;
		[Export("viewControllerForPhoneLoginWithPhoneNumber:state:")]
		UIViewController GetViewControllerForPhoneLogin(string phoneNumber, string state);

		/**
		  Returns a view controller to resume a login that was pending when the app shutdown.
		 */
		//- (nullable UIViewController<AKFViewController> *)viewControllerForLoginResume;
		[Export("viewControllerForLoginResume")]
		UIViewController GetViewControllerForLoginResume();
	}

	[BaseType(typeof(NSObject))]
	partial interface AKFAccountPreferences {
		//@property (nonatomic, weak) id<AKFAccountPreferencesDelegate> delegate;
		[Export("delagate")]
		IAKFAccountPreferencesDelegate Delegate { get; }

		//- (void)deletePreferenceForKey:(NSString *)key;
		[Export("deletePreferenceForKey:")]
		void DeletePreferenceForKey(string key);

		//- (void)loadPreferenceForKey:(NSString *)key;
		[Export("loadPreferenceForKey:")]
		void LoadPreferenceForKey(string key);

		//- (void)loadPreferences;
		[Export("loadPreferences")]
		void LoadPreferences();

		//- (void)setPreferenceForKey:(NSString *)key value:(nullable NSString *)value;
		[Export("setPreferenceForKey:value:")]
		void SetPreferenceForKey(string key, string value);
	}

	[BaseType(typeof(NSObject))]
	[Model, Protocol]
	partial interface AKFAccountPreferencesDelegate {
		//- (void)accountPreferences:(AKFAccountPreferences *)accountPreferences
		//didDeletePreferenceForKey:(NSString *)key
		//                  error:(nullable NSError *)error;
		[Export("accountPreferences:didDeletePreferenceForKey:error:")]
		void DidDeletePreference(AKFAccountPreferences accountPreferences, string key, NSError error);

		//- (void)accountPreferences:(AKFAccountPreferences *)accountPreferences
		//didLoadPreferences:(nullable NSDictionary<NSString *, NSString *> *)preferences
		//			 error:(nullable NSError *)error;
		[Export("accountPreferences:didLoadPreferences:error:")]
		void DidLoadPreferences(AKFAccountPreferences accountPreferences, NSDictionary<NSString, NSString> preferences, NSError error);

		//- (void)accountPreferences:(AKFAccountPreferences *)accountPreferences
		//didLoadPreferenceForKey:(NSString*)key
		//				 value:(nullable NSString *)value
		//				 error:(nullable NSError *)error;
		[Export("accountPreferences:didLoadPreferenceForKey:value:error:")]
		void DidLoadPreferenceForKey(AKFAccountPreferences accountPreferences, string key, string value, NSError error);

		//- (void)accountPreferences:(AKFAccountPreferences *)accountPreferences
		//didSetPreferenceForKey:(NSString*)key
		//				value:(NSString*)value
		//			   error:(nullable NSError *)error;
		[Export("accountPreferences:didSetPreferenceForKey:value:error:")]
		void DidSetPreferenceForKey(AKFAccountPreferences accountPreferences, string key, string value, NSError error);
	}

	[BaseType(typeof(NSObject))]
	[Model, Protocol]
	partial interface AKFAdvancedUIActionController {
		//- (void)back;
		[Export("back"), Abstract]
		void Back();

		//- (void)cancel;
		[Export("cancel"), Abstract]
		void Cancel();
	}

	[BaseType(typeof(NSObject))]
	[Model, Protocol]
	partial interface AKFAdvancedUIManager {
		//- (nullable UIView *)actionBarViewForState:(AKFLoginFlowState)state;
		[Export("actionBarViewForState:")]
		UIView GetActionBarForState(AKFLoginFlowState state);

		//- (nullable UIView *)bodyViewForState:(AKFLoginFlowState)state;
		[Export("bodyViewForState:")]
		UIView GetBodyViewForState(AKFLoginFlowState state);

		//- (AKFButtonType)buttonTypeForState:(AKFLoginFlowState)state;
		[Export("buttonTypeForState:")]
		AKFButtonType GetButtonTypeForState(AKFLoginFlowState state);

		//- (nullable UIView *)footerViewForState:(AKFLoginFlowState)state;
		[Export("footerViewForState:")]
		UIView GetFooterViewForState(AKFLoginFlowState state);

		//- (nullable UIView *)headerViewForState:(AKFLoginFlowState)state;
		[Export("headerViewForState:")]
		UIView GetHeaderViewForState(AKFLoginFlowState state);

		//- (void)setActionController:(nonnull id<AKFAdvancedUIActionController>)actionController;
		[Export("setActionController:")]
		void SetActionController(IAKFAdvancedUIActionController actionController);

		//void - (void)setError:(nonnull NSError *)error;
		[Export("setError:")]
		void SetError(NSError error);

		//- (AKFTextPosition)textPositionForState:(AKFLoginFlowState)state;
		[Export("textPositionForState:")]
		AKFTextPosition GetTextPositionForState(AKFLoginFlowState state);
	}

	[BaseType(typeof(NSObject))]
	[Model, Protocol]
	partial interface AKFAdvancedUIManaging { 
		//@property (nonatomic, strong) id<AKFAdvancedUIManager> advancedUIManager;
		[Export("advancedUIManager"), Abstract]
		IAKFAdvancedUIManager AdvancedUIManager { get; set; }
	}

	[BaseType(typeof(NSObject))]
	[Model, Protocol]
	partial interface AKFConfiguring { 
		//@property (nonatomic, copy) NSArray<NSString *> *blacklistedCountryCodes;
		[Export("blacklistedCountryCodes"), Abstract]
		string[] BlacklistedCountryCodes { get; set; }

		//@property (nonatomic, copy) NSString *defaultCountryCode;
		[Export("defaultCountryCode"), Abstract]
		string DefaultCountryCode { get; set; }

		//@property (nonatomic) BOOL enableSendToFacebook;
		[Export("enableSendToFacebook"), Abstract]
		bool EnableSendToFacebook { get; set; }

		//@property (nonatomic, copy) NSArray<NSString *> *whitelistedCountryCodes;
		[Export("whitelistedCountryCodes"), Abstract]
		string[] WhitelistedCountryCodes { get; set; }
	}

	[Static]
	partial interface AKFError { 
		[Field("AKFErrorDomain", "__Internal")]
		NSString AKFErrorDomain { get; }

		[Field("AKFErrorDeveloperMessageKey", "__Internal")]
		NSString AKFErrorDeveloperMessageKey { get; }

		[Field("AKFErrorUserMessageKey", "__Internal")]
		NSString AKFErrorUserMessageKey { get; }

		[Field("AKFErrorObjectKey", "__Internal")]
		NSString AKFErrorObjectKey { get; }
	}

	[Static]
	partial interface AKFErrorDetail { 
		[Field("AKFServerErrorDomain", "__Internal")]
		NSString AKFServerErrorDomain { get; }
	}

	[Static]
	partial interface AKFLoginFlowStateConstants { 
		[Field("AKFLoginFlowStateCount", "__Internal")]
		nuint AKFLoginFlowStateCount { get; }
	}

	[BaseType(typeof(NSObject))]
	partial interface AKFPhoneNumber :INSCopying, INSSecureCoding {
		//- (instancetype)initWithCountryCode:(NSString *)countryCode
		//                   phoneNumber:(NSString *)phoneNumber
		//NS_DESIGNATED_INITIALIZER;
		[Export("initWithCountryCode:phoneNumber:")]
		IntPtr Constructor(string countryCode, string phoneNumber);

		//- (instancetype)initWithCountryCode:(NSString *)countryCode
		//							countryISO:(NSString*)iso
	  	//							phoneNumber:(NSString*)phoneNumber;
		[Export("initWithCountryCode:countryISO:phoneNumber:")]
		IntPtr Constructor(string countryCode, string countryISO, string phoneNumber);

		//@property (nonatomic, copy, readonly) NSString *countryCode;
		[Export("countryCode")]
		string CountryCode { get; }

		[Export("countryISO")]
		string CountryISO { get; }

		[Export("phoneNumber")]
		string PhoneNumber { get; }

		//- (BOOL)isEqualToPhoneNumber:(AKFPhoneNumber *)phoneNumber;
		[Export("isEqualToPhoneNumber:")]
		bool isEqualToPhoneNumber(AKFPhoneNumber phoneNumber);

		[Export("stringRepresentation")]
		string stringRepresentation();
	}

	[BaseType(typeof(NSObject))]
	partial interface AKFSettings {
		//+ (NSString*)clientToken;
		[Export("clientToken"), Static]
		string GetClientToken();

		//+ (NSString*)clientToken;
		[Export("setClientToken:"), Static]
		void SetClientToken(string clientToken);
	}

	[BaseType(typeof(NSObject))]
	partial interface AKFTheme : INSCopying{ 
		//+ (instancetype)defaultTheme;
		[Export("defaultTheme"), Static]
		AKFTheme DefaultTheme { get; }

		//+ (instancetype)defaultTheme;
		[Export("outlineTheme"), Static]
		AKFTheme OutlineTheme { get; }

		//+ (instancetype)outlineThemeWithPrimaryColor:(UIColor *)primaryColor
		// 								primaryTextColor:(UIColor*)primaryTextColor
		//								secondaryTextColor:(UIColor*)secondaryTextColor
		//								statusBarStyle:(UIStatusBarStyle)statusBarStyle;
		[Export("outlineThemeWithPrimaryColor:primaryTextColor:secondaryTextColor:statusBarStyle:"), Static]
		AKFTheme CreateOutlineTheme(UIColor primaryColor, UIColor primaryTextColor, UIColor secondaryTextColor, UIStatusBarStyle statusBarStyle);

		//+ (instancetype)themeWithPrimaryColor:(UIColor *)primaryColor
		//						primaryTextColor:(UIColor*)primaryTextColor
		//					 	secondaryColor:(UIColor*)secondaryColor
		//						secondaryTextColor:(UIColor*)secondaryTextColor
	   	//						statusBarStyle:(UIStatusBarStyle)statusBarStyle;
		[Export("themeWithPrimaryColor:primaryTextColor:secondaryColor:secondaryTextColor:statusBarStyle:"), Static]
		AKFTheme CreateTheme(UIColor primaryColor, UIColor primaryTextColor, UIColor secondaryColor, UIColor secondaryTextColor, UIStatusBarStyle statusBarStyle);


		//@property(nonatomic, copy) UIColor* backgroundColor;
		[Export("backgroundColor")]
		UIColor BackgroundColor { get; set; }

		//@property(nullable, nonatomic, copy) UIImage* backgroundImage;
		[Export("backgroundImage")]
		UIImage BackgroundImage { get; set; }

		//@property(nonatomic, copy) UIColor* buttonBackgroundColor;
		[Export("buttonBackgroundColor")]
		UIColor ButtonBackgroundColor { get; set; }

		//@property(nonatomic, copy) UIColor* buttonBorderColor;
		[Export("buttonBorderColor")]
		UIColor ButtonBorderColor { get; set; }

		//@property(nonatomic, copy) UIColor* buttonDisabledBackgroundColor;
		[Export("buttonDisabledBackgroundColor")]
		UIColor ButtonDisabledBackgroundColor { get; set; }

		//@property(nonatomic, copy) UIColor* buttonDisabledBorderColor;
		[Export("buttonDisabledBorderColor")]
		UIColor ButtonDisabledBorderColor { get; set; }

		//@property(nonatomic, copy) UIColor* buttonDisabledTextColor;
		[Export("buttonDisabledTextColor")]
		UIColor ButtonDisabledTextColor { get; set; }

		//@property(nonatomic, copy) UIColor* buttonHighlightedBackgroundColor;
		[Export("buttonHighlightedBackgroundColor")]
		UIColor ButtonHighlightedBackgroundColor { get; set; }

		//@property(nonatomic, copy) UIColor* buttonHighlightedBorderColor;
		[Export("buttonHighlightedBorderColor")]
		UIColor ButtonHighlightedBorderColor { get; set; }

		//@property(nonatomic, copy) UIColor* buttonHighlightedTextColor;
		[Export("buttonHighlightedTextColor")]
		UIColor ButtonHighlightedTextColor { get; set; }

		//@property(nonatomic, copy) UIColor* buttonTextColor;
		[Export("buttonTextColor")]
		UIColor ButtonTextColor { get; set; }

		//@property(nonatomic, assign) NSUInteger contentBodyLayoutWeight;
		[Export("contentBodyLayoutWeight")]
		nuint ContentBodyLayoutWeight { get; set; }

		//@property(nonatomic, assign) NSUInteger contentBottomLayoutWeight;
		[Export("contentBottomLayoutWeight")]
		nuint ContentBottomLayoutWeight { get; set; }

		//@property(nonatomic, assign) NSUInteger contentFooterLayoutWeight;
		[Export("contentFooterLayoutWeight")]
		nuint contentFooterLayoutWeight { get; set; }

		//@property(nonatomic, assign) NSUInteger contentHeaderLayoutWeight;
		[Export("contentHeaderLayoutWeight")]
		nuint ContentHeaderLayoutWeight { get; set; }

		//@property(nonatomic, assign) CGFloat contentMarginLeft;
		[Export("contentMarginLeft")]
		nfloat ContentMarginLeft { get; set; }

		//@property(nonatomic, assign) CGFloat contentMarginRight;
		[Export("contentMarginRight")]
		nfloat ContentMarginRight { get; set; }

		//@property(nonatomic, assign) CGFloat contentMaxWidth;
		[Export("contentMaxWidth")]
		nfloat ContentMaxWidth { get; set; }

		//@property(nonatomic, assign) CGFloat contentMinHeight;
		[Export("contentMinHeight")]
		nfloat ContentMinHeight { get; set; }

		//@property(nonatomic, assign) NSUInteger contentTextLayoutWeight;
		[Export("contentTextLayoutWeight")]
		nuint ContentTextLayoutWeight { get; set; }

		//@property(nonatomic, assign) NSUInteger contentTopLayoutWeight;
		[Export("contentTopLayoutWeight")]
		nuint ContentTopLayoutWeight { get; set; }

		//@property(nonatomic, copy) UIColor* headerBackgroundColor;
		[Export("headerBackgroundColor")]
		UIColor HeaderBackgroundColor { get; set; }

		//@property(nonatomic, strong) UIColor* headerButtonTextColor;
		[Export("headerButtonTextColor")]
		UIColor HeaderButtonTextColor { get; set; }

		//@property(nonatomic, copy) UIColor* headerTextColor;
		[Export("HeaderTextColor")]
		UIColor HeaderTextColor { get; set; }

		//@property(nonatomic, assign) AKFHeaderTextType headerTextType;
		[Export("headerTextType")]
		AKFHeaderTextType HeaderTextType { get; set; }

		//@property(nonatomic, copy) UIColor* iconColor;
		[Export("iconColor")]
		UIColor IconColor { get; set; }

		//@property(nonatomic, copy) UIColor* inputBackgroundColor;
		[Export("inputBackgroundColor")]
		UIColor InputBackgroundColor { get; set; }

		//@property(nonatomic, copy) UIColor* inputBorderColor;
		[Export("inputBorderColor")]
		UIColor InputBorderColor { get; set; }

		//@property(nonatomic, copy) UIColor* inputTextColor;
		[Export("inputTextColor")]
		UIColor InputTextColor { get; set; }

		//@property(nonatomic, assign) UIStatusBarStyle statusBarStyle;
		[Export("statusBarStyle")]
		UIStatusBarStyle StatusBarStyle { get; set; }

		//@property(nonatomic, copy) UIColor* textColor;
		[Export("textColor")]
		UIColor TextColor { get; set; }

		//@property(nonatomic, copy) UIColor* titleColor;
		[Export("titleColor")]
		UIColor TitleColor { get; set; }

		//- (BOOL)isEqualToTheme:(AKFTheme*)theme
		[Export("isEqualToTheme:")]
		bool isEqualToTheme(AKFTheme theme);
	}

	[BaseType(typeof(NSObject))]
	[Model, Protocol]
	partial interface AKFTheming { 
		//@property (null_resettable, nonatomic, copy) AKFTheme *theme UI_APPEARANCE_SELECTOR;
		[Export("theme"), Abstract]
		AKFTheme Theme { get; set; }
	}

	[BaseType(typeof(NSObject))]
	[Model, Protocol]
	partial interface AKFViewController : AKFAdvancedUIManaging, AKFConfiguring, AKFTheming { 
		//@property (nonatomic, weak) id<AKFViewControllerDelegate> delegate;
		[Export("delegate"), Abstract]
		IAKFViewControllerDelegate Delegate { get; set; }

		//@property (nonatomic, assign, readonly) AKFLoginType loginType;
		[Export("loginType"), Abstract]
		AKFLoginType LoginType { get; }
	}

	[BaseType(typeof(UIViewController))]
	partial interface AKFNavigationController : AKFViewController { 
		
	}

	[BaseType(typeof(NSObject))]
	[Model, Protocol]
	partial interface AKFViewControllerDelegate {
		/**
		  Called when the login completes with an authorization code response type.

		 - Parameter viewController: the AKFViewController that was used
		 - Parameter code: the authorization code that can be exchanged for an access token with the app secret
		 - Parameter state: the state param value that was passed in at the beginning of the flow
		 */
		//- (void)viewController:(UIViewController<AKFViewController>*)viewController didCompleteLoginWithAuthorizationCode:(NSString*)code state:(NSString*)state;
		[Export("viewController:didCompleteLoginWithAuthorizationCode:state:")]
		void DidCompleteLoginWithAuthorizationCode(UIViewController viewController, string code, string state);

		/**
		  Called when the login completes with an access token response type.

		 - Parameter viewController: the AKFViewController that was used
		 - Parameter accessToken: the access token for the logged in account
		 - Parameter state: the state param value that was passed in at the beginning of the flow
		 */
		//- (void)viewController:(UIViewController<AKFViewController>*)viewController didCompleteLoginWithAccessToken:(id<AKFAccessToken>)accessToken state:(NSString*)state;
		[Export("viewController:didCompleteLoginWithAccessToken:state:")]
		void DidCompleteLoginWithAccessToken(UIViewController viewController, IAKFAccessToken accessToken, string state);

		/**
		  Called when the login failes with an error

		 - Parameter viewController: the AKFViewController that was used
		 - Parameter error: the error that occurred
		 */
		//- (void)viewController:(UIViewController<AKFViewController>*)viewController didFailWithError:(NSError*)error;
		[Export("viewController:didFailWithError:")]
		void DidFailWithError(UIViewController viewController, NSError error);

		/**
		  Called when the login flow is cancelled through the UI.

		 - Parameter viewController: the AKFViewController that was used
		 */
		//- (void)viewControllerDidCancel:(UIViewController<AKFViewController>*)viewController;
		[Export("viewControllerDidCancel:")]
		void DidCancel(UIViewController viewController);
	}
}
