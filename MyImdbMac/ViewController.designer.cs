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
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButton CkeckOnlineMoviePage { get; set; }

		[Outlet]
		AppKit.NSButton DeleteMovieButton { get; set; }

		[Outlet]
		AppKit.NSTextField DisplayCountry { get; set; }

		[Outlet]
		AppKit.NSTextField DisplayImdbRating { get; set; }

		[Outlet]
		AppKit.NSComboBox DisplayTitle { get; set; }

		[Outlet]
		AppKit.NSTextField DisplayTypes { get; set; }

		[Outlet]
		AppKit.NSTextField DisplayUserRating { get; set; }

		[Outlet]
		AppKit.NSTextField DisplayYear { get; set; }

		[Outlet]
		AppKit.NSButton FormAddMovieButton { get; set; }

		[Outlet]
		AppKit.NSTextField FormMovieName { get; set; }

		[Outlet]
		AppKit.NSTextField FormMovieYear { get; set; }

		[Outlet]
		AppKit.NSLevelIndicator LevelIndicatorImdb { get; set; }

		[Outlet]
		AppKit.NSLevelIndicator LevelIndicatorUser { get; set; }

		[Outlet]
		AppKit.NSImageView LoadingGif { get; set; }

		[Outlet]
		AppKit.NSImageView LogoView { get; set; }

		[Outlet]
		AppKit.NSButton MostRatedImdbMovieButton { get; set; }

		[Outlet]
		AppKit.NSImageView MoviePoster { get; set; }

		[Outlet]
		AppKit.NSTextField NumberImdbRatings { get; set; }

		[Outlet]
		AppKit.NSButton ProfileButton { get; set; }

		[Outlet]
		AppKit.NSTextField RateMovieMessage { get; set; }

		[Outlet]
		AppKit.NSSegmentedControl RateMovieScale { get; set; }

		[Outlet]
		AppKit.NSButton RecommendationButton { get; set; }

		[Outlet]
		AppKit.NSButton SignOutButton { get; set; }

		[Outlet]
		AppKit.NSButton StatisticsButton { get; set; }

		[Outlet]
		AppKit.NSButton UpdateAllButton { get; set; }

		[Outlet]
		AppKit.NSButton UpdateMovieButton { get; set; }

		[Outlet]
		AppKit.NSTextField UserInfo { get; set; }

		[Outlet]
		AppKit.NSTextField WelcomeTextField { get; set; }

		[Action ("DeleteMovieButton_Clicked:")]
		partial void DeleteMovieButton_Clicked (Foundation.NSObject sender);

		[Action ("DisplayTitle_Changed:")]
		partial void DisplayTitle_Changed (Foundation.NSObject sender);

		[Action ("FormAddMovieButton_Clicked:")]
		partial void FormAddMovieButton_Clicked (Foundation.NSObject sender);

		[Action ("MostRatedImdbMovieButton_Clicked:")]
		partial void MostRatedImdbMovieButton_Clicked (Foundation.NSObject sender);

		[Action ("ProfileButton_Clicked:")]
		partial void ProfileButton_Clicked (Foundation.NSObject sender);

		[Action ("RateMovieScale_Changed:")]
		partial void RateMovieScale_Changed (AppKit.NSSegmentedControl sender);

		[Action ("RecommendationButton_Clicked:")]
		partial void RecommendationButton_Clicked (Foundation.NSObject sender);

		[Action ("SignOutButton_Clicked:")]
		partial void SignOutButton_Clicked (Foundation.NSObject sender);

		[Action ("StatisticsButton_Clicked:")]
		partial void StatisticsButton_Clicked (Foundation.NSObject sender);

		[Action ("SwitchUserButton_Clicked:")]
		partial void SwitchUserButton_Clicked (Foundation.NSObject sender);

		[Action ("UpdateAllButton_Clicked:")]
		partial void UpdateAllButton_Clicked (Foundation.NSObject sender);

		[Action ("UpdateMovieButton_Clicked:")]
		partial void UpdateMovieButton_Clicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CkeckOnlineMoviePage != null) {
				CkeckOnlineMoviePage.Dispose ();
				CkeckOnlineMoviePage = null;
			}

			if (DeleteMovieButton != null) {
				DeleteMovieButton.Dispose ();
				DeleteMovieButton = null;
			}

			if (DisplayCountry != null) {
				DisplayCountry.Dispose ();
				DisplayCountry = null;
			}

			if (DisplayImdbRating != null) {
				DisplayImdbRating.Dispose ();
				DisplayImdbRating = null;
			}

			if (DisplayTitle != null) {
				DisplayTitle.Dispose ();
				DisplayTitle = null;
			}

			if (DisplayTypes != null) {
				DisplayTypes.Dispose ();
				DisplayTypes = null;
			}

			if (DisplayUserRating != null) {
				DisplayUserRating.Dispose ();
				DisplayUserRating = null;
			}

			if (DisplayYear != null) {
				DisplayYear.Dispose ();
				DisplayYear = null;
			}

			if (FormAddMovieButton != null) {
				FormAddMovieButton.Dispose ();
				FormAddMovieButton = null;
			}

			if (FormMovieName != null) {
				FormMovieName.Dispose ();
				FormMovieName = null;
			}

			if (FormMovieYear != null) {
				FormMovieYear.Dispose ();
				FormMovieYear = null;
			}

			if (LevelIndicatorImdb != null) {
				LevelIndicatorImdb.Dispose ();
				LevelIndicatorImdb = null;
			}

			if (LevelIndicatorUser != null) {
				LevelIndicatorUser.Dispose ();
				LevelIndicatorUser = null;
			}

			if (LoadingGif != null) {
				LoadingGif.Dispose ();
				LoadingGif = null;
			}

			if (LogoView != null) {
				LogoView.Dispose ();
				LogoView = null;
			}

			if (MostRatedImdbMovieButton != null) {
				MostRatedImdbMovieButton.Dispose ();
				MostRatedImdbMovieButton = null;
			}

			if (MoviePoster != null) {
				MoviePoster.Dispose ();
				MoviePoster = null;
			}

			if (NumberImdbRatings != null) {
				NumberImdbRatings.Dispose ();
				NumberImdbRatings = null;
			}

			if (ProfileButton != null) {
				ProfileButton.Dispose ();
				ProfileButton = null;
			}

			if (RateMovieMessage != null) {
				RateMovieMessage.Dispose ();
				RateMovieMessage = null;
			}

			if (RateMovieScale != null) {
				RateMovieScale.Dispose ();
				RateMovieScale = null;
			}

			if (SignOutButton != null) {
				SignOutButton.Dispose ();
				SignOutButton = null;
			}

			if (StatisticsButton != null) {
				StatisticsButton.Dispose ();
				StatisticsButton = null;
			}

			if (UpdateAllButton != null) {
				UpdateAllButton.Dispose ();
				UpdateAllButton = null;
			}

			if (UpdateMovieButton != null) {
				UpdateMovieButton.Dispose ();
				UpdateMovieButton = null;
			}

			if (RecommendationButton != null) {
				RecommendationButton.Dispose ();
				RecommendationButton = null;
			}

			if (UserInfo != null) {
				UserInfo.Dispose ();
				UserInfo = null;
			}

			if (WelcomeTextField != null) {
				WelcomeTextField.Dispose ();
				WelcomeTextField = null;
			}
		}
	}
}
