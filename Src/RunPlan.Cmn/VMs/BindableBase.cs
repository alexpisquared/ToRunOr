using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VMs
{
  public abstract class BindableBase : INotifyPropertyChanged
  {
    protected bool Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null) // From http://danrigby.com/2012/04/01/inotifypropertychanged-the-net-4-5-way-revisited/
    {
      if (Equals(storage, value))
      {
        return false;
      }

      storage = value;
      this.OnPropertyChanged(propertyName);
      return true;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }



  public abstract class BindableBase_MVA : INotifyPropertyChanged // from MVA
  {
    protected void Set<T>(ref T storage, T value, [CallerMemberName()]string propertyName = null)
    {
      if (!object.Equals(storage, value))
      {
        storage = value;
        this.OnPropertyChanged(propertyName);
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

  }

}
