using System;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Models;

namespace Autosalon.ViewModels;

public class CarPageViewModel : ViewModelBase
{
    private Automobile _selectedAutomobile;


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
        SelectedAutomobile = toBuyCarsViewModel.SelectedCar;
        AddToCartCommand = new RelayCommand(OnAddToCartExecute, CanAddToCartExecuted);
    }

    public ICommand AddToCartCommand { get; }

    private bool CanAddToCartExecuted(object o) => true;

    private void OnAddToCartExecute(object o)
    {
        using (var context = new AutosalonContext())
        {
            var order = new Order(); ;
            // order.CustomerId = 
            order.AutomobileId = SelectedAutomobile.Id;
            order.Date = DateTime.Now;
            order.Status = "In processing";
            context.Automobiles.Remove(SelectedAutomobile);
            context.SaveChanges();
        }


        // cart.AddToCart(SelectedAutomobile);
    }
}