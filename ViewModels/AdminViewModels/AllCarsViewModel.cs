﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Models;
using Autosalon.Pages;
using Autosalon.Pages.AdminPages;
using Autosalon.ViewModels.AdminViewModels;
using static System.String;

namespace Autosalon.ViewModels.AdminViewModels;

public class AllCarsViewModel : ViewModelBase
{
    private ObservableCollection<Automobile>? _carList;

    private AdminViewModel _adminViewModel;
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
        set => Set(ref _searchBox, value);
    }
    
    public Page CarPage
    {
        get => _carPage;
        set => Set(ref _carPage, value);
    }
    public AllCarsViewModel(AdminViewModel adminViewModel)
    {
        _adminViewModel = adminViewModel;
        CarPage = new CarPageForAdmin();
        SortByBrandCommand = new RelayCommand(OnSortByBrandExecute, CanSortByBrandExecuted);
        SortByPriceCommand = new RelayCommand(OnSortByPriceExecute, CanSortByPriceExecuted);
        
        SortByPriceDescCommand = new RelayCommand(OnSortByPriceDescExecute, CanSortByPriceDescExecuted);
        SearchInputCommand = new RelayCommand(OnSearchInputExecute, CanSearchInputExecuted);
        
    }

    public ObservableCollection<Automobile>? CarList
    {
        get => _carList ??= new ObservableCollection<Automobile>(AutosalonContext.GetContext().Automobiles);
        set => Set(ref _carList, value);
    }
    
    private Automobile _selectedCar;
    public Automobile SelectedCar
    {
        get => _selectedCar;
        set
        {
            Set(ref _selectedCar, value);
            CarPage.DataContext = new CarPageForAdminViewModel(this);
            _adminViewModel.CurrentPage = CarPage;
        }
    }

   
    
    #region FilterTextInputCommand

    private ObservableCollection<Automobile> ToFilterList()
    {
        if(!IsNullOrEmpty(Filter1) && IsNullOrEmpty(Filter2))
        {
            var value = int.Parse(Filter1);
            var sortedList = new ObservableCollection<Automobile>(CarList.Where(t => t.Price >= value));
            return sortedList;
        }

        if(!IsNullOrEmpty(Filter1) && !IsNullOrEmpty(Filter2))
        {
            var value1 = int.Parse(Filter1);
            var value2 = int.Parse(Filter2);
            var sortedList = new ObservableCollection<Automobile>(CarList.Where(t => t.Price >= value1 && t.Price <= value2));
            return sortedList;

        }

        if (!IsNullOrEmpty(Filter2) && IsNullOrEmpty(Filter1))
        {
            var value = int.Parse(Filter2);
            var sortedList = new ObservableCollection<Automobile>(CarList.Where(t => t.Price <= value));
            return sortedList;

        }

        return new ObservableCollection<Automobile>(AutosalonContext.GetContext().Automobiles);
    }
    
    #endregion
    

    #region SearchInputCommand
    public ICommand SearchInputCommand { get; }
    private bool CanSearchInputExecuted(object o) => true;
    private void OnSearchInputExecute(object o)
    {
        Regex regex = new Regex(SearchBox?.ToLower() ?? string.Empty);
        var searchList = new ObservableCollection<Automobile>(CarList.Where(t => regex.Match(t.Brand.ToLower()).Success));
    }
    #endregion
     
    
    #region SortCommands
    public ICommand SortByBrandCommand { get; }
    public ICommand SortByPriceCommand { get; }
    public ICommand SortByPriceDescCommand { get; }
    
    
    private bool CanSortByBrandExecuted(object o) => true;
    private void OnSortByBrandExecute(object o)
    {
        var sortedList = new ObservableCollection<Automobile>(CarList.OrderBy(t => t.Brand));
        CarList = sortedList;
    }
    
    private bool CanSortByPriceExecuted(object o) => true;
    private void OnSortByPriceExecute(object o)
    {
        var sortedList = new ObservableCollection<Automobile>(CarList.OrderBy(t => t.Price));
        CarList = sortedList;
    }
    
    private bool CanSortByPriceDescExecuted(object o) => true;
    private void OnSortByPriceDescExecute(object o)
    {
        var sortedList = new ObservableCollection<Automobile>(CarList.OrderByDescending(t => t.Price));
        CarList = sortedList;
    }
    #endregion
}