using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ToRunOr.Vws
{
	public sealed partial class PrivacyPolicy : Page
	{
		public PrivacyPolicy()
		{
			this.InitializeComponent();
		}
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var rootFrame = Window.Current.Content as Frame;
			if (rootFrame.CanGoBack)
			{
				SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
			}
		}

		void BackButton_Click(object sender, RoutedEventArgs e) { if (this.Frame.CanGoBack) this.Frame.GoBack(); }
	}
}
