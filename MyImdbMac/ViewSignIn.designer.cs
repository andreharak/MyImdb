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
	[Register ("ViewSignIn")]
	partial class ViewSignIn
	{
		[Outlet]
		AppKit.NSImageView BackgroundImage { get; set; }

		[Outlet]
		AppKit.NSTextField InputEmail { get; set; }

		[Outlet]
		AppKit.NSSecureTextField InputPassword { get; set; }

		[Outlet]
		AppKit.NSImageView LogoView { get; set; }

		[Outlet]
		AppKit.NSButton SignUpButton { get; set; }

		[Action ("SignInButton_Clicked:")]
		partial void SignInButton_Clicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (InputEmail != null) {
				InputEmail.Dispose ();
				InputEmail = null;
			}

			if (InputPassword != null) {
				InputPassword.Dispose ();
				InputPassword = null;
			}

			if (LogoView != null) {
				LogoView.Dispose ();
				LogoView = null;
			}

			if (SignUpButton != null) {
				SignUpButton.Dispose ();
				SignUpButton = null;
			}

			if (BackgroundImage != null) {
				BackgroundImage.Dispose ();
				BackgroundImage = null;
			}
		}
	}
}
