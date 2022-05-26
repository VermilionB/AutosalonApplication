using System;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Infrastructure;
using Autosalon.Models;
using Microsoft.Win32;

namespace Autosalon.ViewModels.AdminViewModels;

public class AddCarViewModel : ViewModelBase
{
    private readonly OpenFileDialog _openFileDialog = new OpenFileDialog();

    public AddCarViewModel()
    {
        AddCarCommand = new RelayCommand(OnAddCarExecute, CanAddCarExecuted);
        AddImageCommand = new RelayCommand(OnAddImageExecute, CanAddImageExecuted);
    }

    private string _brand;
    private string _model;
    private string _price;
    private double _mileage;
    private string _imagePath;
    private DateTime _releaseDate;
    private string _power;
    private string _fuel;
    private string _color;
    private string _description;

    public string Power
    {
        get => _power;
        set => Set(ref _power, value);
    }

    public string Brand
    {
        get => _brand;
        set => Set(ref _brand, value);
    }

    public string Model
    {
        get => _model;
        set => Set(ref _model, value);
    }

    public string Price
    {
        get => _price;
        set => Set(ref _price, value);
    }

    public double Mileage
    {
        get => _mileage;
        set => Set(ref _mileage, value);
    }

    public string ImagePath
    {
        get => _imagePath;
        set => Set(ref _imagePath, value);
    }

    public DateTime ReleaseDate
    {
        get => _releaseDate;
        set => Set(ref _releaseDate, value);
    }

    public string Fuel
    {
        get => _fuel;
        set => Set(ref _fuel, value);
    }

    public string Color
    {
        get => _color;
        set => Set(ref _color, value);
    }
    public string Description
    {
        get => _description;
        set => Set(ref _description, value);
    }

    public ICommand AddCarCommand { get; }
    private bool CanAddCarExecuted(object o) => true;

    private void OnAddCarExecute(object o)
    {
        Automobile newCar = new Automobile(Guid.NewGuid(), Brand, Model, Color, int.Parse(Price), (int) Mileage,
            _openFileDialog.FileName,
            int.Parse(Power), Fuel, ReleaseDate, "Approved", Description);


        using (var db = AutosalonContext.GetContext())
        {
            db.Automobiles.Add(newCar);
            db.SaveChanges();
        }

        ClearAllFields();
    }

    public ICommand AddImageCommand { get; }
    private bool CanAddImageExecuted(object o) => true;

    private void OnAddImageExecute(object o)
    {
        _openFileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)" +
                                 "|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
        _openFileDialog.ShowDialog();
        ImagePath = _openFileDialog.FileName;
    }


    private void ClearAllFields()
    {
        Brand = "";
        Model = "";
        Price = "";
        Mileage = 0;
        ReleaseDate = DateTime.Now;
        Power = "";
        Fuel = "";
        ImagePath = "";
        Color = "";
    }
}