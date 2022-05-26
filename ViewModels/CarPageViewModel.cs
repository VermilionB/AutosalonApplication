using System;
using System.Windows;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Models;

namespace Autosalon.ViewModels;

public class CarPageViewModel : ViewModelBase
{
    private Automobile _selectedAutomobile;
    private Window _orderForm;

    public string Power
    {
        get => SelectedAutomobile.Power.ToString();
    }

    public string? Brand
    {
        get => SelectedAutomobile.Brand;
    }

    public string? Model
    {
        get => SelectedAutomobile.Model;
    }

    public string? Price
    {
        get => SelectedAutomobile.Price.ToString();
    }

    public double Mileage
    {
        get => SelectedAutomobile.Mileage;
    }

    public string Image
    {
        get => _selectedAutomobile.Image;
    }

    public string ReleaseDate
    {
        get => SelectedAutomobile.ReleaseDate.ToString("dd.MM.yyyy");
    }

    public string Fuel
    {
        get => SelectedAutomobile.Fuel;
    }

    public string? Color
    {
        get => SelectedAutomobile.Color;
    }


    public Automobile SelectedAutomobile
    {
        get => _selectedAutomobile;
        set => Set(ref _selectedAutomobile, value);
    }

    public CarPageViewModel(ToBuyCarsViewModel toBuyCarsViewModel)
    {
        _orderForm = new OrderForm();

        SelectedAutomobile = toBuyCarsViewModel.SelectedCar;
        AddToCartCommand = new RelayCommand(OnAddToCartExecute, CanAddToCartExecuted);
    }
    
    public string Description
    {
        get => SelectedAutomobile.Description;
    }

    public ICommand AddToCartCommand { get; }

    private bool CanAddToCartExecuted(object o) => true;

    private void OnAddToCartExecute(object o)
    {
        _orderForm.DataContext = new OrderFormViewModel(this);
        _orderForm.ShowDialog();
        
    }
}