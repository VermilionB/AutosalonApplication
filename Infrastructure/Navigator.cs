using System;
using Autosalon.Base;

namespace Autosalon.Infrastructure;

public class Navigator
{
    public event Action? CurrentViewModelChanged;
    private ViewModelBase? _currentViewModel;
    public ViewModelBase? CurrentViewModel
    {
        get
        {
            return _currentViewModel;
        }
        set
        {
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }

}