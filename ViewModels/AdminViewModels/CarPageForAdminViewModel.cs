using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Models;
using Autosalon.Pages;
using Autosalon.Resources.UserControls;

namespace Autosalon.ViewModels.AdminViewModels;

public class CarPageForAdminViewModel : ViewModelBase
{
    public CarPageForAdminViewModel(AllCarsViewModel allCarsViewModel)
    {
        SelectedAutomobile = allCarsViewModel.SelectedCar;
        ApproveCommand = new RelayCommand(OnApproveCommandExecute, CanApproveCommandExecuted);
    }
    
    public ICommand ApproveCommand { get; }
    private bool CanApproveCommandExecuted(object o) => true;
    private void OnApproveCommandExecute(object o)
    {
        SelectedAutomobile.Approved = "Approved";
        using (var db = new AutosalonContext())
        {
            db.Automobiles.Update(SelectedAutomobile);
            db.SaveChanges();
        }
        var message = new CustomMessageBox("Car approved successfully", MessageType.Info , MessageButtons.Ok).ShowDialog();
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

    public string Approved
    {
        get => SelectedAutomobile.Approved;
    }
    public Automobile SelectedAutomobile
    {
        get => _selectedAutomobile;
        set => Set(ref _selectedAutomobile, value);
    }
    
}