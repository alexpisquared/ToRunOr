using VMs;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ToRunOr.Vws
{
	public sealed partial class DevDbgView : Page
	{
		MainPageVM _vm = null;
		public DevDbgView()
		{
			this.InitializeComponent();

			_vm = MainPageVM.Instance;
			DataContext = _vm;
		}
		protected override void OnNavigatedTo(NavigationEventArgs e) { if (Window.Current.Content is Frame rootFrame && rootFrame.CanGoBack) SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible; }
		void BackButton_Click(object sender, RoutedEventArgs e) { if (this.Frame.CanGoBack) this.Frame.GoBack(); }
		void onEdit(object sender, RoutedEventArgs e) { b2.Visibility = Visibility.Visible; b1.Visibility = Visibility.Collapsed; }
		void onView(object sender, RoutedEventArgs e) { b1.Visibility = Visibility.Visible; b2.Visibility = Visibility.Collapsed; }

		void m1() { l1.RenderTransform = new RotateTransform { Angle = 120 }; }
	}
}
