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
	[Register ("ViewOmdbApi")]
	partial class ViewOmdbApi
	{
		[Outlet]
		AppKit.NSButton CkeckOnlineMoviePage { get; set; }

		[Outlet]
		AppKit.NSButton CloseButton { get; set; }

		[Outlet]
		AppKit.NSTextField MessageToUser { get; set; }

		[Outlet]
		AppKit.NSTextField MessageToUser2 { get; set; }

		[Outlet]
		AppKit.NSImageView MoviePoster { get; set; }

		[Outlet]
		AppKit.NSTextField MovieTitle { get; set; }

		[Outlet]
		AppKit.NSTextField MovieYear { get; set; }

		[Outlet]
		AppKit.NSButton NoButton { get; set; }

		[Outlet]
		AppKit.NSButton YesButton { get; set; }

		[Outlet]
		AppKit.NSTextField YesNoMessage { get; set; }

		[Action ("CloseButton_Clicked:")]
		partial void CloseButton_Clicked (Foundation.NSObject sender);

		[Action ("NoButton_Clicked:")]
		partial void NoButton_Clicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CkeckOnlineMoviePage != null) {
				CkeckOnlineMoviePage.Dispose ();
				CkeckOnlineMoviePage = null;
			}

			if (CloseButton != null) {
				CloseButton.Dispose ();
				CloseButton = null;
			}

			if (MessageToUser != null) {
				MessageToUser.Dispose ();
				MessageToUser = null;
			}

			if (MessageToUser2 != null) {
				MessageToUser2.Dispose ();
				MessageToUser2 = null;
			}

			if (MoviePoster != null) {
				MoviePoster.Dispose ();
				MoviePoster = null;
			}

			if (MovieTitle != null) {
				MovieTitle.Dispose ();
				MovieTitle = null;
			}

			if (MovieYear != null) {
				MovieYear.Dispose ();
				MovieYear = null;
			}

			if (NoButton != null) {
				NoButton.Dispose ();
				NoButton = null;
			}

			if (YesButton != null) {
				YesButton.Dispose ();
				YesButton = null;
			}

			if (YesNoMessage != null) {
				YesNoMessage.Dispose ();
				YesNoMessage = null;
			}
		}
	}
}
