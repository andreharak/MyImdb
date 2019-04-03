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
	[Register ("ViewProfile")]
	partial class ViewProfile
	{
		[Outlet]
		AppKit.NSButton ConfirmButton { get; set; }

		[Outlet]
		AppKit.NSTextField InputApiKey { get; set; }

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

		[Action ("CancelButton_Clicked:")]
		partial void CancelButton_Clicked (Foundation.NSObject sender);

		[Action ("ConfirmButton_Clicked:")]
		partial void ConfirmButton_Clicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ConfirmButton != null) {
				ConfirmButton.Dispose ();
				ConfirmButton = null;
			}

			if (InputApiKey != null) {
				InputApiKey.Dispose ();
				InputApiKey = null;
			}

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
		}
	}
}
