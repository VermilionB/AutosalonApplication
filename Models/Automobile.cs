using System;
using System.Collections.Generic;

namespace Autosalon.Models
{
    public partial class Automobile
    {
        public Automobile()
        {
            Orders = new HashSet<Order>();
            Requests = new HashSet<Request>();
        }
        
        public Automobile(Guid id, string brand, string model, string color, double price, int mileage, string image, int power,
            string fuel, DateTime releaseDate, string approved)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Color = color;
            Price = price;
            Mileage = mileage;
            Image = image;
            Power = power;
            Fuel = fuel;
            ReleaseDate = releaseDate;
            Approved = approved;
        }

        public Guid Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public double? Price { get; set; }
        public int Mileage { get; set; }
        public string Image { get; set; } = null!;
        public int Power { get; set; }
        public string Fuel { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string Approved { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
