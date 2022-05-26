using System.Windows.Controls;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Infrastructure;
using Autosalon.Pages;

namespace Autosalon.ViewModels;

public class CustomerViewModel : ViewModelBase
{
    private Page _currentPage;
    private Page _toSellCarsPage;
    private Page _toBuyCarsPage;
    private Page _homePage;
    private Page _myOrdersPage;
    private Page _myProfilePage;
    private Page _aboutPage;

    public Page CurrentPage
    {
        get => _currentPage;
        set => Set(ref _currentPage, value);
    }
    
    public CustomerViewModel()
    {
        _myProfilePage = new MyProfilePage();
        _myOrdersPage = new MyOrdersPage();
        _homePage = new HomePage();
        _toSellCarsPage = new ToSellCarsPage();
        _toBuyCarsPage = new ToBuyCarsPage();
        _aboutPage = new AboutPage();
        CurrentPage = _homePage;

        ToHomePageCommand = new RelayCommand(OnHomePageExecute, CanHomePageCanExecuted);
        ToSellCarsButtonCommand = new RelayCommand(OnToSellCarsExecute, CanToSellCarsExecuted);
        ToBuyCarsButtonCommand = new RelayCommand(OnToBuyCarsExecute, CanToBuyCarsExecuted);
        MyOrdersCommand = new RelayCommand(OnMyOrdersExecute, CanMyOrdersExecuted);
        MyProfilePageCommand = new RelayCommand(OnMyProfileExecute, CanMyProfileExecuted);
        AboutCommand = new RelayCommand(OnAboutExecute, CanAboutExecuted);
    }

    public ICommand MyOrdersCommand { get; }
    public ICommand ToSellCarsButtonCommand { get; }
    public ICommand ToBuyCarsButtonCommand { get; }
    public ICommand MyProfilePageCommand { get; }
    public ICommand AboutCommand { get; }

    private bool CanAboutExecuted(object o) => true;

    private void OnAboutExecute(object o)
    {
        _aboutPage.DataContext = new AboutViewModel();
        CurrentPage = _aboutPage;
    }

    private bool CanMyProfileExecuted(object o) => true;
    private void OnMyProfileExecute(object o)
    {
        _myProfilePage.DataContext = new MyProfileViewModel(this);
        CurrentPage = _myProfilePage;
    }

    private bool CanMyOrdersExecuted(object o) => true;
    
    private void OnMyOrdersExecute(object o)
    {
        _myOrdersPage.DataContext = new MyOrdersViewModel();
        CurrentPage = _myOrdersPage;
    }
    private bool CanToSellCarsExecuted(object o) => true;

    private void OnToSellCarsExecute(object o)
    {
        _toSellCarsPage.DataContext = new ToSellCarsViewModel();
        CurrentPage = _toSellCarsPage;
    }

    private bool CanToBuyCarsExecuted(object o) => true;

    private void OnToBuyCarsExecute(object o)
    {
        _toBuyCarsPage.DataContext = new ToBuyCarsViewModel(this);
        CurrentPage = _toBuyCarsPage;
    }

    #region ToHomePageCommand

    public ICommand ToHomePageCommand { get; }

    private bool CanHomePageCanExecuted(object o) => true;

    private void OnHomePageExecute(object o)
    {
        _homePage.DataContext = new HomePageViewModel();
        CurrentPage = _homePage;
    }

    #endregion
    
    
    
}