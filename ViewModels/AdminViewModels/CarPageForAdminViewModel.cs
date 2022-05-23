using Autosalon.Base;
using Autosalon.Models;

namespace Autosalon.ViewModels.AdminViewModels;

public class CarPageForAdminViewModel : ViewModelBase
{
    public CarPageForAdminViewModel(AllCarsViewModel allCarsViewModel)
    {
        SelectedAutomobile = allCarsViewModel.SelectedCar;
    }
    
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
    
}