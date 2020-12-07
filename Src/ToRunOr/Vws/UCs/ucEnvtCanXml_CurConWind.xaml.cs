using System;
using System.Threading.Tasks;
using VMs;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace ToRunOr.Vws.UCs
{
	public sealed partial class UcEnvtCanXml_CurConWind : UserControl
	{
		EnvtCanXmlVM _vm = null;

		public UcEnvtCanXml_CurConWind()
		{
			this.InitializeComponent();
			if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;

			_vm = EnvtCanXmlVM.Instance;
			DataContext = _vm;

			Loaded += OnResuming;
			Application.Current.Resuming += OnResuming;
		}

		async void OnResuming(object sender, object o)
		{
			ucRoot.Opacity = .1;
			await _vm.Refresh();
			ucRoot.Opacity = 1;
			edkfSecEnd2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(50 / (1.5 + _vm.WindKmph)));
			edkfSecEnd3.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(50 / (1.5 + _vm.WindGust)));
		}
	}
}
