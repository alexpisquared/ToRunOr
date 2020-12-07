using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using VMs;

namespace ToRunOr.Vws.UCs
{
	public sealed partial class ucEnvtCanHtml_PastFore24Hr : UserControl
	{
		EnvtCanXmlVM _vm = null;

		public ucEnvtCanHtml_PastFore24Hr()
		{
			this.InitializeComponent();
			if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;

			_vm = EnvtCanXmlVM.Instance;
			DataContext = _vm;

			Loaded += OnResuming;
			Application.Current.Resuming += OnResuming;
		}

		async void OnResuming(object sender, object o) { await _vm.Refresh(); }
	}
}
