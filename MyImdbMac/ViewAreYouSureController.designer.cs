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
	[Register ("ViewAreYouSureController")]
	partial class ViewAreYouSureController
	{
		[Outlet]
		AppKit.NSButton YesButton { get; set; }

		[Action ("NoButton_Clicked:")]
		partial void NoButton_Clicked (Foundation.NSObject sender);

		[Action ("YesButton_Clicked:")]
		partial void YesButton_Clicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (YesButton != null) {
				YesButton.Dispose ();
				YesButton = null;
			}
		}
	}
}
