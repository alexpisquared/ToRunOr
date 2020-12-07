using System.Windows.Input;
using Windows.UI.Xaml;
using AsLink;
using MVVM.Common;
using System;

namespace VMs
{
	public class MainPageVM : BindableBase
	{
		public static MainPageVM Instance { get; }
		static MainPageVM() { Instance = Instance ?? new MainPageVM(); }// implement singleton pattern		}

		MainPageVM()
		{
#if DEBUG
			IsDevDbg = true;
			DevDbgViz = Visibility.Visible;
#else
			IsDevDbg = false;
			DevDbgViz = Visibility.Collapsed;
#endif
		}

		public DateTime ResumedAt = DateTime.Now;

		bool _IsDevDbg;           /**/public bool IsDevDbg { get { return _IsDevDbg; } set { Set(ref _IsDevDbg, value); } }
		Visibility _DevDbgViz;    /**/public Visibility DevDbgViz { get { return _DevDbgViz; } set { Set(ref _DevDbgViz, value); } }
		string _ResumeHist;        /**/

		public string ResumeHist
		{
			get
			{
				_ResumeHist = AppSettingsHelper.ReadVal("ResumeHist") as string;
				return _ResumeHist??"";
			}
			set
			{
				const int maxLen = 100;
				if (Set(ref _ResumeHist, value))
					AppSettingsHelper.SaveVal("ResumeHist", value.Length < maxLen ? value : value.Substring(value.Length - maxLen));
			}
		}



		ICommand _CutText;     /**/public ICommand CutText { get { return _CutText ?? (_CutText = new RelayCommand(x => { ResumeHist = _ResumeHist.Substring(ResumeHist.Length / 2); })); } }

		public void ScenarioCleanup() { }
	}

}
