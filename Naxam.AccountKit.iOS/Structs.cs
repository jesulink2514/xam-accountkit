using System;
using ObjCRuntime;

namespace AccountKit
{
	[Native]
	public enum AKFResponseType : ulong
	{
		AccessToken = 0,
		AuthorizationCode
	}

	[Native]
	public enum AKFButtonType : ulong
	{
		Default = 0,
		Begin,
		Confirm,
		Continue,
		LogIn,
		Next,
		Ok,
		Send,
		Start,
		Submit
	}

	[Native]
	public enum AKFLoginFlowState : ulong
	{
		None = 0,
		PhoneNumberInput,
		EmailInput,
		EmailVerify,
		SendingCode,
		SentCode,
		CodeInput,
		VerifyingCode,
		Verified,
		Error,
		ResendCode,
		CountryCode
	}

	[Native]
	public enum AKFTextPosition : ulong
	{
		Default = 0,
		AboveBody,
		BelowBody
	}

	[Native]
	public enum AKFLoginType : ulong
	{
		Email = 0,
		Phone
	}

	[Native]
	public enum AKFErrorCode : ulong
	{
		NetworkConnectionError = 100,
		ServerError = 200,
		LoginRequestInvalidatedError = 300,
		InvalidParameterValueError = 400
	}

	[Native]
	public enum AKFServerErrorCode : ulong
	{
		AKFInvalidServerParameterValueError = 201
	}

	[Native]
	public enum AKFLoginRequestInvalidatedErrorCode : ulong
	{
		AKFLoginRequestExpiredError = 301
	}

	[Native]
	public enum AKFInvalidParameterValueErrorCode : ulong
	{
		InvalidEmailAddressError = 401,
		InvalidPhoneNumberError = 402,
		InvalidCodingValueError = 403,
		InvalidAccessTokenError = 404,
		InvalidAccountPreferenceKeyError = 405,
		InvalidAccountPreferenceValueError = 406,
		OperationNotSuccessful = 407
	}

	[Native]
	public enum AKFServerResponseErrorCode : ulong
	{
		AKFServerResponseErrorCodeInvalidConfirmationCode = 15003
	}

	[Native]
	public enum AKFHeaderTextType : ulong
	{
		Login = 0,
		AppName
	}
}
