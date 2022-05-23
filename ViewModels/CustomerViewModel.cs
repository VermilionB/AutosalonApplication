using System.Windows.Controls;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Infrastructure;
using Autosalon.Pages;

namespace Autosalon.ViewModels;

public class CustomerViewModel : ViewModelBase
{
    private readonly Navigator _navigator;
    private Page _currentPage;
    private Page _toSellCarsPage;
    private Page _toBuyCarsPage;
    private Page _homePage;
    private Page _myOrdersPage;

    public Page CurrentPage
    {
        get => _currentPage;
        set => Set(ref _currentPage, value);
    }

    public ViewModelBase? CurrentViewModel => _navigator.CurrentViewModel;

    public CustomerViewModel(Navigator navigator)
    {
        _navigator = navigator;
        _navigator.CurrentViewModelChanged += OnCurrentViewModelChanged;

        _myOrdersPage = new MyOrdersPage();
        _homePage = new HomePage();
        _toSellCarsPage = new ToSellCarsPage();
        _toBuyCarsPage = new ToBuyCarsPage();
        CurrentPage = _homePage;

        ToHomePageCommand = new RelayCommand(OnHomePageExecute, CanHomePageCanExecuted);
        ToSellCarsButtonCommand = new RelayCommand(OnToSellCarsExecute, CanToSellCarsExecuted);
        ToBuyCarsButtonCommand = new RelayCommand(OnToBuyCarsExecute, CanToBuyCarsExecuted);
        MyOrdersCommnd = new RelayCommand(OnMyOrdersExecute, CanMyOrdersExecuted);
    }

    public ICommand MyOrdersCommnd { get; }
    public ICommand ToSellCarsButtonCommand { get; }
    public ICommand ToBuyCarsButtonCommand { get; }

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

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}