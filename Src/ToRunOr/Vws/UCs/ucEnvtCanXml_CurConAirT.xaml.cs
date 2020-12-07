using System.Threading.Tasks;
using VMs;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ToRunOr.Vws.UCs
{
	public sealed partial class ucEnvtCanXml_CurConAirT : UserControl
	{
		EnvtCanXmlVM _vm = null;

		public ucEnvtCanXml_CurConAirT()
		{
			this.InitializeComponent();
			if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;

			_vm = EnvtCanXmlVM.Instance;
			DataContext = _vm;

			Loaded += OnResuming;
			Application.Current.Resuming += OnResuming;
		}

		async void OnResuming(object sender, object o) { ucRoot.Opacity = .1; await _vm.Refresh(); ucRoot.Opacity = 1; }
	}
}
