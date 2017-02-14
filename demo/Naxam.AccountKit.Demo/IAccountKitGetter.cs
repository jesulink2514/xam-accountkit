using System;
namespace Naxam.AccountKit.Demo
{
	public interface IAccountKitGetter
	{
		IAccountKitAuth AccountKitAuth { get; }
	}
}
