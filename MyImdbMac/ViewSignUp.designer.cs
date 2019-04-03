// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ImdbMacApp
{
	[Register ("ViewSignUp")]
	partial class ViewSignUp
	{
		[Outlet]
		AppKit.NSComboBox InputCountry { get; set; }

		[Outlet]
		AppKit.NSTextField InputEmail { get; set; }

		[Outlet]
		AppKit.NSTextField InputFirstName { get; set; }

		[Outlet]
		AppKit.NSTextField InputLastName { get; set; }

		[Outlet]
		AppKit.NSSecureTextField InputPassword1 { get; set; }

		[Outlet]
		AppKit.NSSecureTextField InputPassword2 { get; set; }

		[Outlet]
		AppKit.NSImageView LogoView { get; set; }

		[Action ("SignInButton_Clicked:")]
		partial void SignInButton_Clicked (Foundation.NSObject sender);

		[Action ("SignUpButton_Clicked:")]
		partial void SignUpButton_Clicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (InputCountry != null) {
				InputCountry.Dispose ();
				InputCountry = null;
			}

			if (InputEmail != null) {
				InputEmail.Dispose ();
				InputEmail = null;
			}

			if (InputFirstName != null) {
				InputFirstName.Dispose ();
				InputFirstName = null;
			}

			if (InputLastName != null) {
				InputLastName.Dispose ();
				InputLastName = null;
			}

			if (InputPassword1 != null) {
				InputPassword1.Dispose ();
				InputPassword1 = null;
			}

			if (InputPassword2 != null) {
				InputPassword2.Dispose ();
				InputPassword2 = null;
			}

			if (LogoView != null) {
				LogoView.Dispose ();
				LogoView = null;
			}
		}
	}
}
