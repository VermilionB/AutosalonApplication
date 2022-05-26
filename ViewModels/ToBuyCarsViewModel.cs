using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Infrastructure;
using Autosalon.Models;
using Autosalon.Pages;
using static System.String;

namespace Autosalon.ViewModels;

public class ToBuyCarsViewModel : ViewModelBase
{
    private ObservableCollection<Automobile>? _carList;

    private CustomerViewModel _customerViewModel;
    private string? _filter1;
    private string? _filter2;

    private string? _searchBox;
    private Page _carPage;
    
    public string? Filter1
    {
        get => _filter1;
        set
        {
            Set(ref _filter1, value);
            CarList = ToFilterList();
        }
    }

    public string? Filter2
    {
        get => _filter2;
        set
        {
            Set(ref _filter2, value);
            CarList = ToFilterList();
        }
    }

    public string? SearchBox
    {
        get => _searchBox;
        set
        {
            Set(ref _searchBox, value);
            CarList = SearchCommand();
        }
    }
    
    public Page CarPage
    {
        get => _carPage;
        set => Set(ref _carPage, value);
    }
    public ToBuyCarsViewModel(CustomerViewModel customerViewModel)
    {
        _customerViewModel = customerViewModel;
        CarPage = new CarPage();
        
        SortByBrandCommand = new RelayCommand(OnSortByBrandExecute, CanSortByBrandExecuted);
        SortByPriceCommand = new RelayCommand(OnSortByPriceExecute, CanSortByPriceExecuted);
        SortByPriceDescCommand = new RelayCommand(OnSortByPriceDescExecute, CanSortByPriceDescExecuted);
    }

    public ObservableCollection<Automobile>? CarList
    {
        get => _carList ??= new ObservableCollection<Automobile>(AutosalonContext.GetContext().Automobiles.Where(t => t.Approved == "Approved"));
        set => Set(ref _carList, value);
    }
    
    private Automobile _selectedCar;
    public Automobile SelectedCar
    {
        get => _selectedCar;
        set
        {
            Set(ref _selectedCar, value);
            CarPage.DataContext = new CarPageViewModel(this);
            _customerViewModel.CurrentPage = CarPage;
        }
    }

   
    
    #region FilterTextInputCommand

    private ObservableCollection<Automobile> ToFilterList()
    {
        if(!IsNullOrEmpty(Filter1) && IsNullOrEmpty(Filter2))
        {
            var value = int.Parse(Filter1);
            var sortedList = new ObservableCollection<Automobile>(CarList.Where(t => t.Price >= value && t.Approved == "Approved"));
            return sortedList;
        }

        if(!IsNullOrEmpty(Filter1) && !IsNullOrEmpty(Filter2))
        {
            var value1 = int.Parse(Filter1);
            var value2 = int.Parse(Filter2);
            var sortedList = new ObservableCollection<Automobile>(CarList.Where(t => t.Price >= value1 && t.Price <= value2 && t.Approved == "Approved"));
            return sortedList;

        }

        if (!IsNullOrEmpty(Filter2) && IsNullOrEmpty(Filter1))
        {
            var value = int.Parse(Filter2);
            var sortedList = new ObservableCollection<Automobile>(CarList.Where(t => t.Price <= value && t.Approved == "Approved"));
            return sortedList;

        }

        return new ObservableCollection<Automobile>(AutosalonContext.GetContext().Automobiles.Where(t => t.Approved == "Approved"));
    }
    
    #endregion
    

    #region SearchInputCommand
    private ObservableCollection<Automobile> SearchCommand()
    {
        if(SearchBox.Length == 0)
        {
            return new ObservableCollection<Automobile>(AutosalonContext.GetContext().Automobiles.Where(t => t.Approved == "Approved"));
        }
        Regex regex = new Regex(SearchBox?.ToLower() ?? string.Empty);
        return new ObservableCollection<Automobile>(CarList.Where(t => regex.Match(t.Brand?.ToLower() ?? string.Empty).Success));
    }
    #endregion
     
    
    #region SortCommands
    public ICommand SortByBrandCommand { get; }
    public ICommand SortByPriceCommand { get; }
    public ICommand SortByPriceDescCommand { get; }
    
    
    private bool CanSortByBrandExecuted(object o) => true;
    private void OnSortByBrandExecute(object o)
    {
        var sortedList = new ObservableCollection<Automobile>(CarList.Where(t => t.Approved == "Approved").OrderBy(t => t.Brand));
        CarList = sortedList;
    }
    
    private bool CanSortByPriceExecuted(object o) => true;
    private void OnSortByPriceExecute(object o)
    {
        var sortedList = new ObservableCollection<Automobile>(CarList.Where(t => t.Approved == "Approved").OrderBy(t => t.Price));
        CarList = sortedList;
    }
    
    private bool CanSortByPriceDescExecuted(object o) => true;
    private void OnSortByPriceDescExecute(object o)
    {
        var sortedList = new ObservableCollection<Automobile>(CarList.Where(t => t.Approved == "Approved").OrderByDescending(t => t.Price));
        CarList = sortedList;
    }
    #endregion
}