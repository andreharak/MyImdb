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
	[Register ("ViewRecommendations")]
	partial class ViewRecommendations
	{
		[Outlet]
		AppKit.NSButton CkeckOnlineMoviePage { get; set; }

		[Outlet]
		AppKit.NSButton CloseButton { get; set; }

		[Outlet]
		AppKit.NSImageView LoadingGif { get; set; }

		[Outlet]
		AppKit.NSTextField MatchLevel { get; set; }

		[Outlet]
		AppKit.NSTextField MessageToUser { get; set; }

		[Outlet]
		AppKit.NSTextField MessageToUser2 { get; set; }

		[Outlet]
		AppKit.NSTextField MovieCountries { get; set; }

		[Outlet]
		AppKit.NSTextField MovieGenres { get; set; }

		[Outlet]
		AppKit.NSTextField MovieNbOfRatings { get; set; }

		[Outlet]
		AppKit.NSImageView MoviePoster { get; set; }

		[Outlet]
		AppKit.NSTextField MovieRating { get; set; }

		[Outlet]
		AppKit.NSLevelIndicator MovieRatingBar { get; set; }

		[Outlet]
		AppKit.NSTextField MovieTitle { get; set; }

		[Outlet]
		AppKit.NSTextField MovieYear { get; set; }

		[Outlet]
		AppKit.NSButton NoButton { get; set; }

		[Outlet]
		AppKit.NSButton StoryButton { get; set; }

		[Outlet]
		AppKit.NSButton YesButton { get; set; }

		[Outlet]
		AppKit.NSTextField YesNoMessage { get; set; }

		[Outlet]
		AppKit.NSButton YouTubeButton { get; set; }

		[Action ("CloseButton_Clicked:")]
		partial void CloseButton_Clicked (Foundation.NSObject sender);

		[Action ("NoButton_Clicked:")]
		partial void NoButton_Clicked (Foundation.NSObject sender);

		[Action ("StoryButton_Clicked:")]
		partial void StoryButton_Clicked (Foundation.NSObject sender);

		[Action ("YesButton_Clicked:")]
		partial void YesButton_Clicked (Foundation.NSObject sender);
		
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

			if (LoadingGif != null) {
				LoadingGif.Dispose ();
				LoadingGif = null;
			}

			if (MatchLevel != null) {
				MatchLevel.Dispose ();
				MatchLevel = null;
			}

			if (MessageToUser != null) {
				MessageToUser.Dispose ();
				MessageToUser = null;
			}

			if (MessageToUser2 != null) {
				MessageToUser2.Dispose ();
				MessageToUser2 = null;
			}

			if (MovieCountries != null) {
				MovieCountries.Dispose ();
				MovieCountries = null;
			}

			if (MovieGenres != null) {
				MovieGenres.Dispose ();
				MovieGenres = null;
			}

			if (MovieNbOfRatings != null) {
				MovieNbOfRatings.Dispose ();
				MovieNbOfRatings = null;
			}

			if (MoviePoster != null) {
				MoviePoster.Dispose ();
				MoviePoster = null;
			}

			if (MovieRating != null) {
				MovieRating.Dispose ();
				MovieRating = null;
			}

			if (MovieRatingBar != null) {
				MovieRatingBar.Dispose ();
				MovieRatingBar = null;
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

			if (StoryButton != null) {
				StoryButton.Dispose ();
				StoryButton = null;
			}

			if (YesButton != null) {
				YesButton.Dispose ();
				YesButton = null;
			}

			if (YesNoMessage != null) {
				YesNoMessage.Dispose ();
				YesNoMessage = null;
			}

			if (YouTubeButton != null) {
				YouTubeButton.Dispose ();
				YouTubeButton = null;
			}
		}
	}
}
