using AsLink;
using VMs;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ToRunOr
{
	public sealed partial class ZoomPanels : Page
	{
		MainPageVM _vm = null;

		public ZoomPanels()
		{
			this.InitializeComponent();

			_vm = MainPageVM.Instance ;;
			DataContext = _vm;

			var bt = DevOp.BuildTime(typeof(App));
#if DEBUG
			ApplicationView.GetForCurrentView().Title = tbVer.Text = $@"Dbg: {(DateTime.Now - bt):d\ h\:mm} ago";
#else
			ApplicationView.GetForCurrentView().Title = tbVer.Text = $@"Rls: {bt}";
#endif


			//C:\gh\Windows - universal - samples\Samples\BackButton\cs\Scenario1.xaml.cs
			// I want this page to be always cached so that we don't have to add logic to save/restore state for the checkbox.
			this.NavigationCacheMode = NavigationCacheMode.Required;
		}

		protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
		{
			_vm?.ScenarioCleanup();
		}

		async void onMax(object sender, RoutedEventArgs e)
		{
			var btn = ((AppBarToggleButton) sender);
			btn.Icon = new SymbolIcon(Symbol.ZoomOut);

			while ((FrameworkElement) btn.Parent == null || ActualWidth <= 0 || ActualWidth <= 0) await Task.Delay(100);

			var prn = (FrameworkElement) btn.Parent;
			prn.Width = ActualWidth;
			prn.Height = ActualHeight;

			//..Debug.WriteLine($"{btn.ActualWidth}x{btn.ActualHeight}"); // 68x61.5 on phone, 68x60 on PC
		}

		void onMin(object sender, RoutedEventArgs e)
		{
			var btn = ((AppBarToggleButton) sender);
			btn.Icon = new SymbolIcon(Symbol.ZoomIn);

			var prn = ((FrameworkElement) btn.Parent);
			prn.Width = prn.Height = 128;
		}




		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
		}
	}
}
