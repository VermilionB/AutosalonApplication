using System.Collections.ObjectModel;
using Autosalon.Base;
using Autosalon.Models;

namespace Autosalon.ViewModels.AdminViewModels;

public class AllManagersViewModel : ViewModelBase
{
    private ObservableCollection<Manager> _managersList;
    
    public ObservableCollection<Manager> ManagersList
    {
        get => _managersList;
        set => Set(ref _managersList, value);
    }

    public AllManagersViewModel()
    {
        ManagersList = new ObservableCollection<Manager>(AutosalonContext.GetContext().Managers);
    }
}