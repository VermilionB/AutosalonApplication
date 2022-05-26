using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Windows.Controls;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Infrastructure;
using Autosalon.Pages;
using Autosalon.Pages.AdminPages;

namespace Autosalon.ViewModels.AdminViewModels;

public class AdminViewModel : ViewModelBase
{
    private Page _currentPage;
    private Page _addCarPage;
    private Page _allCarsPage;
    private Page _homePageForAdmin;
    private Page _ordersPage;
    private Page _allManagersPage;
    private Page _myAdminProfilePage;

    public Page CurrentPage
    {
        get => _currentPage;
        set => Set(ref _currentPage, value);
    }
    
    public AdminViewModel()
    {

        _homePageForAdmin = new HomePageForAdmin();
        _addCarPage = new AddCarPage();
        _allCarsPage = new AllCarsPage();
        _ordersPage = new OrdersPage();
        _allManagersPage = new AllManagers();
        _myAdminProfilePage = new MyAdminProfilePage();
        
        CurrentPage = _homePageForAdmin;
        _homePageForAdmin.DataContext = new HomePageForAdminViewModel();

        ToHomePageCommand = new RelayCommand(OnHomePageExecute, CanHomePageCanExecuted);
        ToSellCarsButtonCommand = new RelayCommand(OnToSellCarsExecute, CanToSellCarsExecuted);
        AllCarsButtonCommand = new RelayCommand(OnAllCarsExecute, CanAllCarsExecuted);
        OrdersButtonCommand = new RelayCommand(OnOrdersExecute, CanOrdersExecuted);
        AllManagersButtonCommand = new RelayCommand(OnAllManagersExecute, CanAllManagersExecuted);
        MyAdminProfileCommand = new RelayCommand(OnMyAdminProfileExecute, CanMyAdminProfileExecuted);
    }

    public ICommand MyAdminProfileCommand { get; }
    public ICommand ToSellCarsButtonCommand { get; }
    public ICommand AllCarsButtonCommand { get; }
    public ICommand OrdersButtonCommand { get; }
    public ICommand AllManagersButtonCommand { get; }
    
    private bool CanMyAdminProfileExecuted(object o) => true;
    private void OnMyAdminProfileExecute(object o)
    {
        _myAdminProfilePage.DataContext = new MyAdminProfileViewModel(this);
        CurrentPage = _myAdminProfilePage;
    }
    

    private bool CanAllManagersExecuted(object o) => true;
    
    private void OnAllManagersExecute(object o)
    {
        _allManagersPage.DataContext = new AllManagersViewModel();
        CurrentPage = _allManagersPage;
    }

    private bool CanToSellCarsExecuted(object o) => true;

    private void OnToSellCarsExecute(object o)
    {
        _addCarPage.DataContext = new AddCarViewModel();
        CurrentPage = _addCarPage;
    }

    private bool CanAllCarsExecuted(object o) => true;

    private void OnAllCarsExecute(object o)
    {
        _allCarsPage.DataContext = new AllCarsViewModel(this);
        CurrentPage = _allCarsPage;
    }

    private bool CanOrdersExecuted(object o) => true;

    private void OnOrdersExecute(object o)
    {
        _ordersPage.DataContext = new OrdersViewModel();
        CurrentPage = _ordersPage;
    }

    #region ToHomePageCommand

    public ICommand ToHomePageCommand { get; }

    private bool CanHomePageCanExecuted(object o) => true;

    private void OnHomePageExecute(object o)
    {
        _homePageForAdmin.DataContext = new HomePageForAdminViewModel();

        CurrentPage = _homePageForAdmin;
    }

    #endregion
    
}